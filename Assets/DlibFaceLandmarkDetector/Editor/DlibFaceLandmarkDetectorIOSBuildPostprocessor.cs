#if (UNITY_5 || UNITY_5_3_OR_NEWER) && UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

using System.Diagnostics;

#if UNITY_2017_2_OR_NEWER
using UnityEditor.iOS.Xcode.Extensions;
#endif
using System;
using System.Linq;
using System.Collections;
using System.IO;

namespace DlibFaceLandmarkDetector.Editor
{
    public class DlibFaceLandmarkDetectorIOSBuildPostprocessor : MonoBehaviour
    {

        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {

            string dlibLibraryPath = Directory.GetFiles(path, "libdlibfacelandmarkdetector.a", SearchOption.AllDirectories).FirstOrDefault();
            if (string.IsNullOrEmpty(dlibLibraryPath))
                throw new System.Exception("Can't find libdlibfacelandmarkdetector.a");

            if (PlayerSettings.iOS.sdkVersion == iOSSdkVersion.DeviceSDK)
            {
                RemoveSimulatorArchitectures(Path.GetDirectoryName(dlibLibraryPath), "libdlibfacelandmarkdetector.a");
            }
        }

        /// <summary>
        /// Removes the simulator architectures.
        /// </summary>
        /// <param name="workingDirectory">Working directory.</param>
        /// <param name="filePath">File path.</param>
        private static void RemoveSimulatorArchitectures(string workingDirectory, string filePath)
        {
            if (!IsSimulatorArchitectures(workingDirectory, filePath)) return;

            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.WorkingDirectory = workingDirectory;

            process.StartInfo.Arguments = "-c \" ";

            process.StartInfo.Arguments += "lipo -remove i386 " + filePath + " -o " + filePath + ";";
            process.StartInfo.Arguments += "lipo -remove x86_64 " + filePath + " -o " + filePath + ";";
            process.StartInfo.Arguments += "lipo -info " + filePath + ";";

            process.StartInfo.Arguments += " \"";

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            process.Close();

            if (string.IsNullOrEmpty(error))
            {
                UnityEngine.Debug.Log("Success RemoveSimulatorArchitectures() : " + output);
            }
            else
            {
                UnityEngine.Debug.LogError("Error RemoveSimulatorArchitectures() : " + error);
            }
        }

        /// <summary>
        /// Whether the file contains the simulator architectures?
        /// </summary>
        /// <param name="workingDirectory">Working directory.</param>
        /// <param name="filePath">File path.</param>
        private static bool IsSimulatorArchitectures(string workingDirectory, string filePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.WorkingDirectory = workingDirectory;

            process.StartInfo.Arguments = "-c \" ";

            process.StartInfo.Arguments += "lipo -archs " + filePath + ";";

            process.StartInfo.Arguments += " \"";

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            process.Close();

            //if (string.IsNullOrEmpty(error))
            //{
            //    UnityEngine.Debug.Log("Success IsSimulatorArchitectures() : " + output);                
            //}
            //else
            //{
            //    UnityEngine.Debug.LogError("Error IsSimulatorArchitectures() : " + error);
            //    return false;
            //}

            return output.Contains("i386") || output.Contains("x86_64");
        }
    }
}
#endif