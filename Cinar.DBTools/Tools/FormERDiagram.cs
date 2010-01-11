using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cinar.Database;
using Cinar.Extensions;
using Cinar.UICommands;

namespace Cinar.DBTools.Tools
{
    public partial class FormERDiagram : Form
    {
        CommandManager cmdMan = new CommandManager();

        public FormERDiagram()
        {
            InitializeComponent();

            cmdMan.Commands = new CommandCollection(){
                new Command {
                    Execute = cmdNewSchema,
                    Triggers = new List<CommandTrigger>(){
                        new CommandTrigger{ Control = menuNew},
                        //new CommandTrigger{ Control = btnNewConnection},
                    }
                }
            };
            cmdMan.SetCommandTriggers();
            //cmdMan.SetCommandControlsVisibility();
            cmdMan.SetCommandControlsEnable();
        }

        Schema currentSchema;

        private void cmdNewSchema(string arg)
        {
            SelectTableDialog std = new SelectTableDialog();
            if (std.ShowDialog() == DialogResult.OK)
            {
                currentSchema = createNewSchema(std.SelectedTables);
                correctPanelSize();
                panelPaint();
            }
        }

        private Schema createNewSchema(List<Table> tables)
        {
            Schema schema = new Schema();
            schema.Name = Provider.Database.Name + " Schema";
            addTablesToSchema(schema, tables);
            return schema;
        }

        private void addTablesToSchema(Schema schema, List<Table> tables)
        {
            for (int i=0; i<tables.Count; i++)
            {
                Table tbl = tables[i];
                TableView tv = new TableView();
                tv.Position = new Point(i * (Schema.Def_TableWidth + 20) + 20, 20);
                tv.Size = new Size(Schema.Def_TableWidth, Schema.Def_TitleHeight + tbl.Fields.Count * Schema.Def_FieldHeight);
                tv.TableName = tbl.Name;

                schema.Tables.Add(tv);
            }
        }

        private void correctPanelSize()
        {
            if(currentSchema!=null)
                panel.Size = currentSchema.CalculateTotalSize();
        }

        private void panelPaint()
        {
            if (currentSchema != null)
                currentSchema.Draw(panel.CreateGraphics(), panel.Width, panel.Height);
        }

        TableView selectedTV = null;
        bool dragging = false;
        Size dragDelta;

        private void panelOnPaint(object sender, PaintEventArgs e)
        {
            panelPaint();
        }

        private void panelMouseDown(object sender, MouseEventArgs e)
        {
            if (currentSchema != null)
            {
                TableView tv = currentSchema.HitTest(e.Location);
                if (tv != null)
                {
                    if (selectedTV != null)
                        selectedTV.Selected = false;
                    selectedTV = tv;
                    dragDelta = (Size)(e.Location - (Size)selectedTV.Position);
                    dragging = true;
                    selectedTV.Selected = true;
                }
            }
        }

        private void panelMouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                selectedTV.Position = e.Location - dragDelta;
                correctPanelSize();
                panelPaint();
            }
        }

        private void panelMouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            if (selectedTV != null)
                propertyGrid.SelectedObject = Provider.Database.Tables[selectedTV.TableName];
        }
    }

    public class Schema
    {
        public static int Def_TableWidth = 100;
        public static int Def_TitleHeight = 20;
        public static int Def_FieldHeight = 20;

        public string Name { get; set; }
        public List<TableView> Tables { get; set; }
        public List<ConnectionLine> ConnectionLines { get; set; }

        public Schema()
        {
            Tables = new List<TableView>();
            ConnectionLines = new List<ConnectionLine>();
        }

        public Size CalculateTotalSize()
        {
            int maxX1 = Tables.Max(tv => tv.Position.X + tv.Size.Width);
            int maxY1 = Tables.Max(tv => tv.Position.Y + tv.Size.Height);

            int maxX2 = ConnectionLines.Count > 0 ? ConnectionLines.Max(cl => cl.Corners.Max(p => p.X)) : 0;
            int maxY2 = ConnectionLines.Count > 0 ? ConnectionLines.Max(cl => cl.Corners.Max(p => p.Y)) : 0;

            return new Size(maxX1 > maxX2 ? maxX1 : maxX2, maxY1 > maxY2 ? maxY1 : maxY2) + new Size(20, 20);
        }

        internal void Draw(Graphics graphics, int width, int height)
        {

            Bitmap offScreenBmp = new Bitmap(width, height);
            Graphics offScreenDC = Graphics.FromImage(offScreenBmp);

            foreach (TableView tv in this.Tables)
                tv.Draw(offScreenDC);
            foreach (ConnectionLine cl in this.ConnectionLines)
                cl.Draw(offScreenDC);

            graphics.FillRectangle(Brushes.White, new Rectangle(0,0, width, height));
            graphics.DrawImage(offScreenBmp, 0, 0);
        }

        public TableView HitTest(Point point)
        {
            foreach (TableView tv in this.Tables)
                if (tv.Position.X <= point.X && tv.Position.Y <= point.Y && tv.Position.X + tv.Size.Width >= point.X && tv.Position.Y + Def_TitleHeight >= point.Y)
                    return tv;
            return null;
        }
    }

    public class TableView
    {
        public string TableName { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
        public bool Selected;

        internal void Draw(Graphics graphics)
        {
            //graphics.FillRectangle(Brushes.White, new Rectangle(Position + new Size(1, 1), Size - new Size(2, 2)));
            graphics.DrawRectangle(Pens.Black, new Rectangle(Position, Size));
            Rectangle rectTitle = new Rectangle(Position + new Size(1, 1), new Size(Size.Width - 1, Schema.Def_TitleHeight));
            graphics.FillRectangle(Selected ? SystemBrushes.ActiveCaption : SystemBrushes.InactiveCaption, rectTitle);
            graphics.DrawString(this.TableName, Control.DefaultFont, Brushes.White, rectTitle.ToRectangleF(), GetTitleStringFormat());
            
            Table table = Provider.Database.Tables[this.TableName];
            for (int i = 0; i < table.Fields.Count; i++ )
            {
                Field field = table.Fields[i];
                Rectangle rectField = new Rectangle(Position + new Size(1, 1 + Schema.Def_TitleHeight + Schema.Def_FieldHeight * i), new Size(Size.Width - 2, Schema.Def_FieldHeight));
                StringFormat sf = GetFieldStringFormat();
                graphics.DrawString(field.Name, Control.DefaultFont, Brushes.Black, rectField.ToRectangleF(), sf);
            }
        }

        private StringFormat GetTitleStringFormat()
        {
            return new StringFormat
              {
                  Alignment = StringAlignment.Center,
                  LineAlignment = StringAlignment.Center,
                  FormatFlags = StringFormatFlags.NoWrap
              };
        }

        private StringFormat GetFieldStringFormat()
        {
            return new StringFormat
              {
                  Alignment = StringAlignment.Near,
                  LineAlignment = StringAlignment.Center,
                  FormatFlags = StringFormatFlags.NoWrap
              };
        }
    }

    public class ConnectionLine
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public List<Point> Corners { get; set; }

        public ConnectionLine()
        {
            Corners = new List<Point>();
        }

        internal void Draw(Graphics graphics)
        {
            //throw new NotImplementedException();
        }
    }
}
