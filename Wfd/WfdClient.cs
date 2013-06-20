using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace EugenPechanec.NativeWifi.Wfd {
    public sealed class WfdClient : IDisposable {
        
        public Version ClientVersion {
            get {
                OperatingSystem os = Environment.OSVersion;
                if (os.Platform == PlatformID.Win32NT) {
                    Version vs = os.Version;
                    if (vs.Major > 6) {
                        return new Version(1, 0);
                    } else if (vs.Major == 6 && vs.Minor >= 2) {
                        return new Version(1, 0);
                    }
                }
                return new Version(0, 0);
            }
        }

        internal IntPtr clientHandle;
        public Version NegotiatedVersion { get; private set; }
        
        private WfdClient() {
            uint clientVersionDword = Util.VersionToDword(ClientVersion);
            uint negotiatedVersionDword;
            Util.ThrowIfError(NativeMethods.WlanOpenHandle(clientVersionDword, IntPtr.Zero, out negotiatedVersionDword, out clientHandle));
            NegotiatedVersion = Util.DwordToVersion(negotiatedVersionDword);

        }

        ~WfdClient() {
            if (clientHandle != IntPtr.Zero) {
                NativeMethods.WfdCloseHandle(clientHandle);
                clientHandle = IntPtr.Zero;
            }
        }

        public void Dispose() {
            if (clientHandle != IntPtr.Zero) {
                NativeMethods.WfdCloseHandle(clientHandle);
                clientHandle = IntPtr.Zero;
            }
        }
    }
}
