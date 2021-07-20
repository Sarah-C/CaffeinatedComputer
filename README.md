# CaffeinatedComputer
Despite the folder name - this does NOT wiggle the mouse. It uses Windows own power management API.

It lives in your system tray:

![image](https://user-images.githubusercontent.com/1586332/126336859-210218ee-128a-4357-bb6f-3c76bfece72b.png)

The program when it starts running in the system tray, asks Windows Kernel (winbase.h) to keep the "System" as well as the "Display" powered. (Everything)

     _PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired)_
     
     _PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired)_
 
 When the user closes it (right click the icon, and "Close the program"), the requests are cleared.
 
 https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-powersetrequest
 
 
