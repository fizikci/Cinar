﻿using Cinar.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinar.CMS.DesktopEditor
{
    public partial class FormSettings : Form
    {
        public FormSettings(int index)
        {
            InitializeComponent();

            var s = Settings.Load();

            if (s.Providers.ContainsKey(index))
            {
                editConnectionString.Text = s.ConnectionStrings[index];
                editProvider.Text = s.Providers[index].ToString();
                editSiteAddress.Text = s.SiteAddress[index];
            }
        }

        public string ConnectingString { get { return editConnectionString.Text; } }
        public DatabaseProvider ConnectionProvider { get { return (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), editProvider.Text); } }
        public string SiteAddress { get { return editSiteAddress.Text; } }
    }
}