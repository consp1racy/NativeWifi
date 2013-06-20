using System;
using System.Text;
using System.Runtime.InteropServices;

namespace EugenPechanec.NativeWifi {
    internal static partial class NativeMethods {
        // WI-FI DIRECT ==============================================================================
        //[DllImport("wlanapi.dll", EntryPoint="WFD_OPEN_SESSION_COMPLETE_CALLBACK")]
        //internal static extern delegate void WfdOpenSessionCompleteCallback(
        //    [In] ref IntPtr hSessionHandle,
        //    [In] IntPtr pvContext,
        //    [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidSessionInterface,
        //    [In] uint dwError,
        //    [In] WlanReasonCode dwReasonCode
        //);
        [DllImport("wlanapi.dll", EntryPoint="WFDCancelOpenSession")]
        internal static extern int WfdCancelOpenSession(
            [In] ref IntPtr hSessionHandle
        );
        [DllImport("wlanapi.dll", EntryPoint = "WFDCloseHandle")]
        internal static extern int WfdCloseHandle(
            [In] IntPtr hClientHandle
        );
        [DllImport("wlanapi.dll", EntryPoint = "WFDCloseSession")]
        internal static extern int WfdCloseSession(
            [In] ref IntPtr hSessionHandle
        );

        [DllImport("wlanapi.dll", EntryPoint = "WFDOpenHandle")]
        internal static extern int WfdOpenHandle(
            //[In] uint dwClientVersion,
            [In] uint dwClientVersion,
            [Out] out uint pdwNegotiatedVersion,
            [Out] out IntPtr phClientHandle
        );
        [DllImport("wlanapi.dll", EntryPoint = "WFDOpenLegacySession")]
        internal static extern int WfdOpenLegacySession(
            [In] IntPtr hClientHandle,
            [In] ref Dot11MacAddress pLegacyMacAddress,
            [In] IntPtr phSessionHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid pGuidSessionInterface
        );
        [DllImport("wlanapi.dll", EntryPoint = "WFDStartOpenSession")]
        internal static extern int WfdStartOpenSession(
            [In] IntPtr hClientHandle,
            [In] ref Dot11MacAddress pDeviceAddress,
            [In, Optional] IntPtr pvContext,
            [In] WfdOpenSessionCompleteCallbackDelegate pfnCallback,
            [Out] out IntPtr phSessionHandle
        );
        [DllImport("wlanapi.dll", EntryPoint = "WFDUpdateDeviceVisibility")]
        internal static extern int WfdUpdateDeviceVisibility(
            [In] ref Dot11MacAddress pDeviceAddress
        );
         
        // WLAN PART 1 ===============================================================================

