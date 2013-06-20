// Copyright (c) 2013 Eugen Pechanec

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
namespace EugenPechanec.NativeWifi.Wlan {
    public sealed class WlanHostedNetwork {

        // FIELDS ==================================================================

        private readonly WlanClient client;

        // PROPERTIES ==============================================================

        public WlanHostedNetworkState State { get; private set; }

        public Guid Guid { get; private set; }

        public PhysicalAddress MacAddress { get; private set; }

        public Dot11PhyType PhyType { get; private set; }

        public int Channel { get; private set; }

        public WlanHostedNetworkPeerState[] Peers { get; private set; }

        public bool Started { get { return State == WlanHostedNetworkState.Active; } }

        public bool Enabled {
            get { return (bool)QueryProperty(WlanHostedNetworkOpcode.Enable); }
            set { SetProperty(WlanHostedNetworkOpcode.Enable, value); }
        }

        private WlanHostedNetworkSecuritySettings SecuritySettings {
            get { return (WlanHostedNetworkSecuritySettings)QueryProperty(WlanHostedNetworkOpcode.SecuritySettings); }
        }

        private WlanHostedNetworkConnectionSettings ConnectionSettings {
            get { return (WlanHostedNetworkConnectionSettings)QueryProperty(WlanHostedNetworkOpcode.ConnectionSettings); }
            set { SetProperty(WlanHostedNetworkOpcode.ConnectionSettings, value); }
        }

        public Key SecondaryKey {
            get { return QuerySecondaryKey(); }
            set { SetSecondaryKey(value); }
        }

        public Dot11Ssid Ssid {
            get { return ConnectionSettings.Ssid; }
            set {
                WlanHostedNetworkConnectionSettings settings = ConnectionSettings;
                settings.Ssid = value;
                ConnectionSettings = settings;
            }
        }

        public int MaxNumberOfPeers {
            get { return (int)ConnectionSettings.MaxNumberOfPeers; }
            set {
                WlanHostedNetworkConnectionSettings settings = ConnectionSettings;
                settings.MaxNumberOfPeers = (uint)value;
                ConnectionSettings = settings;
            }
        }

        public Dot11AuthAlgorithm AuthenticationAlgorithm { get { return SecuritySettings.AuthAlgo; } }

        public Dot11CipherAlgorithm CipherAlgorithm { get { return SecuritySettings.CipherAlgo; } }

