﻿// Copyright 2016 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Google.PowerShell.Common
{
    /// <summary>
    /// This class shell executes "gcloud {command} --format=json",
    /// to allow delegation to Cloud SDK implementation. Used for things like
    /// credential management.
    /// </summary>
    public static class GCloudWrapper
    {
        /// <summary>
        /// Returns the global installation properties path of GoogleCloud SDK.
        /// </summary>
        public static async Task<string> GetInstallationPropertiesPath()
        {
            string gCloudInfoOutput = await GetGCloudCommandOutput("info");
            JToken gCloudInfoJson = JObject.Parse(gCloudInfoOutput);

            // SelectToken will return null if token cannot be found.
            gCloudInfoJson = gCloudInfoJson.SelectToken("config.paths.installation_properties_path");

            if (gCloudInfoJson != null && gCloudInfoJson.Type == JTokenType.String)
            {
                return gCloudInfoJson.Value<string>();
            }

            throw new FileNotFoundException("Installation Properties file for Google Cloud SDK cannot be found.");
        }

        /// <summary>
        /// Returns the access token of the current active config.
        /// </summary>
        public static async Task<ActiveUserToken> GetAccessToken(CancellationToken cancellationToken)
        {
            // We get the issued time before the command so we won't be too late
            // when it comes to token expiry.
            DateTime issuedTime = DateTime.Now;

            string userCredentialJson = await GetGCloudCommandOutput("auth print-access-token");
            cancellationToken.ThrowIfCancellationRequested();
            string currentUser = CloudSdkSettings.GetSettingsValue("account");
            return new ActiveUserToken(userCredentialJson, currentUser);
        }

        /// <summary>
        /// Execute cmd.exe /c "gcloud {command} --format=json" and returns
        /// the standard output if the command returns exit code 0.
        /// This will not pop up any new windows.
        /// The environment parameter is used to set environment variable for the execution.
        /// </summary>
        private static async Task<string> GetGCloudCommandOutput(string command, IDictionary<string, string> environment = null)
        {
#if !UNIX
            var actualCommand = $"gcloud {command} --format=json";
            ProcessOutput processOutput = await ProcessUtils.GetCommandOutput("cmd.exe", $"/c \"{actualCommand}\"", environment);
#else
            var actualCommand = $"{command} --format=json";
            ProcessOutput processOutput = await ProcessUtils.GetCommandOutput("gcloud", $"{actualCommand}", environment);
#endif
            if (processOutput.Succeeded)
            {
                return processOutput.StandardOutput;
            }

            if (!string.IsNullOrWhiteSpace(processOutput.StandardError))
            {
                throw new InvalidOperationException($"Command {actualCommand} failed with error: {processOutput.StandardError}");
            }

            throw new InvalidOperationException($"Command {actualCommand} failed.");
        }
    }
}