        [DllImport("wlanapi.dll")]
        internal static extern IntPtr WlanAllocateMemory(
            [In] uint dwMemorySize
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanCloseHandle(
            [In] IntPtr hClientHandle,
            [In, Out] IntPtr pReserved
        );
        
        [DllImport("wlanapi.dll")]
        internal static extern int WlanConnect(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In] ref WlanConnectionParameters pConnectionParameters,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanDeleteProfile(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanDisconnect(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanEnumInterfaces(
            [In] IntPtr hClientHandle,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppInterfaceList
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanExtractPsdIEDataList(
            [In] IntPtr hClientHandle,
            [In] uint dwIeDataSize,
            [In] IntPtr pRawIeData,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strFormat,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppPsdIEDataList
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanFreeMemory(
            [In] IntPtr pMemory
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetAvailableNetworkList(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In] WlanGetAvailableNetworkFlags dwFlags,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppAvailableNetworkList
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetFilterList(
            [In] IntPtr hClientHandle,
            [In] WlanFilterListType wlanFilterListType,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppNetworkList
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetInterfaceCapability(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppCapability
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetNetworkBssList(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Optional] ref Dot11Ssid pDot11Ssid,
            [In] Dot11BssType dot11BssType,
            [In] bool bSecurityEnabled,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppWlanBssList
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetNetworkBssList(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Optional] IntPtr pDot11Ssid,
            [In] Dot11BssType dot11BssType,
            [In] bool bSecurityEnabled,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppWlanBssList
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetProfile(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr strProfileXml,
            [Out, Optional] out WlanProfileFlags dwFlags,
            [Out, Optional] out WlanAccess pdwGrantedAccess
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetProfileCustomUserData(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In, Out] IntPtr pReserved,
            [Out, Optional] out uint dwDataSize,
            [Out, Optional] out IntPtr ppData
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetProfileList(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Out] IntPtr pReserved,
            [Out] out IntPtr ppProfileList
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanGetSecuritySettings(
            [In] IntPtr hClientHandle,
            [In] WlanSecurableObject SecurableObject,
            [Out, Optional] out WlanOpcodeValueType pValueType,
            [Out] out IntPtr strCurrentSDDL,
            [Out] out WlanAccess pdwGrantedAccess
        );
        // WLAN PART 2 ===============================================================================
        [DllImport("wlanapi.dll")]
        internal static extern int WlanIhvControl(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In] WlanIhvControlType Type,
            [In] uint dwInBufferSize,
            [In] IntPtr pInBuffer,
            [In] uint dwOutBufferSize,
            [Out, Optional] out IntPtr pOutBuffer,
            [Out] out uint pdwBytesReturned
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanOpenHandle(
            [In] uint dwClientVersion,
            [In, Out] IntPtr pReserved,
            [Out] out uint pdwNegotiatedVersion,
            [Out] out IntPtr phClientHandle
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanQueryAutoConfigParameter(
            [In] IntPtr hClientHandle,
            [In] WlanAutoConfOpcode OpCode,
            [In, Out] IntPtr pReserved,
            [Out] out uint pdwDataSize,
            [Out] out IntPtr ppData,
            [Out, Optional] out WlanOpcodeValueType pWlanOpodeValueType
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanQueryInterface(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In] WlanIntfOpcode OpCode,
            [In, Out] IntPtr pReserved,
            [Out] out uint pdwDataSize,
            [Out] out IntPtr ppData,
            [Out, Optional] out WlanOpcodeValueType pWlanOpcodeValueType
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanReasonCodeToString(
            [In] WlanReasonCode dwReasonCode,
            [In] uint dwBufferSize,
            [In, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pStringBuffer, //ref?
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanRegisterNotification(
            [In] IntPtr hClientHandle,
            [In] WlanNotificationSource dwNotifSource,
            [In] bool bIgnoreDuplicate,
            [In, Optional] WlanNotificationCallbackDelegate funcCallback,
            [In, Optional] IntPtr pCallbackContext,
            [In, Out] IntPtr pReserved,
            [Out, Optional] out WlanNotificationSource pdwPrevNotifSource
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanRegisterVirtualStationNotification(
            [In] IntPtr hClientHandle,
            [In] bool bRegister,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanRenameProfile(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strOldProfileName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strNewProfileName,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSaveTemporaryProfile(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In, Optional, MarshalAs(UnmanagedType.LPWStr)] string strAllUserProfileSecurity,
            [In] WlanProfileFlags dwFlags,
            [In] bool bOverWrite,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanScan(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Optional] IntPtr pDot11Ssid,
            [In, Optional] IntPtr pIeData,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanScan(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Optional] IntPtr pDot11Ssid,
            [In, Optional] ref WlanRawData pIeData,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanScan(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Optional] ref Dot11Ssid pDot11Ssid,
            [In, Optional] IntPtr pIeData,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanScan(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, Optional] ref Dot11Ssid pDot11Ssid,
            [In, Optional] ref WlanRawData pIeData,
            [In, Out] IntPtr pReserved
        );
        //[DllImport("wlanapi.dll")]
        //internal static extern int WlanUIEditProfile(
        //    [In] uint dwClientVersion,
        //    [In, MarshalAs(UnmanagedType.LPWStr)] string wstrProfileName,
        //    [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
        //    [In] IntPtr hWnd,
        //    [In] WlDisplayPages wlStartPage,
        //    [In, Out] IntPtr pReserved,
        //    [Out] out WlanReasonCode wlanReasonCode
        //);
        //= WLAN PART 3 ===============================================================================
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetAutoConfigParameter(
            [In] IntPtr hClientHandle,
            [In] WlanAutoConfOpcode OpCode,
            [In] uint dwDataSize,
            [In] IntPtr pData,
            [In, Out] IntPtr pReserved
        );
        //[DllImport("wlanapi.dll")]
        //internal static extern int WlanSetAutoConfigParameter(
        //    [In] IntPtr hClientHandle,
        //    [In] WlanAutoConfOpcode OpCode,
        //    [In] uint dwDataSize,
        //    [In] ref bool pData,
        //    [In, Out] IntPtr pReserved
        //);
        //[DllImport("wlanapi.dll")]
        //internal static extern int WlanSetAutoConfigParameter(
        //    [In] IntPtr hClientHandle,
        //    [In] WlanAutoConfOpcode OpCode,
        //    [In] uint dwDataSize,
        //    [In] ref uint pData,
        //    [In, Out] IntPtr pReserved
        //);
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetFilterList(
            [In] IntPtr hClientHandle,
            [In] WlanFilterListType wlanFilterListType,
            [In, Optional] IntPtr pNetworkList,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetFilterList(
            [In] IntPtr hClientHandle,
            [In] WlanFilterListType wlanFilterListType,
            [In, Optional] ref Dot11NetworkList pNetworkList,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetInterface(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In] WlanIntfOpcode OpCode,
            [In] uint dwDataSize,
            [In] IntPtr pData,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetProfile(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In] WlanProfileFlags dwFlags,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileXml,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strAllUserProfileSecurity,
            [In] bool bOverwrite,
            [In, Out] IntPtr pReserved,
            [Out] out WlanReasonCode dwReasonCode
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetProfileCustomUserData(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In] uint dwDataSize,
            [In] IntPtr pData,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetProfileEapUserData(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In] EapMethodType eapType,
            [In] uint dwFlags, // flags?
            [In] uint dwEapUserDataSize,
            [In] IntPtr pbEapUserData,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetProfileEapXmlUserData(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In] uint dwFlags, // flags?
            [In, MarshalAs(UnmanagedType.LPWStr)] string strEapXmlUserData,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetProfileList(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In] uint dwItems,
            [In] IntPtr strProfileNames, // ref?
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetProfilePosition(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strProfileName,
            [In] uint dwPosition,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetPsdIEDataList(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strFormat,
            [In] IntPtr pPsdIEDataList,
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetPsdIEDataList(
            [In] IntPtr hClientHandle,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strFormat,
            [In] ref WlanRawDataList pPsdIEDataList, // check WlanRawDataList type!
            [In, Out] IntPtr pReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanSetSecuritySettings(
            [In] IntPtr hClientHandle,
            [In] WlanSecurableObject SecurableObject,
            [In, MarshalAs(UnmanagedType.LPWStr)] string strModifiedSDDL
        );
        // HOSTED NETWORK ============================================================================
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkForceStart(
            [In] IntPtr hClientHandle,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkForceStop(
            [In] IntPtr hClientHandle,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkStartUsing(
            [In] IntPtr hClientHandle,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkStopUsing(
            [In] IntPtr hClientHandle,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkInitSettings(
            [In] IntPtr hClientHandle,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkRefreshSecuritySettings(
            [In] IntPtr hClientHandle,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkQueryProperty(
            [In] IntPtr hClientHandle,
            [In] WlanHostedNetworkOpcode OpCode,
            [Out] out uint pdwDataSize,
            [Out] out IntPtr ppvData,
            [Out] out WlanOpcodeValueType pWlanOpcodeValueType,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkSetProperty(
            [In] IntPtr hClientHandle,
            [In] WlanHostedNetworkOpcode OpCode,
            [In] int pdwDataSize,
            [In] IntPtr ppvData,
            [Out, Optional] out WlanOpcodeValueType pWlanOpcodeValueType,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkQuerySecondaryKey(
            [In] IntPtr hClientHandle,
            [Out] out uint pdwKeyLength,
            [Out] out IntPtr ppucKeyData,
            [Out] out bool pbIsPassPhrase,
            [Out] out bool pbPersistent,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkSetSecondaryKey(
            [In] IntPtr hClientHandle,
            [In] uint pdwKeyLength,
            [In] IntPtr ppucKeyData,
            [In] bool pbIsPassPhrase,
            [In] bool pbPersistent,
            [Out, Optional] out WlanHostedNetworkReason pFailReason,
            [In, Out] IntPtr pvReserved
        );
        [DllImport("wlanapi.dll")]
        internal static extern int WlanHostedNetworkQueryStatus(
            [In] IntPtr hClientHandle,
            [Out] out IntPtr ppWlanHostedNetworkStatus,
            [In, Out] IntPtr pvReserved
        );
        // DELEGATES ===================================================================================
        internal delegate void WlanNotificationCallbackDelegate(ref WlanNotificationData notificationData, IntPtr context);
         
        internal delegate void WfdOpenSessionCompleteCallbackDelegate(ref IntPtr hSessionHandle, IntPtr pvContext, Guid guidSessionInterface, uint dwError, WlanReasonCode dwReasonCode);
    }
}
