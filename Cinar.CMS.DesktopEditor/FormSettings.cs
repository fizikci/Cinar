using Cinar.CMS.DesktopEditor.Controls;
using Cinar.Database;
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
                try
                {
                    editConnectionString.Text = s.ConnectionStrings[index];
                    editProvider.Text = s.Providers[index].ToString();
                    editSiteAddress.Text = s.SiteAddress[index];
                    editEmail.Text = s.Emails[index];
                    editPassword.Text = s.Passwords[index];

                    try
                    {
                        Database.Database db = Provider.GetDb(index);
                        var dt = db.GetDataTable("select Id, Title from Content where ClassName='Category' order by Title");
                        loadCategories(editCategoryId0, dt);
                        loadCategories(editCategoryId1, dt);
                        loadCategories(editCategoryId2, dt);
                        loadCategories(editCategoryId3, dt);
                        loadCategories(editCategoryId4, dt);
                        loadCategories(editCategoryId5, dt);
                        loadCategories(editCategoryId6, dt);
                        loadCategories(editCategoryId7, dt);
                        loadCategories(editCategoryId8, dt);
                        loadCategories(editCategoryId9, dt);
                    }
                    catch { }

                    if (s.Feed == null || string.IsNullOrWhiteSpace(s.Feed[index]))
                        s.Feed[index] = "|\n|\n|\n|\n|\n|\n|\n|\n|\n|\n";

                    var feeds = s.Feed[index].Replace("\r", "").Split('\n');
                    try
                    {
                        editRSS0.Text = feeds[0].Split('|')[0];
                        editCategoryId0.SelectedIndex = editCategoryId0.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[0].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS1.Text = feeds[1].Split('|')[0];
                        editCategoryId1.SelectedIndex = editCategoryId1.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[1].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS2.Text = feeds[2].Split('|')[0];
                        editCategoryId2.SelectedIndex = editCategoryId2.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[2].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS3.Text = feeds[3].Split('|')[0];
                        editCategoryId3.SelectedIndex = editCategoryId3.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[3].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS4.Text = feeds[4].Split('|')[0];
                        editCategoryId4.SelectedIndex = editCategoryId4.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[4].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS5.Text = feeds[5].Split('|')[0];
                        editCategoryId5.SelectedIndex = editCategoryId5.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[5].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS6.Text = feeds[6].Split('|')[0];
                        editCategoryId6.SelectedIndex = editCategoryId6.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[6].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS7.Text = feeds[7].Split('|')[0];
                        editCategoryId7.SelectedIndex = editCategoryId7.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[7].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS8.Text = feeds[8].Split('|')[0];
                        editCategoryId8.SelectedIndex = editCategoryId8.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[8].Split('|')[1]));
                    }
                    catch { }
                    try
                    {
                        editRSS9.Text = feeds[9].Split('|')[0];
                        editCategoryId9.SelectedIndex = editCategoryId9.Items.Cast<Item>().IndexOf(i => i.Id == Convert.ToInt32(feeds[9].Split('|')[1]));
                    }
                    catch { }
                }
                catch { }
            }
        }

        private void loadCategories(ComboBox editCategoryId, DataTable dt)
        {
            editCategoryId.Items.Clear();

            foreach (DataRow dr in dt.Rows)
                editCategoryId.Items.Add(new Item(dr));
        }

        public string ConnectingString { get { return editConnectionString.Text; } }
        public DatabaseProvider ConnectionProvider { get { return (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), editProvider.Text); } }
        public string SiteAddress { get { return editSiteAddress.Text; } }
        public string Email { get { return editEmail.Text; } }
        public string Password { get { return editPassword.Text; } }
        public string Feed { 
            get 
            {
                string res = "";
                res += editRSS0.Text + "|" + (editCategoryId0.SelectedItem != null ? (editCategoryId0.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS1.Text + "|" + (editCategoryId1.SelectedItem != null ? (editCategoryId1.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS2.Text + "|" + (editCategoryId2.SelectedItem != null ? (editCategoryId2.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS3.Text + "|" + (editCategoryId3.SelectedItem != null ? (editCategoryId3.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS4.Text + "|" + (editCategoryId4.SelectedItem != null ? (editCategoryId4.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS5.Text + "|" + (editCategoryId5.SelectedItem != null ? (editCategoryId5.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS6.Text + "|" + (editCategoryId6.SelectedItem != null ? (editCategoryId6.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS7.Text + "|" + (editCategoryId7.SelectedItem != null ? (editCategoryId7.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS8.Text + "|" + (editCategoryId8.SelectedItem != null ? (editCategoryId8.SelectedItem as Item).Id.ToString() : "") + "\n";
                res += editRSS9.Text + "|" + (editCategoryId9.SelectedItem != null ? (editCategoryId9.SelectedItem as Item).Id.ToString() : "") + "\n";
                return res;
            } 
        }
    }
}
