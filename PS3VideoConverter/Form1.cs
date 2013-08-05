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
using Dts.Projects.VideoConverter.ConversionEngine.Helpers;

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
            FeedFilesListBox();
        }

        private void FeedFilesListBox()
        {
            string startPath = Application.StartupPath;
            DirectoryInfo rootFolder = new DirectoryInfo(@"D:\Series");
            List<ConversionEntity> entities = SearchForConvertableFiles(rootFolder);
            checkedListBoxFiles.Items.Clear();
            foreach (ConversionEntity entity in entities)
            {
                checkedListBoxFiles.Items.Add(entity);
            }
        }

        private List<ConversionEntity> SearchForConvertableFiles(DirectoryInfo directory)
        {
            List<ConversionEntity> conversionEntities = new List<ConversionEntity>();

            foreach (DirectoryInfo info in directory.GetDirectories())
            {
                if (!info.Name.Equals("Originals"))
                {
                    conversionEntities.AddRange(SearchForConvertableFiles(info));
                }
            }

            List<string> filePaths = Directory.GetFiles(directory.FullName).ToList();

            List<string> fileNames = (from fileName in filePaths
                                      where ((fileName.EndsWith(".avi") && !fileName.Contains("[CONVERTED]"))                                      
                                      && !(from convertedFileName in filePaths where convertedFileName.Equals(fileName.Replace(".avi", ".[CONVERTED].avi")) select convertedFileName).Any()
                                      )
                                      select fileName).ToList();            

            foreach (string fileName in fileNames)
            {
                if (!CopyFileHelper.HasInvalidChars(fileName))
                {
                    ConversionEntity entity = new ConversionEntity();
                    entity.FileName = fileName;//.Replace(Application.StartupPath,".");
                    string subtitlesFileName = fileName.Replace(".avi", ".srt");
                    if (File.Exists(subtitlesFileName))
                    {
                        entity.SubtitlesFileName = fileName.Replace(".avi", ".srt").Replace(Application.StartupPath, ".");
                    }
                    else
                    {
                        if (!directory.Name.Equals("ES"))
                            continue;
                    }
                    entity.ConvertedFileName = fileName.Replace(".avi", ".[CONVERTED].avi").Replace(Application.StartupPath, ".");

                    entity.OriginalPostconversionDestinationFolder = directory.FullName + "\\Originals";
                    conversionEntities.Add(entity);
                }
            }
            return conversionEntities;
        }

        private void btnStartConversion_Click(object sender, EventArgs e)
        {
            ConvertSelectedFiles();
        }

        private void ConvertSelectedFiles()
        {
            listBoxConverted.Items.Clear();
            foreach (ConversionEntity entity in checkedListBoxFiles.SelectedItems)
            {
                Task conversionTask = new Task(entity);
                try
                {
                    OnDemandConversionEngine.ExecuteCommand(conversionTask);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            FeedFilesListBox();
        }
    }
}