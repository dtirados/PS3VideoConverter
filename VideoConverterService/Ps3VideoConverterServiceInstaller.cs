using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace VideoConverterService
{
    [RunInstaller(true)]
    public partial class Ps3VideoConverterServiceInstaller : System.Configuration.Install.Installer
    {
        public Ps3VideoConverterServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
