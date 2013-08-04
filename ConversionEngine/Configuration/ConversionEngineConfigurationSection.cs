using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace Dts.Projects.VideoConverter.ConversionEngine.Configuration
{
    public class ConversionEngineConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("Folders")]
        public string Folders
        {
            get
            {
                return (string)this["Folders"];
            }
            set
            {
                this["Folders"] = value;
            }
        }
    }

    public class ConversionEngineFoldersSection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderConfigElement)(element)).Path;
        }

        public FolderConfigElement this[int idx]
        {
            get
            {
                return (FolderConfigElement)BaseGet(idx);
            }
        }
    }

    public class FolderConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("Path", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Path
        {
            get
            {
                return ((string)(base["path"]));
            }
            set
            {
                base["path"] = value;
            }
        }

        [ConfigurationProperty("ForeignLanguage", DefaultValue = "", IsKey = false, IsRequired = false)]
        public bool Value
        {
            get
            {
                return ((bool)(base["foreignLanguage"]));
            }
            set
            {
                base["foreignLanguage"] = value;
            }
        }
    }
}