// Copyright (c) 2013 Eugen Pechanec

using System;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace EugenPechanec.NativeWifi {
    public class Util {
        private Util() { }

        public static string ToHexDelimited(byte[] bytes, int length = -1) {
            int len = length < 0 ? bytes.Length : length;
            if (len < bytes.Length) throw new ArgumentException();
            string hex = "";
            for (int i = 0; i < len; i++) {
                hex += String.Format(":{0:x2}", bytes[i]);
            }
            return hex.Substring(1);
        }
        public static string ToHex(byte[] bytes, int length = -1) {
            int len = length < 0 ? bytes.Length : length;
            if (len < bytes.Length) throw new ArgumentException();
            string hex = "";
            for (int i = 0; i < len; i++) {
                hex += String.Format("{0:x2}", bytes[i]);
            }
            return hex;
        }
        /// <summary>
        /// Helper method to wrap calls to Native WiFi API methods.
        /// If the method falls, throws an exception containing the error code.
        /// </summary>
        /// <param name="win32ErrorCode">The error code.</param>
        [DebuggerStepThrough]
        internal static void ThrowIfError(int win32ErrorCode) {
            if (win32ErrorCode != 0)
                throw new Win32Exception(win32ErrorCode);
        }
        /// <summary>
        /// Gets a string that describes a specified reason code.
        /// </summary>
        /// <param name="peerStateChange">The reason code.</param>
        /// <returns>The string.</returns>
        internal static string GetStringForReasonCode(WlanReasonCode reasonCode) {
            StringBuilder sb = new StringBuilder(1024); // the 1024 dataSize here is arbitrary; the WlanReasonCodeToString docs fail to specify a recommended dataSize
            ThrowIfError(
                NativeMethods.WlanReasonCodeToString(reasonCode, (uint)sb.Capacity, sb, IntPtr.Zero));
            return sb.ToString();
        }
        //=========================================================================
        internal static Version DwordToVersion(uint dword) {
            int major = (int)dword & 0xffff;
            int minor = (int)dword >> 16;
            Version version = new Version(major, minor);
            return version;
        }
        internal static uint VersionToDword(Version version) {
            uint major = (uint)version.Major;
            uint minor = (uint)version.Minor;
            uint dword = minor << 16 | major;
            return dword;
        }

        internal static PhysicalAddress Dot11MacAddressToPhysicalAddress(Dot11MacAddress macAddress) {
            return Dot11MacAddress.ToPhysicalAddress(macAddress);
        }
        internal static Dot11MacAddress PhysicalAddressToDot11MacAddress(PhysicalAddress phyAddress) {
            return Dot11MacAddress.FromPhysicalAddress(phyAddress);
        }
        internal static PhysicalAddress[] ConvertDot11MacAddresses(Dot11MacAddress[] macAddresses) {
            int length = macAddresses.Length;
            PhysicalAddress[] array = new PhysicalAddress[length];
            for (int i = 0; i < length; i++) {
                array[i] = Dot11MacAddress.ToPhysicalAddress(macAddresses[i]);
            }
            return array;
        }
        internal static Dot11MacAddress[] ConvertPhysicalAddresses(PhysicalAddress[] phyAddresses) {
            int length = phyAddresses.Length;
            Dot11MacAddress[] array = new Dot11MacAddress[length];
            for (int i = 0; i < length; i++) {
                array[i] = Dot11MacAddress.FromPhysicalAddress(phyAddresses[i]);
            }
            return array;
        }
        //=========================================================================

        internal static T ParseStruct<T>(IntPtr pointer, uint size) where T : struct {
            int expectedSize = Marshal.SizeOf(typeof(T));
            T value = new T();
            if (size >= expectedSize) {
                value = (T)Marshal.PtrToStructure(pointer, typeof(T));
            }
            return value;
        }

        internal static uint ParseDword(IntPtr pointer) {
            uint value = Convert.ToUInt32(Marshal.ReadInt32(pointer));
            return value;
        }

    }
}
