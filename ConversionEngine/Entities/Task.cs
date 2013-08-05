using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Dts.Projects.VideoConverter.ConversionEngine.Entities
{
    public class Task
    {
        public ConversionEntity Entity { get; private set; }

        public Task(ConversionEntity entity)
        {
            this.Entity = entity;
        }

        public string GetCommand()
        {
            if (Entity == null)
                return null;
            else
                return ComposeCommand();
        }

        private string ComposeCommand()
        {
            StringBuilder command = new StringBuilder();
            command.Append(@"mencoder ");
            command.Append("\"");
            command.Append(Entity.FileName);
            command.Append("\"");
            command.Append("  -channels 6 -ovc xvid -xvidencopts fixed_quant=4 -vf harddup -oac copy ");
            if (!string.IsNullOrEmpty(Entity.SubtitlesFileName))
            {
                command.Append(string.Format(" -sub {0}{1}{0} ", "\"", Entity.SubtitlesFileName));
            }
            command.Append (string.Format (" -o {0}{1}{0} ",  "\"", Entity.ConvertedFileName));
            return command.ToString();
        }

        protected virtual void InternalFinishAction()
        {
            if (File.Exists(Entity.ConvertedFileName))
            {
                if (Entity.OriginalPostconversionDestinationFolder != null)
                {
                    FileInfo originalFile = new FileInfo(Entity.FileName);

                    if (!Directory.Exists(Entity.OriginalPostconversionDestinationFolder))
                    {
                        Directory.CreateDirectory(Entity.OriginalPostconversionDestinationFolder);
                    }

                    string destinationPath = Path.Combine(Entity.OriginalPostconversionDestinationFolder, originalFile.Name);

                    originalFile.MoveTo(destinationPath);
                }
            }
        }

        public void OnFinishAction()
        {
            InternalFinishAction();
        }
    }
}
