using System;

namespace EugenPechanec.NativeWifi {
    /// <summary>
    /// The DOT11_AUTH_ALGORITHM enumerated type defines a wireless LAN authentication algorithm.
    /// </summary>
    public enum Dot11AuthAlgorithm : uint {
        Open = 1,
        SharedKey = 2,
        Wpa = 3,
        WpaPsk = 4,
        WpaNone = 5,
        Rsna = 6,
        RsnaPsk = 7,
        IhvStart = 0x80000000,
        IhvEnd = 0xffffffff
    }
    /// <summary>
    /// The DOT11_BSS_TYPE enumerated type defines a basic service set (BSS) network type.
    /// </summary>
    public enum Dot11BssType {
        Infrastructure = 1,
        Independent = 2,
        Any = 3
    }
    public enum Dot11CipherAlgorithm : uint {
        None = 0x00,
        Wep40 = 0x01,
        Tkip = 0x02,
        Ccmp = 0x04,
        Wep104 = 0x05,
        WpaUseGroup = 0x100,
        RsnUseGroup = 0x100,
        Wep = 0x101,
        IhvStart = 0x80000000,
        IhvEnd = 0xffffffff
    }
    public enum Dot11PhyType : uint {
        Unknown = 0,
        Any = 0,
        Fhss = 1,
        Dsss = 2,
        IrBaseBand = 3,
        Ofdm = 4,
        Hrdsss = 5,
        Erp = 6,
        Ht = 7,
        IhvStart = 0x80000000,
        IhvEnd = 0xffffffff
    }
    public enum Dot11RadioState {
        Unknown,
        On,
        Off
    }
    public enum WlDisplayPages {
        WlConnectionPage,
        WlSecurityPage
    }
    public enum WlanAdhocNetworkState {
        Formed = 0,
        Connected = 1
    }
    public enum WlanAutoConfOpcode {
        //Start = 0,
        ShowDeniedNetworks = 1,
        PowerSetting = 2,
        OnlyUseGroupProfilesForAllowedNetworks = 3,
        AllowExplicitCredentials = 4,
        BlockPeriod = 5,
        AllowVirtualStationExtensibility = 6,
        //End = 7
    }
    public enum WlanConnectionMode {
        Profile,
        TemporaryProfile,
        DiscoverySecure,
        DiscoveryUnsecure,
        Auto,
        Invalid
    }
    public enum WlanFilterListType {
        GpPermit,
        GpDeny,
        UserPermit,
        UserDeny
    }
    public enum WlanHostedNetworkNotificationCode {
        StateChange = 0x00001000,
        PeerStateChange,
        RadioStateChange
    }
    public enum WlanHostedNetworkOpcode {
        ConnectionSettings,
        SecuritySettings,
        StationProfile,
        Enable
    }
    public enum WlanHostedNetworkPeerAuthState {
        Invalid,
        Authenticated
    }
    public enum WlanHostedNetworkReason {
        Success = 0,
        Unspecified,
        BadParameters,
        ServiceShuttingDown,
        InsufficientResources,
        ElevationRequired,
        ReadOnly,
        PersistenceFailed,
        CryptError,
        Impersonation,
        StopBeforeStart,
        InterfaceAvailable,
        InterfaceUnavailable,
        MiniportStopped,
        MiniportStarted,
        IncompatibleConnectionStarted,
        IncompatibleConnectionStopped,
        UserAction,
        ClientAbort,
        ApStartFailed,
        PeerArrived,
        PeerDeparted,
        PeerTimeout,
        GpDenied,
        ServiceUnavailable,
        DeviceChange,
        PropertiesChange,
        VirtualStationBlockingUse,
        ServiceAvailableOnVirtualStation
    }
    public enum WlanHostedNetworkState {
        Unavailable,
        Idle,
        Active
    }
    public enum WlanIhvControlType {
        Service,
        Driver
    }
    public enum WlanInterfaceState {
        NotReady = 0,
        Connected = 1,
        AdhocNetworkFormed = 2,
        Disconnecting = 3,
        Disconnected = 4,
        Associating = 5,
        Discovering = 6,
        Authenticating = 7
    }
    public enum WlanInterfaceType {
        Emulated80211 = 0,
        Native80211,
        Invalid
    }
    public enum WlanIntfOpcode {
        AutoconfStart = 0x000000000,
        AutoconfEnabled,
        BackgroundScanEnabled,
        MediaStreamingMode,
        RadioState,
        BssType,
        InterfaceState,
        CurrentConnection,
        ChannelNumber,
        SupportedInfrastructureAuthCipherPairs,
        SupportedAdhocAuthCipherPairs,
        SupportedCountryOrRegionStringList,
        CurrentOperationMode,
        SupportedSafeMode,
        CertifiedSafeMode,
        HostedNetworkCapable,
        ManagementFrameProtectionCapable,
        AutoconfEnd = 0x0fffffff,
        MsmStart = 0x10000100,
        Statistics,
        Rssi,
        MsmEnd = 0x1fffffff,
        SecurityStart = 0x20010000,
        SecurityEnd = 0x2fffffff,
        IhvStart = 0x30000000,
        IhvEnd = 0x3fffffff
    }
    public enum WlanAcmNotificationCode {
        Start = 0,
        AutoconfEnabled,
        AutoconfDisabled,
        BackgroundScanEnabled,
        BackgroundScanDisabled,
        BssTypeChange,
        PowerSettingChange,
        ScanComplete,
        ScanFail,
        ConnectionStart,
        ConnectionComplete,
        ConnectionAttemptFail,
        FilterListChange,
        InterfaceArrival,
        InterfaceRemoval,
        ProfileChange,
        ProfileNameChange,
        ProfilesExhausted,
        NetworkNotAvailable,
        NetworkAvailable,
        Disconnecting,
        Disconnected,
        AdhocNetworkStateChange,
        ProfileUnblocked,
        ScreenPowerChange,
        ProfileBlocked,
        ScanListRefresh,
        End
    }
    public enum WlanMsmNotificationCode {
        Start = 0,
        Associating,
        Associated,
        Authenticating,
        Connected,
        RoamingStart,
        RoamingEnd,
        RadioStateChange,
        SignalQualityChange,
        Disassociating,
        Disconnected,
        PeerJoin,
        PeerLeave,
        AdapterRemoval,
        AdapterOperationModeChange,
        End
    }
    public enum WlanOpcodeValueType {
        QueryOnly = 0,
        SetByGroupPolicy = 1,
        SetByUser = 2,
        Invalid = 3
    }
    public enum WlanPowerSetting {
        NoSaving,
        LowSaving,
        MediumSaving,
        MaximumSaving,
        Invalid
    }
    public enum WlanSecurableObject {
        PermitList = 0,
        DenyList = 1,
        AcEnabled = 2,
        BcScanEnabled = 3,
        BssType = 4,
        ShowDenied = 5,
        InterfaceProperties = 6,
        IhvControl = 7,
        AllUserProfilesOrder = 8,
        AddNewAllUserProfiles = 9,
        AddNewPerUserProfiles = 10,
        MediaStreamingModeEnabled = 11,
        CurrentOperationMode = 12,
        GetPlaintextKey = 13,
        HostedNetworkElevatedAccess = 14,
        VirtualStationExtensibility = 15,
        WfdElevatedAccess = 16
    }
    public enum WlanReasonCode : uint {
        RangeSize = 0x10000,
        Base = 0x10000,
        Success = 0,
        Unknown = Base + 1,
        // range for Auto Config
        // AC network incompatible reason codes
        AcBase = Base + RangeSize,
        NetworkNotCompatible,
        ProfileNotCompatible,
        // AC connect reason code
        AcConnectBase = (AcBase + RangeSize / 2),
        NoAutoConnection,
        NotVisible,
        GpDenied,
        UserDenied,
        BssTypeNotAllowed,
        InFailedList,
        InBlockedList,
        SsidListTooLong,
        ConnectCallFail,
        ScanCallFail,
        NetworkNotAvailable,
        ProfileChangedOrDeleted,
        KeyMismatch,
        UserNotRespond,
        AcEnd = (AcBase + RangeSize - 1),
        // range for profile manager
        // it has profile adding failure reason codes, but may not have 
        // connection reason codes
        // Profile validation errors
        ProfileBase = Base + (7 * RangeSize),
        InvalidProfileSchema,
        ProfileMissing,
        InvalidProfileName,
        InvalidProfiletype,
        InvalidPhyType,
        MsmSecurityMissing,
        IhvSecurityNotSupported,
        IhvOuiMismatch,
        // Ihv OUI not present but there is Ihv settings in profile
        IhvOuiMissing,
        // Ihv OUI is present but there is no Ihv settings in profile
        IhvSettingsMissing,
        // both/conflict MSMSec and Ihv security settings exist in profile 
        ConflictSecurity,
        // no Ihv or MSMSec security settings in profile
        SecurityMissing,
        InvalidBssType,
        InvalidAdhocConnectionMode,
        NonBroadcastSetForAdhoc,
        AutoSwitchSetForAdhoc,
        AutoSwitchSetForManualConnection,
        IhvSecurityOneXMissing,
        ProfileSsidInvalid,
        TooManySsid,
        ProfileConnectBase = (ProfileBase + RangeSize / 2),
        ProfileEnd = (ProfileBase + RangeSize - 1),
        // range for Msm
        // Msm network incompatible reasons
        MsmBase = Base + (2 * RangeSize),
        UnsupportedSecuritySetByOs,
        UnsupportedSecuritySet,
        BssTypeUnmatch,
        PhyTypeUnmatch,
        DatarateUnmatch,
        // Msm connection failure reasons, to be defined
        // failure reason codes
        MsmConnectBase = (MsmBase + RangeSize / 2),
        // user called to disconnect
        UserCancelled,
        // got disconnect while associating
        AssociationFailure,
        // timeout for association
        AssociationTimeout,
        // pre-association security completed with failure
        PreSecurityFailure,
        // fail to start post-association security
        StartSecurityFailure,
        // post-association security completed with failure
        SecurityFailure,
        // security watchdog timeout
        SecurityTimeout,
        // got disconnect from driver when roaming
        RoamingFailure,
        // failed to start security for roaming
        RoamingSecurityFailure,
        // failed to start security for adhoc-join
        AdhocSecurityFailure,
        // got disconnection from driver
        DriverDisconnected,
        // driver operation failed
        DriverOperationFailure,
        // Ihv service is not available
        IhvNotAvailable,
        // Response from ihv timed out
        IhvNotResponding,
        // Timed out waiting for driver to disconnect
        DisconnectTimeout,
        // An internal error prevented the operation from being completed.
        InternalFailure,
        // UI Request timed out.
        UiRequestTimeout,
        // Roaming too often, post security is not completed after 5 times.
        TooManySecurityAttepmts,
        MsmEnd = (MsmBase + RangeSize - 1),
        // range for MSMSEC
        // MSMSEC reason codes
        MsmSecBase = Base + (3 * RangeSize),
        // Data index specified is not valid
        MsmSecProfileInvalidKeyIndex,
        // Data required, PSK present
        MsmSecProfilePskPresent,
        // Invalid key length
        MsmSecProfileKeyLength,
        // Invalid PSK length
        MsmSecProfilePskLength,
        // No auth/cipher specified
        MsmSecProfileNoAuthCipherSpecified,
        // Too many auth/cipher specified
        MsmSecProfileTooManyAuthCipherSpecified,
        // Profile contains duplicate auth/cipher
        MsmSecProfileDuplicateAuthCipher,
        // Profile raw data is invalid (1x or key data)
        MsmSecProfileRawdataInvalid,
        // Invalid auth/cipher combination
        MsmSecProfileInvalidAuthCipher,
        // 802.1x disabled when it's required to be enabled
        MsmSecProfileOneXDisabled,
        // 802.1x enabled when it's required to be disabled
        MsmSecProfileOneXEnabled,
        MsmSecProfileInvalidPmkCacheMode,
        MsmSecProfileInvalidPmkCacheSize,
        MsmSecProfileInvalidPmkCacheTtl,
        MsmSecProfileInvalidPreauthMode,
        MsmSecProfileInvalidPreauthThrottle,
        // PreAuth enabled when PMK cache is disabled
        MsmSecProfilePreauthOnlyEnabled,
        // Capability matching failed at network
        MsmSecCapabilityNetwork,
        // Capability matching failed at NIC
        MsmSecCapabilityNic,
        // Capability matching failed at profile
        MsmSecCapabilityProfile,
        // Network does not support specified discovery type
        MsmSecCapabilityDiscovery,
        // Passphrase contains invalid character
        MsmSecProfilePassPhraseChar,
        // Data material contains invalid character
        MsmSecProfileKeyMaterialChar,
        // Wrong key type specified for the auth/cipher pair
        MsmSecProfileWrongKeyType,
        // "Mixed cell" suspected (Ap not beaconing privacy, we have privacy enabled profile)
        MsmSecMixedCell,
        // Auth timers or number of timeouts in profile is incorrect
        MsmSecProfileAuthTimersInvalid,
        // Group key update interval in profile is incorrect
        MsmSecProfileInvalidGroupKeyInterval,
        // "Transition network" suspected, trying legacy 802.11 security
        MsmSecTransitionNetwork,
        // Data contains characters which do not map to ASCII
        MsmSecProfileKeyUnmappedChar,
        // Capability matching failed at profile (auth not found)
        MsmSecCapabilityProfileAuth,
        // Capability matching failed at profile (cipher not found)
        MsmSecCapabilityProfileCipher,
        MsmSecConnectBase = (MsmSecBase + RangeSize / 2),
        // Failed to queue UI request
        MsmSecUiRequestFailure,
        // 802.1x authentication did not start within configured time 
        MsmSecAuthStartTimeout,
        // 802.1x authentication did not complete within configured time
        MsmSecAuthSuccessTimeout,
        // Dynamic key exchange did not start within configured time
        MsmSecKeyStartTimeout,
        // Dynamic key exchange did not succeed within configured time
        MsmSecKeySuccessTimeout,
        // Message 3 of 4 way handshake has no key data (RSN/WPA)
        MsmSecM3MissingKeyData,
        // Message 3 of 4 way handshake has no IE (RSN/WPA)
        MsmSecM3MissingIe,
        // Message 3 of 4 way handshake has no Group Data (RSN)
        MsmSecM3MissingGroupKey,
        // Matching security capabilities of IE in M3 failed (RSN/WPA)
        MsmSecPrimaryIeMatching,
        // Matching security capabilities of Secondary IE in M3 failed (RSN)
        MsmSecSecondaryIeMatching,
        // Required a pairwise key but Ap configured only group keys
        MsmSecNoPairwiseKey,
        // Message 1 of group key handshake has no key data (RSN/WPA)
        MsmSecG1MissingKeyData,
        // Message 1 of group key handshake has no group key
        MsmSecG1MissingGroupKey,
        // Ap reset discoverySecure bit after connection was secured
        MsmSecPeerIndicatedInsecure,
        // 802.1x indicated there is no authenticator but profile requires 802.1x
        MsmSecNoAuthenticator,
        // Plumbing settings to NIC failed
        MsmSecNicFailure,
        // Operation was cancelled by caller
        MsmSecCancelled,
        // Data was in incorrect format 
        MsmSecKeyFormat,
        // Security downgrade detected
        MsmSecDowngradeDetected,
        // PSK mismatch suspected
        MsmSecPskMismatchSuspected,
        // Forced failure because connection method was not discoverySecure
        MsmSecForcedFailure,
        // ui request couldn't be queued or user pressed cancel
        MsmSecSecurityUiFailure,
        MsmSecEnd = (MsmSecBase + RangeSize - 1),
    }
}
