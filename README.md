# CaffeinatedComputer
Despite the folder name - this does NOT wiggle the mouse. It uses Windows own power management API.

It lives in your system tray:

![image](https://user-images.githubusercontent.com/1586332/126336544-ab3d2774-b6eb-4708-845e-a83b49f0dae3.png)

The program when it starts running in the system tray, asks Windows Kernel (winbase.h) to keep the "System" as well as the "Display" powered. (Everything)

     _PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired)_
     
     _PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired)_
 
 When the user closes it (right click the icon, and "Close the program"), the requests are cleared.
 
 https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-powersetrequest
 
 
