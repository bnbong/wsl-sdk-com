﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using WslSdk.Contracts;
using WslSdk.Interop;
using WslSdk.Models;

namespace WslSdk
{
    [ComVisible(true)]
    [ProgId("WslSdk.WslService")]
    [Guid("1D0D99B6-AF95-4D3F-B55B-BA17CB2D549B")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class WslService : IWslService
    {
        public WslService()
        {
            // Increment the lock count of objects in the COM server.
            WslComServer.Instance.Lock();
        }

        ~WslService()
        {
            // Decrement the lock count of objects in the COM server.
            WslComServer.Instance.Unlock();
        }

        public bool IsDistroRegistered(string distroName)
        {
            return NativeMethods.WslIsDistributionRegistered(distroName);
        }

        public DistroRegistryInfo GetDefaultDistro()
        {
            return Wsl.GetDefaultDistroFromRegistry();
        }

        public string[] GetDistroList()
        {
            return Wsl.EnumerateDistroFromRegistry().Select(x => x.DistroName).ToArray();
        }

        public string RunWslCommand(string distroName, string commandLine)
        {
            return Wsl.RunWslCommand(distroName, commandLine);
        }

        public DistroRegistryInfo GetDistroInfo(string distroName)
        {
            return Wsl.GetDistroFromRegistry(distroName);
        }

        public string GetDefaultDistroName()
        {
            return Wsl.GetDefaultDistroFromRegistry()?.DistroName;
        }

        public DistroInfo QueryDistroInfo(string distroName)
        {
            return Wsl.QueryDistro(distroName);
        }

        public void SetDefaultUid(string distroName, int defaultUid)
        {
            throw new NotImplementedException();
        }

        public void SetDistroFlags(string distroName, DistroFlags distroFlags)
        {
            throw new NotImplementedException();
        }

        public void RegisterDistro(string newDistroName, string tarGzipFilePath)
        {
            throw new NotImplementedException();
        }

        public void UnregisterDistro(string existingDistroName)
        {
            throw new NotImplementedException();
        }

        public string GetWslWindowsPath(string distroName)
        {
            throw new NotImplementedException();
        }

        public string TranslateToWindowsPath(string distroName, string linuxPath)
        {
            throw new NotImplementedException();
        }

        public string TranslateToLinuxPath(string distroName, string windowsPath)
        {
            throw new NotImplementedException();
        }

        public string CreateDriveMapping(string distroName, string desiredDriveLetter)
        {
            throw new NotImplementedException();
        }

        public string CreateSymbolicLink(string distroName, string desiredPath, string symbolicLinkName)
        {
            throw new NotImplementedException();
        }

        // These routines perform the additional COM registration needed by 
        // the service.

        [ComRegisterFunction]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Register(Type t)
        {
            try
            {
                HelperMethods.RegasmRegisterLocalServer(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw ex; // Re-throw the exception
            }
        }

        [ComUnregisterFunction]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Unregister(Type t)
        {
            try
            {
                HelperMethods.RegasmUnregisterLocalServer(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw ex; // Re-throw the exception
            }
        }
    }
}