NativeWifi
==========

C# managed version of Native Wi-Fi API present on Windows.

Based on http://managedwifi.codeplex.com/ .

The MIT License (MIT)
Copyright (c) 2013 Ilya Konstantinov

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

What was there?
---------------
WlanClient class which could enumerate Wlan interfaces,
  query their settings, profiles, scan and connect to networks.

What is new?
------------
Added OneX enums and structs.
Renamed all enum values to conform to C# style.
Added some functionality to WlanInterface (such as Disconnect).
Added Hosted Network (on Windows 7+) control.
Several times remade marshalling of variable length arrays.
  Now working with IntPtr which allows marshalling back
  au contraire to SizeConst = 1 arrays.
Wi-Fi Direct API is started but not well described and well
  beyond original target of this project.
All events concerning Native Wi-fi.

What has to be done?
--------------------
Complete XMLdoc.
Fix LinkDemands warnings.
Provide alternatives for structs containing Dot11CountryOrRegionString
  (string of len 3) and Dot11MacAddress (PhysicalAddress equivalent)
  which are to be internal.
Finish missing WlanInterface properties/methods: RadioState,
  BackgroundScanEnabled, SupportedInfrastructureAuthCipherPairs,
  SupportedAdhocAuthCipherPairs, CountryOrRegionStringList,
  MediaStreamingMode, Statistics, SupportedSafeMode, CertifiedSafeMode,
  AvailableNetworks, Profiles, BssList, Capability, IhvControl.
Finish missing WlanClient properties/methods: ExtractPsdIeDataList,
  SetPsdIeDataList, GetSecuritySettings, SetSecuritySettings,
  FilterList.
Create or abandon special WlanProfile class (name, xml, custom data).
Fix any misplaced events between WlanInterface and WlanClient.
Determine whether to drop WlanInterface.EditProfile method.
Determine whether to drop the omnipresent and deeply annoying prefix.

Thanks to everyone who contributed to ManagedWifi, it has provided me
with some insight to P/Invoke, events and gave me something to
occupy my head during exam periods.

Copyright (c) 2013 Eugen Pechanec
