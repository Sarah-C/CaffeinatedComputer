Imports System.Runtime.InteropServices

Public Class Power

    Public Shared _PowerRequestContext As POWER_REQUEST_CONTEXT = Nothing
    Public Shared _PowerRequest As IntPtr = Nothing 'HANDLE

    ' Availability Request Functions
    <DllImport("kernel32.dll")>
    Public Shared Function PowerCreateRequest(ByRef Context As POWER_REQUEST_CONTEXT) As IntPtr
    End Function

    <DllImport("kernel32.dll")>
    Public Shared Function PowerSetRequest(ByVal PowerRequestHandle As IntPtr, ByVal RequestType As PowerRequestType) As Boolean
    End Function

    <DllImport("kernel32.dll")>
    Public Shared Function PowerClearRequest(ByVal PowerRequestHandle As IntPtr, ByVal RequestType As PowerRequestType) As Boolean
    End Function

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto, SetLastError:=True, ExactSpelling:=True)>
    Public Shared Function CloseHandle(ByVal hObject As IntPtr) As Integer
    End Function

    ' Availablity Request Enumerations and Constants
    Public Enum PowerRequestType
        PowerRequestDisplayRequired = 0
        PowerRequestSystemRequired
        PowerRequestAwayModeRequired
        PowerRequestMaximum
    End Enum

    Public Const POWER_REQUEST_CONTEXT_VERSION As Integer = 0
    Public Const POWER_REQUEST_CONTEXT_SIMPLE_STRING As Integer = &H1
    Public Const POWER_REQUEST_CONTEXT_DETAILED_STRING As Integer = &H2

    ' Availablity Request Structures
    ' Note:  Windows defines the POWER_REQUEST_CONTEXT structure with an
    ' internal union of SimpleReasonString and Detailed information.
    ' To avoid runtime interop issues, this version of 
    ' POWER_REQUEST_CONTEXT only supports SimpleReasonString.  
    ' To use the detailed information,
    ' define the PowerCreateRequest function with the first 
    ' parameter of type POWER_REQUEST_CONTEXT_DETAILED.
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure POWER_REQUEST_CONTEXT
        Public Version As UInt32
        Public Flags As UInt32
        <MarshalAs(UnmanagedType.LPWStr)>
        Public SimpleReasonString As String
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure PowerRequestContextDetailedInformation
        Public LocalizedReasonModule As IntPtr
        Public LocalizedReasonId As UInt32
        Public ReasonStringCount As UInt32
        <MarshalAs(UnmanagedType.LPWStr)>
        Public ReasonStrings() As String
    End Structure

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure POWER_REQUEST_CONTEXT_DETAILED
        Public Version As UInt32
        Public Flags As UInt32
        Public DetailedInformation As PowerRequestContextDetailedInformation
    End Structure

    ''' <summary>
    ''' Prevent screensaver, display dimming and power saving. This function wraps PInvokes on Win32 API. 
    ''' </summary>
    ''' <param name="enableConstantDisplayAndPower">True to get a constant display and power - False to clear the settings</param>
    Public Shared Sub EnableConstantDisplayAndPower(ByVal enableConstantDisplayAndPower As Boolean)
        If enableConstantDisplayAndPower Then
            ' Set up the diagnostic string
            _PowerRequestContext.Version = POWER_REQUEST_CONTEXT_VERSION
            _PowerRequestContext.Flags = POWER_REQUEST_CONTEXT_SIMPLE_STRING
            _PowerRequestContext.SimpleReasonString = "Continuous measurement" ' your reason for changing the power settings;

            ' Create the request, get a handle
            _PowerRequest = PowerCreateRequest(_PowerRequestContext)

            ' Set the request
            PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired)
            PowerSetRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired)
        Else
            ' Clear the request
            PowerClearRequest(_PowerRequest, PowerRequestType.PowerRequestSystemRequired)
            PowerClearRequest(_PowerRequest, PowerRequestType.PowerRequestDisplayRequired)

            CloseHandle(_PowerRequest)
        End If
    End Sub

End Class
