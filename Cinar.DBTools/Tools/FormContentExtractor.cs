using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Menees.DiffUtils;
using Menees.DiffUtils.Controls;

namespace Cinar.DBTools.Tools
{
    public partial class FormContentExtractor : Form
    {
        public FormContentExtractor()
        {
            InitializeComponent();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            Encoding enc = null;
            IList<string> code1 = txtUrl1.Text.DownloadPage(ref enc).Replace("\r", "").Split('\n');
            IList<string> code2 = txtUrl2.Text.DownloadPage(ref enc).Replace("\r", "").Split('\n');

            TextDiff diff = new TextDiff(HashType.CRC32, true, true);
            EditScript script = diff.Execute(code1, code2);

            List<MyEdit> diffList = new List<MyEdit>();
            for (int editIndex = 0; editIndex < script.Count; editIndex += 2)
            {
                Edit edit = script[editIndex];
                string strA = "";
                for (int i = edit.StartA; i < edit.StartA + edit.Length; i++)
                    strA += code1[i] + Environment.NewLine;
                string strB = "";
                for (int i = edit.StartB; i < edit.StartB + edit.Length; i++)
                    strB += code2[i] + Environment.NewLine;
                diffList.Add(new MyEdit
                {
                    Left = strA,
                    Right = strB,
                    Type = edit.Type
                });
            }

            List<MyEdit> mySubList = getLongestNItems(diffList, 5);


            DiffControl diffControl = new DiffControl();
            diffControl.Dock = DockStyle.Fill;
            panel.Controls.Clear();
            panel.Controls.Add(diffControl);

            diffControl.SetData(code1, code2, script);//, strA, strB);
        }

        private static List<MyEdit> getLongestNItems(List<MyEdit> diffList, int limit)
        {
            return diffList.OrderByDescending(m => m.Left.Length + m.Right.Length).Take(limit).ToList();
        }

    }
    public class MyEdit
    {
        public string Left { get; set; }
        public string Right { get; set; }
        public EditType Type { get; set; }
    }
}
