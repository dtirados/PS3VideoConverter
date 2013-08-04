using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dts.Projects.VideoConverter.ConversionEngine.Entities
{
    public class ConversionEntity
    {
        public string FileName { get; set; }

        public string SubtitlesFileName { get; set; }

        public string ConvertedFileName { get; set; }

        public string OriginalPostconversionDestinationFolder { get; set; }
    }
}
