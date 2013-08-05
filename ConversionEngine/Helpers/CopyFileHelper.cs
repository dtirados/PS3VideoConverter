using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Dts.Projects.VideoConverter.ConversionEngine.Helpers
{
    public static class CopyFileHelper
    {
        public static string SanitizeLanguageName(string currentString)
        {
            string result = null;
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]+", invalidChars);
            result = Regex.Replace(currentString, invalidReStr, string.Empty);
            return result;
        }

        public static bool HasInvalidChars(string currentString)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            Match foundMatch = Regex.Match(currentString, invalidChars);
            return (foundMatch.Captures.Count > 0);
        }
    }
}