        /// <summary>
        /// Gets the network interface of this wireless interface.
        /// </summary>
        /// <remarks>
        /// The network interface allows querying of generic network properties such as the interface's IP address.
        /// </remarks>
        public NetworkInterface NetworkInterface {
            get {
                // Do not cache the NetworkInterface; We need it fresh
                // each time cause otherwise it caches the IP information.
                foreach (NetworkInterface netIface in NetworkInterface.GetAllNetworkInterfaces()) {
                    Guid netIfaceGuid = new Guid(netIface.Id);
                    if (netIfaceGuid.Equals(Guid)) {
                        return netIface;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Gets network interface name.
        /// </summary>
        public string Name { get { return NetworkInterface.Name; } }

        /// <summary>
        /// Gets network interface description.
        /// </summary>
        public string Description { get { return NetworkInterface.Description; } }

        // CONSTRUCTORS ============================================================

        private WlanHostedNetwork(WlanClient client) {
            this.client = client;
            InitSettings();
        }

        public static WlanHostedNetwork CreateHostedNetwork(WlanClient client) {
            WlanHostedNetwork hnwk = new WlanHostedNetwork(client);

            hnwk.ReloadStatus();
            hnwk.HnwkStateChange += (sender, e) => { hnwk.ReloadStatus(); };
            hnwk.HnwkRadioStateChange += (sender, e) => { hnwk.ReloadStatus(); };
            hnwk.HnwkPeerStateChange += (sender, e) => { hnwk.ReloadStatus(); };

            return hnwk;
        }

        // METHODS =================================================================

        private void ReloadStatus() {
            WlanHostedNetworkPeerState[] peerList;
            WlanHostedNetworkStatus status = QueryStatus(out peerList);

            State = status.State;
            Guid = status.IPDeviceID;
            MacAddress = new PhysicalAddress(status.MacAddress.Value);
            PhyType = status.PhyType;
            Channel = (int)status.ChannelFrequency;
            Peers = peerList;
        }

        public void Start() {
            ForceStart();
        }

        public void Stop() {
            ForceStop();
        }

        public void RefreshSecuritySettings() {
            WlanHostedNetworkReason failReason;
            Util.ThrowIfError(NativeMethods.WlanHostedNetworkRefreshSecuritySettings(client.clientHandle, out failReason, IntPtr.Zero));
            //return failReason;
        }

        private WlanHostedNetworkReason ForceStart() {
            WlanHostedNetworkReason failReason;
            Util.ThrowIfError(NativeMethods.WlanHostedNetworkForceStart(client.clientHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        private WlanHostedNetworkReason ForceStop() {
            WlanHostedNetworkReason failReason;
            Util.ThrowIfError(NativeMethods.WlanHostedNetworkForceStop(client.clientHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        private WlanHostedNetworkReason InitSettings() {
            WlanHostedNetworkReason failReason;
            Util.ThrowIfError(NativeMethods.WlanHostedNetworkInitSettings(client.clientHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        private WlanHostedNetworkReason StartUsing() {
            WlanHostedNetworkReason failReason;
            Util.ThrowIfError(NativeMethods.WlanHostedNetworkStartUsing(client.clientHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        private WlanHostedNetworkReason StopUsing() {
            WlanHostedNetworkReason failReason;
            Util.ThrowIfError(NativeMethods.WlanHostedNetworkStopUsing(client.clientHandle, out failReason, IntPtr.Zero));
            return failReason;
        }

        // INTERNALS ===============================================================

        private WlanHostedNetworkStatus QueryStatus(out WlanHostedNetworkPeerState[] peerList) {
            IntPtr statusPtr;
            Util.ThrowIfError(NativeMethods.WlanHostedNetworkQueryStatus(client.clientHandle, out statusPtr, IntPtr.Zero));
            try {
                //WlanHostedNetworkPeerState[] peerList;
                WlanHostedNetworkStatus status = ConvertHostedNetworkStatusPtr(statusPtr, out peerList);
                return status;
            } finally {
                NativeMethods.WlanFreeMemory(statusPtr);
            }
        }

        private WlanHostedNetworkStatus ConvertHostedNetworkStatusPtr(IntPtr statusPtr, out WlanHostedNetworkPeerState[] peerList) {
            WlanHostedNetworkStatus status = (WlanHostedNetworkStatus)Marshal.PtrToStructure(statusPtr, typeof(WlanHostedNetworkStatus));
            uint numberOfItems = status.NumberOfPeers;
            Int64 peersIterator = statusPtr.ToInt64() + Marshal.OffsetOf(typeof(WlanHostedNetworkStatus), "PeerList").ToInt64();
            peerList = new WlanHostedNetworkPeerState[numberOfItems];
            for (int i = 0; i < numberOfItems; i++) {
                peerList[i] = (WlanHostedNetworkPeerState)Marshal.PtrToStructure(new IntPtr(peersIterator), typeof(WlanHostedNetworkPeerState));
                peersIterator += Marshal.SizeOf(typeof(WlanHostedNetworkPeerState));
            }
            return status;
        }

        private void SetProperty(WlanHostedNetworkOpcode opCode, ValueType value) {
            IntPtr data;
            int dataSize = Marshal.SizeOf(value);
            data = Marshal.AllocHGlobal(dataSize);
            switch (opCode) {
                case WlanHostedNetworkOpcode.ConnectionSettings:
                    Marshal.StructureToPtr(value, data, false);
                    dataSize = Marshal.SizeOf(value);
                    break;
                case WlanHostedNetworkOpcode.Enable:
                    Marshal.WriteInt32(data, Convert.ToInt32(value));
                    dataSize = sizeof(int);
                    break;
            }
            try {
                WlanOpcodeValueType opcodeValueType;
                Util.ThrowIfError(
                    NativeMethods.WlanHostedNetworkSetProperty(client.clientHandle, opCode, dataSize, data, out opcodeValueType, IntPtr.Zero));
            } finally {
                Marshal.FreeHGlobal(data);
            }
        }

        private Object QueryProperty(WlanHostedNetworkOpcode opCode) {
            IntPtr data;
            uint dataSize;
            WlanOpcodeValueType opcodeValueType;
            Util.ThrowIfError(
                NativeMethods.WlanHostedNetworkQueryProperty(client.clientHandle, opCode, out dataSize, out data, out opcodeValueType, IntPtr.Zero));
            try {
                switch (opCode) {
                    case WlanHostedNetworkOpcode.ConnectionSettings:
                        return Marshal.PtrToStructure(data, typeof(WlanHostedNetworkConnectionSettings));
                    case WlanHostedNetworkOpcode.SecuritySettings:
                        return Marshal.PtrToStructure(data, typeof(WlanHostedNetworkSecuritySettings));
                    case WlanHostedNetworkOpcode.StationProfile:
                        return Marshal.PtrToStringUni(data);//PWSTR
                    case WlanHostedNetworkOpcode.Enable:
                        return Convert.ToBoolean(Marshal.ReadByte(data));
                    default:
                        return null;
                }
            } finally {
                NativeMethods.WlanFreeMemory(data);
            }
        }

        private Key QuerySecondaryKey() {
            uint keyLength;
            IntPtr keyData;
            bool isPassPhrase;
            bool persistent;
            WlanHostedNetworkReason reasonCode;
            Util.ThrowIfError(
                NativeMethods.WlanHostedNetworkQuerySecondaryKey(client.clientHandle, out keyLength, out keyData, out isPassPhrase, out persistent, out reasonCode, IntPtr.Zero));
            try {
                Key key;
                if (isPassPhrase) {
                    key = new Key(Marshal.PtrToStringAnsi(keyData), persistent);
                } else {
                    key = new Key(Marshal.ReadInt32(keyData), persistent);
                }
                return key;
            } finally {
                NativeMethods.WlanFreeMemory(keyData);
            }
        }

        private WlanHostedNetworkReason SetSecondaryKey(Key key) {
            IntPtr keyDataPtr;
            uint size;
            if (key.IsPassPhrase) {
                keyDataPtr = Marshal.StringToHGlobalAnsi((String)key.Data);
                size = (uint)key.Length + 1;
            } else {
                keyDataPtr = Marshal.AllocHGlobal(sizeof(int));
                Marshal.WriteInt32(keyDataPtr, (int)key.Data);
                size = sizeof(int);
            }
            WlanHostedNetworkReason failReason;
            try {
                Util.ThrowIfError(
                    NativeMethods.WlanHostedNetworkSetSecondaryKey(client.clientHandle, size, keyDataPtr, key.IsPassPhrase, key.Persistent, out failReason, IntPtr.Zero));
            } finally {
                Marshal.FreeHGlobal(keyDataPtr);
            }
            return failReason;
        }

        /// <summary>
        /// Class represents the whole information about a user security key for hosted network.
        /// </summary>
        public sealed class Key {

            /// <summary>
            /// Gets or sets the user key. By doing so changes phy of <see cref="IsPassPhrase"/> depending on whether the parameter is of type String or int.
            /// </summary>
            public Object Data { get; private set; }

            /// <summary>
            /// Gets whether the stored key is a passphrase (of type String) or a binary key (type int).
            /// </summary>
            public bool IsPassPhrase { get; private set; }

            /// <summary>
            /// Gets or sets whether the key is to be preserved over sessions.
            /// </summary>
            public bool Persistent { get; set; }

            /// <summary>
            /// Gets length of the key. If key is binary, returns always <code>32</code>.
            /// </summary>
            public int Length {get {if (IsPassPhrase) { return ((String)Data).Length; } else { return sizeof(int); }}}

            /// <summary>
            /// Creates a new representation of hosted network secondary key.
            /// </summary>
            /// <param name="key">Ansi string representing the key.</param>
            /// <param name="persistent">Whether key is persistent between hosted network sessions.</param>
            public Key(String key, bool persistent = true) {
                Data = key;
                IsPassPhrase = true;
                Persistent = persistent;
            }

            /// <summary>
            /// Creates a new representation of hosted network secondary key.
            /// </summary>
            /// <param name="key">Integer representing the key.</param>
            /// <param name="persistent">Whether key is persistent between hosted network sessions.</param>
            public Key(int key, bool persistent = true) {
                Data = key;
                IsPassPhrase = false;
                Persistent = persistent;
            }

            public override bool Equals(Object obj) {
                if (obj == null) return false;
                if (!this.GetType().IsAssignableFrom(obj.GetType())) return false;
                Key sk = (Key)obj;
                if (this.Data != sk.Data) return false;
                if (this.Length != sk.Length) return false;
                //if (this.IsPassPhrase != sk.IsPassPhrase) return false;
                if (this.Persistent != sk.Persistent) return false;
                return true;
            }

            public override int GetHashCode() {
                return this.Data.GetHashCode() ^ this.Length.GetHashCode() ^ this.Persistent.GetHashCode();
            }
        }

        // EVENTS ===================================================================

        #region Hosted Network specific Events

        public event HnwkStateChangeEventHandler HnwkStateChange;
        public event HnwkPeerStateChangeEventHandler HnwkPeerStateChange;
        public event HnwkRadioStateChangeEventHandler HnwkRadioStateChange;

        internal void OnHnwkStateChange(HnwkStateChangeEventArgs stateChange) {
            if (HnwkStateChange != null)
                HnwkStateChange(this, stateChange);
        }

        internal void OnHnwkPeerStateChange(HnwkPeerStateChangeEventArgs peerStateChange) {
            if (HnwkPeerStateChange != null)
                HnwkPeerStateChange(this, peerStateChange);
        }

        internal void OnHnwkRadioStateChange(HnwkRadioStateChangeEventArgs radioStateChange) {
            if (HnwkRadioStateChange != null)
                HnwkRadioStateChange(this, radioStateChange);
        }

        #endregion Hosted Network specific Events
    }
}
