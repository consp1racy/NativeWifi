using System;

namespace EugenPechanec.NativeWifi {
    [Flags]
    public enum Dot11OperationMode : uint {
        Unknown = 0x00000000,
        Station = 0x00000001,
        Ap = 0x00000002,
        ExtensibleStation = 0x00000004,
        NetworkMonitor = 0x80000000
    }
    [Flags]
    public enum WlanGetAvailableNetworkFlags {
        IncludeAllAdhocProfiles = 0x00000001,
        IncludeAllManualHiddenProfiles = 0x00000002
    }
    [Flags]
    public enum WlanAvailableNetworkFlags {
        Connected = 0x00000001,
        HasProfile = 0x00000002
    }
    [Flags]
    public enum WlanProfileFlags {
        AllUser = 0,
        GroupPolicy = 1,
        User = 2
    }
    [Flags]
    public enum WlanAccess {
        ReadAccess = 0x00020000 | 0x0001,
        ExecuteAccess = ReadAccess | 0x0020,
        WriteAccess = ReadAccess | ExecuteAccess | 0x0002 | 0x00010000 | 0x00040000
    }
    [Flags]
    public enum WlanNotificationSource : uint {
        None = 0,
        OneX = 0X00000004,
        Acm = 0X00000008,
        Msm = 0X00000010,
        Security = 0X00000020,
        Ihv = 0X00000040,
        HostedNetwork = 0X00000080,
        All = 0X0000FFFF
    }
    [Flags]
    public enum WlanConnectionFlags {
        HiddenNetwork = 0x00000001,
        AdhocJoinOnly = 0x00000002,
        IgnorePrivacyBit = 0x00000004,
        EapolPassthrough = 0x00000008
    }
    [Flags]
    public enum WlanConnectionNotificationFlags {
        AdhocNetworkFormed = 0x00000001,
        ConsoleUserProfile = 0x00000004
    }
}
