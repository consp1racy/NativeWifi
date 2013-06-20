// Copyright (c) 2013 Eugen Pechanec

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;

namespace EugenPechanec.NativeWifi {
    //= BASIC =========================================================================================
    [StructLayout(LayoutKind.Sequential)]
    internal struct Dot11CountryOrRegionString {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public byte[] Value;

        public override String ToString() {
            return Encoding.Default.GetString(Value);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Dot11MacAddress {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] Value;

        public static Dot11MacAddress Wildcard {
            get {
                Dot11MacAddress wildcard = new Dot11MacAddress();
                wildcard.Value = new byte[6] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };
                return wildcard;
            }
        }

        public static PhysicalAddress ToPhysicalAddress(Dot11MacAddress mac) {
            return new PhysicalAddress(mac.Value);
        }

        public static Dot11MacAddress FromPhysicalAddress(PhysicalAddress phy) {
            Dot11MacAddress mac = new Dot11MacAddress();
            mac.Value = phy.GetAddressBytes();
            return mac;
        }
    }
    //= DOT11 =========================================================================================
    [StructLayout(LayoutKind.Sequential)]
    public struct Dot11AuthCipherPair {
        public Dot11AuthAlgorithm AuthAlgo;
        public Dot11CipherAlgorithm CipherAlgo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct Dot11BssidList { // always process and pass as IntPtr, never interop directly
        private NdisObjectHeader header;
        private Dot11BssidListHeader listHeader;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        private Dot11MacAddress[] bssids; //dynamic array Dot11MacAddress[]

        public static Dot11BssidList Build(Dot11MacAddress[] bssids) {
            Dot11BssidList list = new Dot11BssidList();
            int maxLength = CalculateMaxLength();
            list.listHeader.TotalNumOfEntries = (ushort)maxLength;
            list.header = NdisObjectHeader.CreateDefault();
            list.Entries = bssids;
            return list;
        }

        private static int CalculateSize(ref Dot11BssidList list) {
            int ndisHeaderSize = Marshal.SizeOf(typeof(NdisObjectHeader));
            int listHeaderSize = Marshal.SizeOf(typeof(Dot11BssidListHeader));
            int macAddressSize = Marshal.SizeOf(typeof(Dot11MacAddress));
            return ndisHeaderSize + listHeaderSize + list.bssids.Length * macAddressSize;
        }

        private static int CalculateMaxLength() {
            int maxSize = ushort.MaxValue;
            int ndisHeaderSize = Marshal.SizeOf(typeof(NdisObjectHeader));
            int listHeaderSize = Marshal.SizeOf(typeof(Dot11BssidListHeader));
            int macAddressSize = Marshal.SizeOf(typeof(Dot11MacAddress));
            int availableSpace = maxSize - ndisHeaderSize - listHeaderSize;
            return availableSpace / macAddressSize;
        }

        public NdisObjectHeader Header {
            get {
                return header;
            }
        }
        public Dot11BssidListHeader ListHeader {
            get {
                return listHeader;
            }
        }
        public Dot11MacAddress[] Entries {
            get {
                return bssids;
            }
            set {
                if (value == null) throw new ArgumentException();
                if (value.Length > listHeader.TotalNumOfEntries) throw new ArgumentException();
                listHeader.NumOfEntries = (uint)value.Length;
                bssids = value;
                int size = CalculateSize(ref this);
                header.Size = (ushort)size;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct Dot11BssidListHeader {
        public uint NumOfEntries; //dataSize of dynamic array
        public uint TotalNumOfEntries;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Dot11Network {
        public Dot11Ssid Ssid;
        public Dot11BssType BssType;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct Dot11NetworkList {
        public uint NumberOfItems; //dataSize of dynamic array
        public uint Index;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr NetworkList; //dynamic array Dot11NetworkList[]
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Dot11Ssid {
        private int ssidLength; //uint
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
        private byte[] ssid;

        public string Ssid {
            get {
                return Encoding.Default.GetString(ssid, 0, ssidLength);
            }
            set {
                int length = value.Length > 31 ? 32 : value.Length;
                ssid = new byte[32];
                Array.Copy(Encoding.Default.GetBytes(value.ToCharArray(), 0, length), ssid, length);
                //TODO check last \0
                ssidLength = length;
            }
        }

        public Dot11Ssid(String SSID) : this() {
            this.Ssid = SSID;
        }

        public byte[] ToByteArray() {
            byte[] array = new byte[ssidLength];
            Array.Copy(ssid, array, ssidLength);
            return array;
        }

        public string ToHex() {
            return Util.ToHex(ssid);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct NdisObjectHeader {
        public byte Type;
        public byte Revision;
        public ushort Size;

        public static NdisObjectHeader CreateDefault() {
            NdisObjectHeader header = new NdisObjectHeader();
            header.Type = 0x80;
            header.Revision = 1;
            header.Size = (ushort)Marshal.SizeOf(header);
            return header;
        }
    }
    //= WLAN ==========================================================================================
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanAssociationAttributes {
        public Dot11Ssid Ssid;
        public Dot11BssType BssType;
        public Dot11MacAddress MacAddress;
        public Dot11PhyType PhyType;
        public uint PhyIndex;
        public uint LanSignalQuality;
        public uint RxRate;
        public uint TxRate;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanAuthCipherPairList {
        public uint NumberOfItems; //dataSize of dynamic array
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr AuthCipherPairList; //dynamic array Dot11AuthCipherPair[]
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanAvailableNetwork {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ProfileName;
        public Dot11Ssid Ssid;
        public Dot11BssType BssType;
        public uint NumberOfBssids;
        public bool NetworkConnectable;
        public WlanReasonCode NotConnectableReason;
        private uint numberOfPhyTypes;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        private Dot11PhyType[] dot11PhyTypes;
        public Dot11PhyType[] Dot11PhyTypes {
            get {
                Dot11PhyType[] array = new Dot11PhyType[numberOfPhyTypes];
                Array.Copy(dot11PhyTypes, array, numberOfPhyTypes);
                return array;
            }
            set {
                if (value.Length > 8) throw new ArgumentException();
                dot11PhyTypes = new Dot11PhyType[8];
                Array.Copy(value, dot11PhyTypes, value.Length);
                numberOfPhyTypes = (uint) value.Length;
            }
        }
        [MarshalAs(UnmanagedType.Bool)]
        public bool MorePhyTypes;
        public uint SignalQuality;
        [MarshalAs(UnmanagedType.Bool)]
        public bool SecurityEnabled;
        public Dot11AuthAlgorithm DefaultAuthAlgo;
        public Dot11CipherAlgorithm DefaultCipherAlgo;
        public WlanAvailableNetworkFlags Flags;
        private uint reserved;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanAvailableNetworkList {
        public uint NumberOfItems; //dataSize of dynamic array
        public uint Index;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr Network; //dynamic array WlanAvailableNetwork[]
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanBssEntry {
        public Dot11Ssid Ssid;
        public uint PhyId;
        public Dot11MacAddress MacAddress;
        public Dot11BssType BssType;
        public Dot11PhyType PhyType;
        public int Rssi;
        public uint LinkQuality;
        public byte InRegDomain;
        public ushort BeaconPeriod;
        public ulong Timestamp;
        public ulong HostTimestamp;
        public ushort CapabilityInformation;
        public uint ChCenterFrequency;
        public WlanRateSet RateSet;
        public uint IeOffset;
        public uint IeSize;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanBssList {
        public uint TotalSize;
        public uint NumberOfItems;//dataSize of dynamic array
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr BssEntries;//dynamic array WlanBssEntry[]
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanConnectionAttributes {
        public WlanInterfaceState InterfaceState;
        public WlanConnectionMode ConnectionMode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ProfileName;
        public WlanAssociationAttributes AssociationAttributes;
        public WlanSecurityAttributes SecurityAttributes;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanConnectionNotificationData {
        public WlanConnectionMode ConnectionMode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ProfileName;
        public Dot11Ssid Ssid;
        public Dot11BssType BssType;
        [MarshalAs(UnmanagedType.Bool)]
        public bool SecurityEnabled;
        public WlanReasonCode ReasonCode;
        public WlanConnectionNotificationFlags Flags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        public string ProfileXml; //null-terminated wide character string
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanConnectionParameters : IDisposable {
        public WlanConnectionMode ConnectionMode;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Profile;
        internal IntPtr Ssid;
        internal IntPtr DesiredBssidList;
        //public Dot11Ssid? Ssid;
        //public Dot11BssidList? DesiredBssidList;
        public Dot11BssType BssType;
        public WlanConnectionFlags Flags;

        public void Dispose() {
            if (Ssid != IntPtr.Zero) {
                Marshal.FreeHGlobal(Ssid);
                Ssid = IntPtr.Zero;
            }
            if (DesiredBssidList != IntPtr.Zero) {
                Marshal.FreeHGlobal(DesiredBssidList);
                DesiredBssidList = IntPtr.Zero;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanCountryOrRegionStringList {
        public uint NumberOfItems;//dataSize of dynamic array
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr CountryOrRegionStringList;//dynamic array Dot11CountryOrRegionString[]
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanInterfaceCapability {
        public WlanInterfaceType Type;
        [MarshalAs(UnmanagedType.Bool)]
        public bool DSupported;
        public uint MaxDesiredSsidListSize;
        public uint MaxDesiredBssidListSize;
        private uint numberOfSupportedPhys;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private Dot11PhyType[] phyTypes;

        public Dot11PhyType[] PhyTypes {
            get {
                Dot11PhyType[] array = new Dot11PhyType[numberOfSupportedPhys];
                Array.Copy(phyTypes, array, numberOfSupportedPhys);
                return array;
            }
            set {
                if (value.Length > 64) throw new ArgumentException();
                phyTypes = new Dot11PhyType[64];
                Array.Copy(value, phyTypes, value.Length);
                numberOfSupportedPhys = (uint)value.Length;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanInterfaceInfo {
        public Guid Guid;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string Description;
        public WlanInterfaceState State;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanInterfaceInfoList {
        public uint NumberOfItems;//dataSize of dynamic array
        public uint Index;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr InterfaceInfo;//dynamic array WlanInterfaceInfo[]
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanMacFrameStatistics {
        public ulong TransmittedFrameCount;
        public ulong ReceivedFrameCount;
        public ulong WEPExcludedCount;
        public ulong TKIPLocalMICFailures;
        public ulong TKIPReplays;
        public ulong TKIPICVErrorCount;
        public ulong CCMPReplays;
        public ulong CCMPDecryptErrors;
        public ulong WEPUndecryptableCount;
        public ulong WEPICVErrorCount;
        public ulong DecryptSuccessCount;
        public ulong DecryptFailureCount;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanMsmNotificationData {
        public WlanConnectionMode ConnectionMode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ProfileName;
        public Dot11Ssid SSID;
        public Dot11BssType BssType;
        public Dot11MacAddress MacAddress;
        [MarshalAs(UnmanagedType.Bool)]
        public bool SecurityEnabled;
        [MarshalAs(UnmanagedType.Bool)]
        public bool FirstPeer;
        [MarshalAs(UnmanagedType.Bool)]
        public bool LastPeer;
        public WlanReasonCode ReasonCode;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanNotificationData {
        public WlanNotificationSource NotificationSource;
        private uint notificationCode;
        public Guid InterfaceGuid;
        internal uint DataSize;
        internal IntPtr Data;

        public object NotificationCode {
            get {
                switch (NotificationSource) {
                    case WlanNotificationSource.Msm:
                        return (WlanMsmNotificationCode)notificationCode;
                    case WlanNotificationSource.Acm:
                        return (WlanAcmNotificationCode)notificationCode;
                    case WlanNotificationSource.HostedNetwork:
                        return (WlanHostedNetworkNotificationCode)notificationCode;
                    case WlanNotificationSource.OneX:
                        return (OneXNotificationType)notificationCode;
                    default:
                        return notificationCode;
                }
            }
        }
        //NOTE Data is internally accessible. Means it must be parsed probably by WlanClient class.
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanPhyFrameStatistics {
        public ulong TransmittedFrameCount;
        public ulong MulticastTransmittedFrameCount;
        public ulong FailedCount;
        public ulong RetryCount;
        public ulong MultipleRetryCount;
        public ulong MaxTXLifetimeExceededCount;
        public ulong TransmittedFragmentCount;
        public ulong RTSSuccessCount;
        public ulong RTSFailureCount;
        public ulong ACKFailureCount;
        public ulong ReceivedFrameCount;
        public ulong MulticastReceivedFrameCount;
        public ulong PromiscuousReceivedFrameCount;
        public ulong MaxRXLifetimeExceededCount;
        public ulong FrameDuplicateCount;
        public ulong ReceivedFragmentCount;
        public ulong PromiscuousReceivedFragmentCount;
        public ulong FCSErrorCount;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanPhyRadioState {
        public uint PhyIndex;
        public Dot11RadioState SoftwareRadioState;
        public Dot11RadioState HardwareRadioState;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WlanProfileInfo {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string ProfileName;
        public WlanProfileFlags Flags;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanProfileInfoList {
        public uint NumberOfItems;//dataSize of dynamic array
        public uint Index;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr ProfileInfo;//dynamic array WlanProfileInfo[]
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanRadioState {
        private uint numberOfPhys;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        private WlanPhyRadioState[] phyRadioState;

        public WlanPhyRadioState[] PhyRadioState {
            get {
                WlanPhyRadioState[] array = new WlanPhyRadioState[numberOfPhys];
                Array.Copy(phyRadioState, array, numberOfPhys);
                return array;
            }
            set {
                if (value.Length > 64) throw new ArgumentException();
                phyRadioState = new WlanPhyRadioState[64];
                Array.Copy(value, phyRadioState, value.Length);
                numberOfPhys = (uint)value.Length;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanRateSet {
        private uint rateSetLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 126)]
        private ushort[] rateSet;

        public ushort[] RateSet {
            get {
                ushort[] array = new ushort[rateSetLength];
                Array.Copy(rateSet, array, rateSetLength);
                return array;
            }
            set {
                if (value.Length > 64) throw new ArgumentException();
                rateSet = new ushort[126];
                Array.Copy(value, rateSet, value.Length);
                rateSetLength = (uint)value.Length;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanRawData {
        internal uint DataSize;//dataSize of dynamic array
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr DataBlob;//dynamic array byte[]
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanRawDataHeader {
        public uint DataOffset;
        public uint DataSize;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanRawDataListHeader { // originally WlanRawDataList
        public uint TotalSize;
        public uint NumberOfItems;
        // here follows dwNumberOfItems of type WlanRawDataHeader in memory.
        // here follows dwNumberOfItems of type WlanRawData in memory.
    }
    [StructLayout(LayoutKind.Sequential)] 
    internal struct WlanRawDataList {// always process and pass as IntPtr, never interop directly
        public WlanRawDataListHeader ListHeader;
        public WlanRawDataHeader[] DataHeaders;
        public WlanRawData[] Data;

        public uint TotalSize {
            get {
                return ListHeader.TotalSize;
            }
        }

        public uint NumberOfItems {
            get {
                return ListHeader.NumberOfItems;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanSecurityAttributes {
        [MarshalAs(UnmanagedType.Bool)]
        public bool SecurityEnabled;
        [MarshalAs(UnmanagedType.Bool)]
        public bool OneXEnabled;
        public Dot11AuthAlgorithm AuthAlgo;
        public Dot11CipherAlgorithm CipherAlgo;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanStatistics {
        public ulong FourWayHandshakeFailures;
        public ulong TKIPCounterMeasuresInvoked;
        public ulong Reserved;
        public WlanMacFrameStatistics MacUcastCounters;
        public WlanMacFrameStatistics MacMcastCounters;
        public uint NumberOfPhys;//dataSize of dynamic array
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr PhyCounters;//dynamic array WlanPhyFrameStatistics[]
    }
    //= HOSTED NETWORK ================================================================================

    [StructLayout(LayoutKind.Sequential)]
    public struct WlanHostedNetworkConnectionSettings {
        public Dot11Ssid Ssid;
        public uint MaxNumberOfPeers;

        public WlanHostedNetworkConnectionSettings(Dot11Ssid SSID, uint MaxNumberOfPeers) {
            this.Ssid = SSID;
            this.MaxNumberOfPeers = MaxNumberOfPeers;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanHostedNetworkDataPeerStateChange {
        public WlanHostedNetworkPeerState OldState;
        public WlanHostedNetworkPeerState NewState;
        public WlanHostedNetworkReason StateChangeReason;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanHostedNetworkPeerState {
        public Dot11MacAddress macAddress;
        public WlanHostedNetworkPeerAuthState AuthState;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanHostedNetworkRadioState {
        public Dot11RadioState SoftwareRadioState;
        public Dot11RadioState HardwareRadioState;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanHostedNetworkSecuritySettings {
        public Dot11AuthAlgorithm AuthAlgo;
        public Dot11CipherAlgorithm CipherAlgo;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct WlanHostedNetworkStateChange {
        public WlanHostedNetworkState OldState;
        public WlanHostedNetworkState NewState;
        public WlanHostedNetworkReason StateChangeReason;
    }
    [StructLayout(LayoutKind.Sequential)]
    internal struct WlanHostedNetworkStatus {
        public WlanHostedNetworkState State;
        public Guid IPDeviceID;
        public Dot11MacAddress MacAddress;
        public Dot11PhyType PhyType;
        public uint ChannelFrequency;
        public uint NumberOfPeers;//dataSize of dynamic array
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public IntPtr PeerList;//dynamic array WlanHostedNetworkPeerState[]
    }

}
