using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Utils.Drawing.Helpers;

namespace Cinar.WinUI
{
    public static class DMT
    {
        public static Dictionary<string, Color> SkinColors = new Dictionary<string, Color>() { 
            {"Black", Color.FromArgb(74,74,74)},
            {"Caramel", Color.FromArgb(140,116,100)},
            {"Money Twins", Color.FromArgb(85,148,217)},
            {"Lilian", Color.FromArgb(101,114,172)},
            {"The Asphalt World", Color.FromArgb(13,130,192)},
            {"iMaginary", Color.FromArgb(101,177,240)},
            {"Blue", Color.FromArgb(73,113,166)}
        };


        public static ServiceProvider provider;
        public static ServiceProvider Provider
        {
            get
            {
                return provider;
            }
        }

        static DMT()
        {
            provider = new ServiceProvider();

            GridLocalizerTr.Active = new GridLocalizerTr();
            LayoutLocalizerTr.Active = new LayoutLocalizerTr();
            LocalizerTr.Active = new LocalizerTr();
            NavBarLocalizerTr.Active = new NavBarLocalizerTr();

            //DevExpress.UserSkins.BonusSkins.Register();
            if (!NativeVista.IsVista)
                DevExpress.Skins.SkinManager.EnableFormSkins();

            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(Cookie.Load().SkinName);
        }
    }
}
