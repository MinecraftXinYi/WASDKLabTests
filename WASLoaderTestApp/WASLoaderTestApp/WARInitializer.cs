﻿using Microsoft.Windows.ApplicationModel.DynamicDependency;
using System;
using System.IO;
using System.Text.Json;

internal static class WARInitializer
{
    public static bool InitializeWAR(out int HResult)
    {
        try
        {
            WASVersionInfoFromConfig wasVersionInfo = new ();
            bool result = Bootstrap.TryInitialize
                (wasVersionInfo.MajorMinorVersion, wasVersionInfo.VersionTag, wasVersionInfo.MinVersion,
                Bootstrap.InitializeOptions.OnNoMatch_ShowUI, out HResult);
            return result;
        }
        catch (Exception ex)
        {
            HResult = ex.HResult;
            return false;
        }
    }

    public class WASVersionInfoFromConfig
    {
        private readonly JsonElement config;
        private readonly JsonDocumentOptions jsonDocumentOptions = new ()
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
        };

        public WASVersionInfoFromConfig()
        {
            string configString = File.ReadAllText("WASDKVersionInfo.json");
            JsonDocument configDoc = JsonDocument.Parse(configString, jsonDocumentOptions);
            config = configDoc.RootElement.GetProperty("WASDKVersionInfo");
        }

        public uint MajorMinorVersion
        {
            get
            {
                string rawdata = config.GetProperty("MajorMinorVersion").GetString();
                return Convert.ToUInt32(rawdata, 16);
            }
        }

        public PackageVersion MinVersion
        {
            get
            {
                string rawdata = config.GetProperty("MinVersion").GetString();
                return new PackageVersion(Convert.ToUInt64(rawdata, 16));
            }
        }

        public string VersionTag
        {
            get
            {
                return config.GetProperty("VersionTag").GetString();
            }
        }
    }
}
