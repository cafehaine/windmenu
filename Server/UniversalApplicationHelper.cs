using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Server
{
    /// <summary>
    /// All of the code for Com interfacing was done by sankar@stackoverflow.
    /// Many thanks to him.
    /// </summary>
    static class UAHelper
    {
        private enum ActivateOptions
        {
            None = 0x00000000
        }

        [ComImport, Guid("2e941141-7f97-4756-ba1d-9decde894a3d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IApplicationActivationManager
        {
            // Activates the specified immersive application for the "Launch" contract, passing the provided arguments
            // string into the application.  Callers can obtain the process Id of the application instance fulfilling this contract.
            IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);
            IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr /*IShellItemArray* */ itemArray, [In] String verb, [Out] out UInt32 processId);
            IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr /* IShellItemArray* */itemArray, [Out] out UInt32 processId);
        }

        [ComImport, Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]//Application Activation Manager
        private class ApplicationActivationManager : IApplicationActivationManager
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)/*, PreserveSig*/]
            public extern IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            public extern IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr /*IShellItemArray* */ itemArray, [In] String verb, [Out] out UInt32 processId);
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            public extern IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr /* IShellItemArray* */itemArray, [Out] out UInt32 processId);
        }

        private static Regex appNameRegex =
            new Regex(@"(.+\.)+([a-zA-Z]*)_.*!.*");

        private static string prettifyName(string name)
        {
            StringBuilder strBldr = new StringBuilder(name.Length * 2);
            bool wasPrevUpper = true;
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (char.IsUpper(c) && !wasPrevUpper)
                    strBldr.Append(" " + c);
                else
                    strBldr.Append(c);

                wasPrevUpper = char.IsUpper(c);
            }

            return strBldr.ToString();
        }

        public static List<KeyValuePair<string,string>> GetApplicationList()
        {
            List<KeyValuePair<string, string>> output =
                new List<KeyValuePair<string, string>>();
            
            using (RegistryKey key = Registry.CurrentUser.
                OpenSubKey(@"Software\Classes"))
            {
                foreach (string subKey in key.GetSubKeyNames())
                    if (subKey.StartsWith("AppX"))
                    {
                        string appID = string.Empty;
                        //TODO: check if null instead of try/catch
                        //TODO: use using or dispose opened keys
                        try
                        {
                            appID = (string)key.OpenSubKey(subKey).
                                OpenSubKey("Application").
                                GetValue("AppUserModelID", string.Empty);
                        }
                        catch (Exception){}
                        if (appID != string.Empty)
                        {
                            Match matches = appNameRegex.Match(appID);
                            if (matches.Success)
                            {
                                if (output.FindIndex(k => k.Value == appID) == -1)
                                {
                                    KeyValuePair<string, string> kvp =
                                        new KeyValuePair<string, string>(
                                            prettifyName(matches.Groups[2].Value),
                                            appID);
                                    output.Add(kvp);
                                }
                            }
                        }
                    }
            }

            return output;
        }

        public static void RunApplication(string app)
        {
            ApplicationActivationManager appActiveManager = new ApplicationActivationManager();//Class not registered
            uint pid;
            appActiveManager.ActivateApplication(app, null, ActivateOptions.None, out pid);
        }
    }
}
