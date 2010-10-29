using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Cinar.Entities.Standart;

namespace Cinar.WinUI
{
    public class UIMetadata
    {
        public List<EditFormAttribute> EditForms { get; set; }

        public UIMetadata()
        {
            EditForms = new List<EditFormAttribute>();
            string plugIns = ConfigurationManager.AppSettings["PlugIns"];
            if (string.IsNullOrEmpty(plugIns))
                throw new Exception("Lütfen konfigürasyon dosyasına pluginleri ekleyiniz.");
            //string plugInsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "").Replace("/", "\\"));
            foreach (string plugInName in plugIns.SplitWithTrim(','))
            {
                string plugInPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), plugInName);
                try
                {
                    Assembly plugInAssembly = Assembly.LoadFile(plugInPath);
                    foreach (Type type in plugInAssembly.GetTypes())
                    {
                        if (type != typeof(ListEntity) && !type.IsInterface && type.GetInterface("IInterpressForm") != null)
                        {
                            EditFormAttribute editForm = (EditFormAttribute)type.GetAttribute(typeof(EditFormAttribute));
                            editForm.FormType = type;
                            EditForms.Add(editForm);
                        }
                    }
                }
                catch 
                {
                }
            }
        }

        public FormEntity CreateFormFor(Type entityType, BaseEntity entity)
        {
            if (entity == null)
                entity = (BaseEntity)Activator.CreateInstance(entityType);

            foreach (var item in EditForms)
                if (item.EntityType == entity.GetType())
                    return new FormEntity((IEntityEditControl)Activator.CreateInstance(item.FormType), entity, item.DisplayName.Replace("ler", "").Replace("lar", ""));

            return null;
        }

        public FormEntity CreateFormFor(BaseEntity entity)
        {
            Type entityType = entity.GetType();
            return CreateFormFor(entityType, entity);
        }
        public DialogResult ShowDialog(BaseEntity entity)
        {
            FormEntity fe = CreateFormFor(entity);
            if (fe == null)
                throw new Exception(entity.GetType().Name + " adlı entitiye ilişkin form bulunamadı!");
            return fe.ShowDialog();
        }
    }
}