using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Dts.Projects.VideoConverter.ConversionEngine.Entities;
using Dts.Projects.VideoConverter.ConversionEngine.Engines;

namespace PS3VideoConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.buttonStart_Click(this, new EventArgs());
        }

        private void SearchForConvertableFiles()
        {
            string startupPath = Application.StartupPath;

            List<string> filePaths = Directory.GetFiles(startupPath).ToList();


            List<string> fileNames = (from fileName in filePaths
                                      where ((fileName.EndsWith(".avi") && !fileName.Contains("[CONVERTED]"))                                      
                                      && !(from convertedFileName in filePaths where convertedFileName.Equals(fileName.Replace(".avi", ".[CONVERTED].avi")) select convertedFileName).Any()
                                      )
                                      select fileName).ToList();

            List<ConversionEntity> conversionEntities = new List<ConversionEntity>();

            foreach (string fileName in fileNames)
            {
                ConversionEntity entity = new ConversionEntity();
                entity.FileName = fileName.Replace(Application.StartupPath,".");
                string subtitlesFileName = fileName.Replace(".avi",".srt");
                if (File.Exists(subtitlesFileName))
                {
                    entity.SubtitlesFileName = fileName.Replace(".avi", ".srt").Replace(Application.StartupPath, ".");
                }
                entity.ConvertedFileName = fileName.Replace(".avi", ".[CONVERTED].avi").Replace(Application.StartupPath, ".");

                entity.OriginalPostconversionDestinationFolder = Application.StartupPath + "\\Originals";
                conversionEntities.Add(entity);                
            }

            foreach (ConversionEntity conversionEntity in conversionEntities)
            {
                Task conversionTask = new Task(conversionEntity);
                OnDemandConversionEngine.ExecuteCommand(conversionTask);
            }
            Application.Exit();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            SearchForConvertableFiles();

            //object aux = System.Configuration.ConfigurationSettings.GetConfig("ConversionEngine");
        }
    }
}