using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using Cinar.Entities;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using Timer=System.Timers.Timer;
using System.Reflection;
using Cinar.Entities.Standart;
using Cinar.Database;
using System.Configuration;

namespace Cinar.WinUI
{
    public class ServiceProvider
    {
        private User clientUser;

        public User ClientUser
        {
            get { return clientUser; }
        }

        //private Database.IDatabase db;
        public Database.Database Db 
        { 
            get 
            {
                if (CinarContext.Db == null)
                   CinarContext.Db = new Cinar.Database.Database(ConfigurationManager.AppSettings["sqlConnection"], (DatabaseProvider)Enum.Parse(typeof(DatabaseProvider), ConfigurationManager.AppSettings["sqlProvider"]));

                return (Database.Database)CinarContext.Db;
            } 
        }

        internal ServiceProvider()
        {
            this.ListEntityViewHorizontal = true;
        }

        public bool Login()
        {
            if (clientUser==null || clientUser.Id == 0)
            {
                LoginDialog giris = new LoginDialog();
                if (giris.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Entities.CinarContext.Db = Db;

                        clientUser = Db.Read<User>("[UserName]={0} AND [Password]={1}", giris.Username, giris.Password);

                        if (clientUser!=null)
                        {
                            foreach (RoleUser ru in Db.ReadList<RoleUser>(FilterExpression.Create("UserId", CriteriaTypes.Eq, clientUser.Id)))
                                foreach (RoleRight rr in Db.ReadList<RoleRight>(FilterExpression.Create("RoleId", CriteriaTypes.Eq, ru.RoleId)))
                                {
                                    Right right = Db.Read<Right>(rr.RightId);
                                    clientUser.AddRight(right.Name);
                                }

                            Entities.CinarContext.ClientUser = clientUser;

                            return true;
                        }

                        Alert("Tanımsız kullanıcı!");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Alert("Hata: " + ex.Message);
                        return false;
                    }
                }
 
                throw new Exception("Cancel");
            }

            FeedBack("Zaten oturum açılmış durumda");
            return true;
        }
        public void Logout()
        {
            clientUser = null;
        }

        public DialogResult ShowMessage(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return XtraMessageBox.Show(message, "Cinar WinApp", buttons, icon);
        }

