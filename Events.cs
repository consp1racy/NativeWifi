// Copyright (c) 2013 Eugen Pechanec

using System;
using System.Security.Permissions;

namespace EugenPechanec.NativeWifi {

    public delegate void WlanEventHandler(object sender, WlanEventArgs e);

    public delegate void OneXEventHandler(object sender, OneXEventArgs e);
    public delegate void OneXResultUpdateEventHandler(object sender, OneXEventArgs e);
    public delegate void OneXAuthRestartedEventHandler(object sender, OneXEventArgs e);

    public delegate void AcmEventHandler(object sender, AcmEventArgs e);
    public delegate void AcmBssTypeChangeEventHandler(object sender, AcmBssTypeChangeEventArgs e);
    public delegate void AcmPowerSettingChangeEventHandler(object sender, AcmPowerSettingChangeEventArgs e);
    public delegate void AcmReasonCodeEventHandler(object sender, AcmReasonCodeEventArgs e);
    public delegate void AcmConnectionEventHandler(object sender, AcmConnectionEventArgs e);
    public delegate void AcmProfileNameChangeEventHandler(object sender, AcmProfileNameChangeEventArgs e);
    public delegate void AcmBooleanEventHandler(object sender, AcmBooleanEventArgs e);
    public delegate void AcmAdhocNetworkStateChange(object sender, AcmAdhocNetworkStateChangeEventArgs e);

    public delegate void MsmEventHandler(object sender, MsmEventArgs e);
    public delegate void MsmNotificationEventHandler(object sender, MsmNotificationEventArgs e);
    public delegate void MsmDwordEventHandler(object sender, MsmDwordEventArgs e);
    public delegate void MsmRadioStateChangeEventHandler(object sender, MsmRadioStateChangeEventArgs e);

    public delegate void SecurityEventHandler(object sender, SecurityEventArgs e);

    public delegate void IhvEventHandler(object sender, IhvEventArgs e);

    public delegate void HnwkEventHandler(object sender, HnwkEventArgs e);
    public delegate void HnwkStateChangeEventHandler(object sender, HnwkStateChangeEventArgs e);
    public delegate void HnwkPeerStateChangeEventHandler(object sender, HnwkPeerStateChangeEventArgs e);
    public delegate void HnwkRadioStateChangeEventHandler(object sender, HnwkRadioStateChangeEventArgs e);


    public class WlanEventArgs : EventArgs {
        private static WlanEventArgs empty = new WlanEventArgs();

        public static new WlanEventArgs Empty {
            get {
                return empty;
            }
        }

        public Guid InterfaceGuid { get; set; }
        public WlanNotificationSource NotificationSource { get; set; }
        public object NotificationCode { get; set; }

        protected WlanEventArgs() { }

        protected WlanEventArgs(WlanNotificationSource source) {
            NotificationSource = source;
        }
    }

    #region OneX Notifications

    public class OneXEventArgs : WlanEventArgs {
        public OneXEventArgs() : base(WlanNotificationSource.OneX) { }
    }

    public class OneXResultUpdateEventArgs : OneXEventArgs {
        public OneXResultUpdateData ResultUpdate { get; set; }
    }

    public class OneXAuthRestartedEventArgs : OneXEventArgs {
        public OneXAuthRestartReason AuthRestartReason { get; set; }
    }

    #endregion OneX Notifications

    #region Acm Notifications

    public class AcmEventArgs : WlanEventArgs {
        public AcmEventArgs() : base(WlanNotificationSource.Acm) { }
    }

    public class AcmBssTypeChangeEventArgs : AcmEventArgs {
        public Dot11BssType BssType { get; set; }
    }

    public class AcmPowerSettingChangeEventArgs : AcmEventArgs {
        public WlanPowerSetting PowerSetting { get; set; }
    }

    public class AcmReasonCodeEventArgs : AcmEventArgs {
        public WlanReasonCode ReasonCode { get; set; }
    }

    public class AcmConnectionEventArgs : AcmEventArgs {
        public WlanConnectionNotificationData ConnectionData { get; set; }
    }

    public class AcmProfileNameChangeEventArgs : AcmEventArgs {
        public String OldName { get; set; }
        public String NewName { get; set; }
    }

    public class AcmBooleanEventArgs : AcmEventArgs {
        public bool Value { get; set; }
    }

    public class AcmAdhocNetworkStateChangeEventArgs : AcmEventArgs {
        public WlanAdhocNetworkState AdhocNetowrkState { get; set; }
    }

    #endregion Acm Notifications

    #region Msm Notifications

    public class MsmEventArgs : WlanEventArgs {
        public MsmEventArgs() : base(WlanNotificationSource.Msm) { }
    }

    public class MsmNotificationEventArgs : MsmEventArgs {
        public WlanMsmNotificationData NotificationData { get; set; }
    }

    public class MsmDwordEventArgs : MsmEventArgs {
        public uint Value { get; set; }
    }

    public class MsmRadioStateChangeEventArgs : MsmEventArgs {
        public WlanPhyRadioState RadioState { get; set; }
    }

    public class MsmOperationModeChange : MsmEventArgs {
        public Dot11OperationMode OperationMode { get; set; }
    }

    #endregion Msm Notifications

    #region Security Notifications

    public class SecurityEventArgs : WlanEventArgs {
        public SecurityEventArgs() : base(WlanNotificationSource.Security) { }
    }

    #endregion Security Notifications

    #region Ihv Notifications

    public class IhvEventArgs : WlanEventArgs {
        internal IntPtr Data { get; set; }
        internal uint DataSize { get; set; }

        public IhvEventArgs() : base(WlanNotificationSource.Ihv) { }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public T InterpretStruct<T>() where T : struct {
            return Util.ParseStruct<T>(Data, DataSize);
        }
    }

    #endregion Ihv Notifications

    #region Hosted Network Notifications

    public class HnwkEventArgs : WlanEventArgs {
        public HnwkEventArgs() : base (WlanNotificationSource.HostedNetwork) { }
    }

    public class HnwkPeerStateChangeEventArgs : HnwkEventArgs {
        public WlanHostedNetworkDataPeerStateChange PeerState { get; set; }
    }

    public class HnwkRadioStateChangeEventArgs : HnwkEventArgs {
        public WlanHostedNetworkRadioState RadioState { get; set; }
    }

    public class HnwkStateChangeEventArgs : HnwkEventArgs {
        public WlanHostedNetworkStateChange State { get; set; }
    }

    #endregion Hosted Network Notifications
}
