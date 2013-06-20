// Copyright (c) 2013 Eugen Pechanec

using System;
using System.Runtime.InteropServices;

namespace EugenPechanec.NativeWifi {
    // = ENUMS ===============================================================
    public enum OneXAuthIdentity {
        None,
        Machine,
        User,
        ExplicitUser,
        Guest,
        Invalid
    }
    public enum OneXAuthRestartReason {
        PeerInitiated,
        MsmInitiated,
        OneXHeldStateTimeout,
        OneXAuthTimeout,
        OneXConfigurationChanged,
        OneXUserChanged,
        QuarantineStateChanged,
        AltCredsTrial,
        Invalid
    }
    public enum OneXAuthStatus {
        NotStarted,
        InProgress,
        NoAuthenticatorFound,
        Success,
        Failure,
        Invalid
    }
    public enum OneXEapMethodBackendSupport {
        SupportUnknown,
        Supported,
        Unsupported
    }
    public enum OneXNotificationType {
        PublicNotificationBase = 0,
        ResultUpdate,
        AuthRestarted,
        EventInvalid,
        NumNotifications = EventInvalid
    }
    public enum OneXReasonCode {
        Success = 0,
        Start = 0x5000,
        UnableToIdentifyUser,
        IdentityNotFound,
        UiDisabled,
        UiFailure,
        EapFailureRecieved,
        AuthenticatorNoLongerPresent,
        NoResponseToIdentity,
        ProfileVersionNotSupported,
        ProfileInvalidLength,
        ProfileDisallowedEapType,
        ProfileInvalidEapTypeOrFlag,
        ProfileInvalidOneXFlags,
        ProfileInvalidTimerValue,
        ProfileInvalidSupplicantCode,
        ProfileInvalidAuthMode,
        ProfileInvaliedEapConnectionProperties,
        UiCancelled,
        ProfileInvalidExplicitCredentials,
        ProfileExpiredExplicitCredentials,
        UiNotPermitted
    }
    // = ADDITIONAL ELEMENTS =================================================
    public enum OneXSupplicantMode {
        OneXSupplicantModeInhibitTransmission,
        OneXSupplicantModeLearn,
        OneXSupplicantModeCompliant,
    }
    public enum OneXAuthMode {
        OneXAuthModeMachineOrUser,
        OneXAuthModeMachineOnly,
        OneXAuthModeUserOnly,
        OneXAuthModeGuest,
        OneXAuthModeUnspecified,
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct EapMethodType {
        public EapType EapType;
        public uint AuthorId;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct EapType {
        public byte Type;
        public uint VendorId;
        public uint VendorType;
    }
    // = STRUCTS =============================================================
    [StructLayout(LayoutKind.Sequential)]
    public struct OneXAuthParams {
        [MarshalAs(UnmanagedType.Bool)]
        public bool UpdatePending;
        public OneXVariableBlob OneXConnProfile;
        public OneXAuthIdentity AuthIdentity;
        public uint QuarantineState;
        // /<summary>
        // / fSessionId : 1
        // /fhUserToken : 1
        // /fOnexUserProfile : 1
        // /fIdentity : 1
        // /fUserName : 1
        // /</summary>
        private uint bitvector1;
        public uint HasDomain;
        public uint SessionId;
        public uint UserToken;
        public OneXVariableBlob OneXUserProfile;
        public OneXVariableBlob Identity;
        public OneXVariableBlob UserName;
        public OneXVariableBlob Domain;
        public uint HasSessionId {
            get {
                return ((uint)((this.bitvector1 & 1u)));
            }
            // set {
            //    this.bitvector1 = ((uint)((phy | this.bitvector1)));
            // }
        }
        public uint HasUserToken {
            get {
                return ((uint)(((this.bitvector1 & 2u)
                            / 2)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 2)
            //                | this.bitvector1)));
            // }
        }
        public uint HasOneXUserProfile {
            get {
                return ((uint)(((this.bitvector1 & 4u)
                            / 4)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 4)
            //                | this.bitvector1)));
            // }
        }
        public uint HasIdentity {
            get {
                return ((uint)(((this.bitvector1 & 8u)
                            / 8)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 8)
            //                | this.bitvector1)));
            // }
        }
        public uint HasUserName {
            get {
                return ((uint)(((this.bitvector1 & 16u)
                            / 16)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 16)
            //                | this.bitvector1)));
            // }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OneXConnectionProfile {
        public uint Version;
        public uint TotalLen;
        // /<summary>
        // /fOneXSupplicantFlags : 1
        // /fsupplicantMode : 1
        // /fauthMode : 1
        // /fHeldPeriod : 1
        // /fAuthPeriod : 1
        // /fStartPeriod : 1
        // /fMaxStart : 1
        // /fMaxAuthFailures : 1
        // /fNetworkAuthTimeout : 1
        // /fAllowLogonDialogs : 1
        // /fNetworkAuthWithUITimeout : 1
        // /fUserBasedVLan : 1
        // /</summary>
        private uint bitvector1;
        public uint OneXSupplicantFlags;
        public OneXSupplicantMode SupplicantMode;
        public OneXAuthMode AuthMode;
        public uint HeldPeriod;
        public uint AuthPeriod;
        public uint StartPeriod;
        public uint MaxStart;
        public uint MaxAuthFailures;
        public uint NetworkAuthTimeout;
        public uint NetworkAuthWithUITimeout;
        [MarshalAs(UnmanagedType.Bool)]
        public bool AllowLogonDialogs;
        [MarshalAs(UnmanagedType.Bool)]
        public bool UserBasedVLan;
        public uint HasOneXSupplicantFlags {
            get {
                return ((uint)((this.bitvector1 & 1u)));
            }
            // set {
            //    this.bitvector1 = ((uint)((phy | this.bitvector1)));
            // }
        }
        public uint HasSupplicantMode {
            get {
                return ((uint)(((this.bitvector1 & 2u)
                            / 2)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 2)
            //                | this.bitvector1)));
            // }
        }
        public uint HasAuthMode {
            get {
                return ((uint)(((this.bitvector1 & 4u)
                            / 4)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 4)
            //                | this.bitvector1)));
            // }
        }
        public uint HasHeldPeriod {
            get {
                return ((uint)(((this.bitvector1 & 8u)
                            / 8)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 8)
            //                | this.bitvector1)));
            // }
        }
        public uint HasAuthPeriod {
            get {
                return ((uint)(((this.bitvector1 & 16u)
                            / 16)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 16)
            //                | this.bitvector1)));
            // }
        }
        public uint HasStartPeriod {
            get {
                return ((uint)(((this.bitvector1 & 32u)
                            / 32)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 32)
            //                | this.bitvector1)));
            // }
        }
        public uint HasMaxStart {
            get {
                return ((uint)(((this.bitvector1 & 64u)
                            / 64)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 64)
            //                | this.bitvector1)));
            // }
        }
        public uint HasMaxAuthFailures {
            get {
                return ((uint)(((this.bitvector1 & 128u)
                            / 128)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 128)
            //                | this.bitvector1)));
            // }
        }
        public uint HasNetworkAuthTimeout {
            get {
                return ((uint)(((this.bitvector1 & 256u)
                            / 256)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 256)
            //                | this.bitvector1)));
            // }
        }
        public uint HasAllowLogonDialogs {
            get {
                return ((uint)(((this.bitvector1 & 512u)
                            / 512)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 512)
            //                | this.bitvector1)));
            // }
        }
        public uint HasNetworkAuthWithUITimeout {
            get {
                return ((uint)(((this.bitvector1 & 1024u)
                            / 1024)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 1024)
            //                | this.bitvector1)));
            // }
        }
        public uint HasUserBasedVLan {
            get {
                return ((uint)(((this.bitvector1 & 2048u)
                            / 2048)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 2048)
            //                | this.bitvector1)));
            // }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OneXEapError {
        public uint WinError;
        public EapMethodType Type;
        public uint ReasonCode;
        public Guid RootCauseGuid;
        public Guid RepairGuid;
        public Guid HelpLinkGuid;
        // /<summary>
        // / fRootCauseString : 1
        // /fRepairString : 1
        // /</summary>
        private uint bitvector1;
        public OneXVariableBlob RootCauseString;
        public OneXVariableBlob RepairString;
        public uint HasRootCauseString {
            get {
                return ((uint)((this.bitvector1 & 1u)));
            }
            // set {
            //    this.bitvector1 = ((uint)((phy | this.bitvector1)));
            // }
        }
        public uint HasRepairString {
            get {
                return ((uint)(((this.bitvector1 & 2u)
                            / 2)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 2)
            //                | this.bitvector1)));
            // }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OneXResultUpdateData {
        public OneXStatus OneXStatus;
        public OneXEapMethodBackendSupport BackendSupport;
        [MarshalAs(UnmanagedType.Bool)]
        public bool BackendEngaged;
        // / fOneXAuthParams : 1
        // /fEapError : 1
        public uint bitvector1;
        public OneXVariableBlob AuthParams;
        public OneXVariableBlob EapError;
        public uint HasOneXAuthParams {
            get {
                return ((uint)((this.bitvector1 & 1u)));
            }
            // set {
            //    this.bitvector1 = ((uint)((phy | this.bitvector1)));
            // }
        }
        public uint HasEapError {
            get {
                return ((uint)(((this.bitvector1 & 2u)
                            / 2)));
            }
            // set {
            //    this.bitvector1 = ((uint)(((phy * 2)
            //                | this.bitvector1)));
            // }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OneXStatus {
        public OneXAuthStatus AuthStatus;
        public uint Reason;
        public uint Error;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct OneXVariableBlob {
        public uint Size;
        public uint Offset;
    }

}
