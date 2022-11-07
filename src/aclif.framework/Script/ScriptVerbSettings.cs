using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aclif
{
    public static partial  class Script
    {
        private static ScriptVerbSettings? _settings;

        public static ScriptVerbSettings Settings => _settings ??= new ScriptVerbSettings();

        public class ScriptVerbSettings
        {
            internal ScriptVerbSettings()
            {
            }

            private IEnumerable<string>? _validFileExtensions;
            public IEnumerable<string> ValidFileExtensions => _validFileExtensions ??= AppDomain.CurrentDomain.GetValidScriptFileExtensions();

        }
    }
}