        public void Alert(string message)
        {
            ShowMessage(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void FeedBack(string message)
        {
            ShowMessage(message, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool Confirm(string message)
        {
            return ShowMessage(message, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void UpdateEditControlValuesWithEntity(ContainerControl controlContainer, BaseEntity entity)
        {
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
            {
                if (pi.Name == "Item") continue;

                object val = pi.GetValue(entity, null);

                BaseEdit c = controlContainer.Controls["edit" + pi.Name] as BaseEdit;
                if (c == null && controlContainer is LayoutControl)
                    c = (controlContainer as LayoutControl).GetControlByName("edit" + pi.Name) as BaseEdit;
                SetValueOfEditControl(pi, c, val);
            }
        }

        public void SetValueOfEditControl(PropertyInfo pi, BaseEdit c, object val)
        {
            if (c != null)
            {
                if (c is LookUp && pi.PropertyType == typeof(long))
                {
                    LookUp lu = c as LookUp;
                    val = Db.Read(lu.EntityType, (int)val);
                }
                else if (c is CheckEdit)
                {
                    if (pi.PropertyType == typeof(int))
                        val = val.Equals(1) || val.Equals("True");
                    else
                        val = Convert.ChangeType(val, typeof(bool));
                }
                else if (pi.PropertyType.IsEnum && val is string)
                    val = Enum.Parse(pi.PropertyType, val.ToString());
                else if (c is ComboBoxEdit)
                {
                    ComboBoxEdit combo = c as ComboBoxEdit;
                    if (combo.Properties.Items.Count > 0 && combo.Properties.Items[0] is ObjectStringPair)
                    {
                        object o = val;
                        val = combo.Properties.Items.Cast<ObjectStringPair>().FirstOrDefault(i => i.Value.Equals(o));
                    }
                }

                c.EditValue = val;
            }
        }

        public void UpdateEntityWithEditControlValues(ContainerControl controlContainer, BaseEntity entity)
        {
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
            {
                BaseEdit c = controlContainer.Controls["edit" + pi.Name] as BaseEdit;
                if(c==null && controlContainer is LayoutControl)
                    c = (controlContainer as LayoutControl).GetControlByName("edit" + pi.Name) as BaseEdit;
                if (c != null)
                {
                    object val = null;
                    try
                    {
                        val = GetValueOfEditControl(pi, c);
                        pi.SetValue(entity, val, null);
                    }
                    catch
                    {
                        Alert(pi.Name + " alanı için " + c.Name + " kontrolünden alınan değer geçersiz: " + val);
                    }
                }
            }
        }

        public object GetValueOfEditControl(PropertyInfo pi, BaseEdit c)
        {
            object val = null;

            if (c is LookUp && pi.PropertyType == typeof(long))
                val = c.EditValue.GetMemberValue((c as LookUp).ValueFieldName);
            else if (c is CheckEdit)
            {
                if (c.EditValue != null)
                {
                    if (pi.PropertyType == typeof(int))
                        val = ((bool)c.EditValue) ? 1 : 0;
                    else
                        val = c.EditValue;
                }
                else
                    return null;
            }
            else if (pi.PropertyType.IsEnum && c.EditValue is string)
                val = Enum.Parse(pi.PropertyType, c.EditValue.ToString());
            else if (c.EditValue is ObjectStringPair)
                val = (c.EditValue as ObjectStringPair).Value;
            else
                val = c.EditValue;
            val = Convert.ChangeType(val, pi.PropertyType);
            return val;
        }

        public object CloneObject(object obj)
        {
            if (obj == null)
                return null;

            object res = Activator.CreateInstance(obj.GetType());

            foreach (PropertyInfo pi in obj.GetType().GetProperties())
                if (pi.PropertyType.IsValueType || pi.PropertyType == typeof(string))
                    pi.SetValue(res, pi.GetValue(obj, null), null);
                else if (pi.PropertyType.IsSubclassOf(typeof(BaseEntity)))
                    pi.SetValue(res, CloneObject(pi.GetValue(obj, null)), null);

            return res;
        }

        public bool ListEntityViewHorizontal { get; set; }

        private UIMetadata uiMetaData;
        public UIMetadata UIMetaData
        {
            get
            {
                if (uiMetaData == null)
                    uiMetaData = new UIMetadata();
                return uiMetaData;
            }
        }

        public void PopulateGridColumns(List<ColumnDefinition> listCols, GridView gridView)
        {
            if (listCols == null)
                listCols = new List<ColumnDefinition> { 
                    new ColumnDefinition{DisplayName="Id", Name="Id", Width = 100}
                };

            int counter = -1;
            gridView.Columns.Clear();
            foreach (ColumnDefinition columnInfo in listCols)
            {
                if (!columnInfo.Visible)
                    continue;

                GridColumn col = new GridColumn();
                col.Caption = columnInfo.DisplayName;
                col.FieldName = columnInfo.Name;
                col.Name = "col" + columnInfo.Name;
                col.VisibleIndex = ++counter;
                col.AppearanceCell.TextOptions.HAlignment = (DevExpress.Utils.HorzAlignment)Enum.Parse(typeof(DevExpress.Utils.HorzAlignment), columnInfo.HAlign.ToString());
                col.DisplayFormat.FormatType = (DevExpress.Utils.FormatType)Enum.Parse(typeof(DevExpress.Utils.FormatType), columnInfo.FormatType.ToString());
                col.DisplayFormat.FormatString = columnInfo.FormatString;
                if (columnInfo.Width > 0)
                    col.Width = columnInfo.Width;
                if (columnInfo.ColumnEdit != null)
                    col.ColumnEdit = (RepositoryItem)columnInfo.ColumnEdit;

                gridView.Columns.Add(col);
            }
        }


        public T Prompt<T>(string question, T defaultAnswer)
        {
            return PromptDialog.Prompt(question, defaultAnswer);
        }

        public Type GetEntityType(string entityFullName)
        {
            return typeof(BaseEntity).Assembly.GetType(entityFullName);
        }

        public Type GetEntityType(string moduleName, string entityName)
        {
            return typeof(BaseEntity).Assembly.GetType("Cinar.Entities." + moduleName + "." + entityName);
        }
    }
}