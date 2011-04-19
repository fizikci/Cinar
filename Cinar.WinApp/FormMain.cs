using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Cinar.UICommands;
using System.Linq;
using DevExpress.XtraNavBar;
using Cinar.WinUI;
using Cinar.Entities.Standart;

namespace Cinar.WinApp
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        CommandManager cmdMan = new CommandManager();
        public ServiceProvider Provider
        {
            get
            {
                return DMT.Provider;
            }
        }
        public FormMain()
        {
            InitializeComponent();
            this.Text = "Çýnar WinApp";
            initializeForm();
        }
        private void initializeForm()
        {
            #region commands
            cmdMan.Commands = new CommandCollection() { 
                new Command {
                    Execute = cmdExit,
                    Trigger = new CommandTrigger { Control = menuExit}
                },
                new Command {
                    Execute = cmdSetSkinForUser,
                    // triggerlar aþaðýda ekleniyor...
                },
                new Command { 
                    Execute = cmdShowAboutDialog,
                    Trigger = new CommandTrigger { Control = menuAbout}
                },
                new Command { 
                    Execute = cmdChangeView,
                    Trigger = new CommandTrigger { Control = menuChangeView},
                    IsEnabled = () => ActiveMdiChild is ListEntity
                },
            };
            #endregion

            // init skins
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Provider.User.UISkinName);

            addModuleLinksToNavBarAndMenu();
            addSkinsToMenu();

            barManager.ForceInitialize();

            cmdMan.SetCommandTriggers();
            cmdMan.ControlHasNoVisibleProperty = (Component cmp, bool val) =>
            {
                if (cmp is BarItem)
                {
                    (cmp as BarItem).Visibility = val ? BarItemVisibility.Always : BarItemVisibility.Never;
                }
            };
            cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();

            //Tracer tracer = new Tracer(navBar, (o1,o2,str,arg)=>{
            //    Console.WriteLine("{0} {1} {2} {3}", o1, o2, str, arg);
            //});
            //tracer.HookAllEvents();

            Application.Idle += appIdleForChildCommandMans;
        }

        private void addSkinsToMenu()
        {
            foreach (DevExpress.Skins.SkinContainer cnt in DevExpress.Skins.SkinManager.Default.Skins)
            {
                BarButtonItem item = new BarButtonItem(barManager, cnt.SkinName);
                menuSkins.AddItem(item);

                cmdMan.Commands["cmdSetSkinForUser"].Triggers.Add(new CommandTrigger
                                                                      {
                                                                          Control = item,
                                                                          Argument = cnt.SkinName
                                                                      });
            }
        }
        private void addModuleLinksToNavBarAndMenu()
        {
            navBar.Items.Clear();
            navBar.Groups.Clear();

            foreach (string category in Provider.UIMetaData.EditForms.OrderBy(f=>f.CategoryName).Select(f => f.CategoryName).Distinct())
            {
                BarSubItem menuFormCat = null;
                NavBarGroup navBarCat = null;

                foreach (EditFormAttribute editForm in Provider.UIMetaData.EditForms.Where(f => f.CategoryName == category).OrderBy(f=>f.DisplayName))
                {
                    if (string.IsNullOrEmpty(editForm.DisplayName))
                        continue;

                    if (!DMT.Provider.ClientUser.HasRight(editForm.RequiredRight))
                        continue;

                    if (menuFormCat == null)
                    {
                        menuFormCat = new BarSubItem();
                        barManager.Items.Add(menuFormCat);
                        menuCommands.LinksPersistInfo.Add(new LinkPersistInfo(menuFormCat));
                        menuFormCat.Caption = category;
                    }

                    BarButtonItem item = new BarButtonItem(barManager, editForm.DisplayName);
                    menuFormCat.LinksPersistInfo.Add(new LinkPersistInfo(item));
					if(!string.IsNullOrEmpty(editForm.ImageKey))
						item.Glyph = (Image)Cinar.WinUI.Properties.Resources.ResourceManager.GetObject(editForm.ImageKey);
                    item.Tag = editForm;

                    if (navBarCat == null)
                    {
                        navBarCat = new NavBarGroup();
                        navBarCat.Caption = category;
                        navBar.Groups.Add(navBarCat);
                    }

                    NavBarItem item2 = new NavBarItem(editForm.DisplayName);
                    navBarCat.ItemLinks.Add(new NavBarItemLink(item2));
					if (!string.IsNullOrEmpty(editForm.ImageKey))
                        item2.SmallImage = (Image)Cinar.WinUI.Properties.Resources.ResourceManager.GetObject(editForm.ImageKey);
                    item2.Tag = editForm;

                    cmdMan.Commands.Add(
                        new Command
                            {
                                Execute = cmdOpenForm,
                                DisplayName = editForm.DisplayName,
                                Triggers = new List<CommandTrigger>{
                                       new CommandTrigger() {Control=item, Argument = editForm.FormType.FullName },
                                       new CommandTrigger() {Control=item2, Argument = editForm.FormType.FullName, Event="LinkClicked" }
                                   }
                            });
                }
            }
        }

        #region commands
        private void cmdExit(string arg)
        {
            this.Close();
        }
        private void cmdSetSkinForUser(string skinName)
        {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            barManager.GetController().PaintStyleName = "Skin";

            Cookie c = Cookie.Load();
            c.SkinName = skinName;
            c.Save();

            //Provider.User.UISkinName = skinName;
            //Provider.User.Save();
        }
        private void cmdShowAboutDialog(string arg)
        {
            //AboutForm form = new AboutForm();
            //form.ShowDialog();
        }
        private void cmdChangeView(string arg)
        {
            ListEntity le = (ListEntity)ActiveMdiChild;
            le.ChangeView();
        }
        Dictionary<ICinarForm, CommandManager> childCommandMans = new Dictionary<ICinarForm, CommandManager>();
        private void cmdOpenForm(string arg)
        {
            EditFormAttribute efa = DMT.Provider.UIMetaData.EditForms.Find(e => e.FormType != null && e.FormType.FullName == arg);
            if (efa != null)
            {
                ICinarForm form = null;
                if (efa.FormType.GetInterface("IEntityEditControl") != null)
                    form = new ListEntity((IEntityEditControl)Activator.CreateInstance(efa.FormType));
                else
                    form = (ICinarForm)Activator.CreateInstance(efa.FormType);
                (form as Form).Text = ((EditFormAttribute)cmdMan.LastSender.GetMemberValue("Tag")).DisplayName;

                CommandManager cmdManChild = new CommandManager();
                cmdManChild.Commands.AddRange(form.GetCommands());
                childCommandMans[form] = cmdManChild;
                (form as Form).WindowState = FormWindowState.Maximized;
                (form as Form).FormClosing += new FormClosingEventHandler(form_FormClosing);
                form.Initialize(cmdManChild);
                cmdManChild.SetCommandTriggers();
                (form as Form).MdiParent = this;
                (form as Form).Show();

                // add usage report
                try
                {
                    DMT.Provider.Db.Save(new UsageReport { UsageType1 = "OpenForm", UsageType2 = efa.FormType.Name, InsertUserId=DMT.Provider.ClientUser.Id });
                }
                catch
                {
                }
            }
        }
        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            childCommandMans.Remove((ICinarForm)sender);
        }
        private void appIdleForChildCommandMans(object sender, EventArgs arg)
        {
            foreach (var item in childCommandMans)
            {
                item.Value.SetCommandControlsEnable();
                item.Value.SetCommandControlsVisibility();
            }
        }
        #endregion

        public string StatusBarText
        {
            get
            {
                return statusBarStaticItem.Caption;
            }
            set
            {
                statusBarStaticItem.Caption = value;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (Provider.ClientUser != null && Provider.ClientUser.Id != 0)
                Provider.Logout();
        }

    }
}