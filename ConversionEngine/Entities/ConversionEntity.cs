using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Dts.Projects.VideoConverter.ConversionEngine.Entities
{
    public class ConversionEntity
    {
        public string FileName { get; set; }

        public string SubtitlesFileName { get; set; }

        public string ConvertedFileName { get; set; }

        public string OriginalPostconversionDestinationFolder { get; set; }

        public override string ToString()
        {
            return String.Format("File: {0}, Folder: {1}, Translate: {2}", Path.GetFileName(FileName), Path.GetDirectoryName(FileName), string.IsNullOrEmpty(SubtitlesFileName)? "No": "Yes");
        }
    }
}
