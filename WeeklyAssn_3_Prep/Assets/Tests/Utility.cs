using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Tests
{
    static class Utility
    {
        static internal Rootobject InitializeConfigValues()
        {
            string curRuntimeDir = Directory.GetCurrentDirectory();
            string inputFilename = Path.Combine(curRuntimeDir, @"Assets\Tests\runtimeParams.json");
#if UNITY_EDITOR_OSX
            inputFilename = Path.Combine(curRuntimeDir, @"Assets/Tests/runtimeParams.json");
#endif
            string contents = File.ReadAllText(inputFilename);
            return JsonUtility.FromJson<Rootobject>(contents);
        }
    }
}
