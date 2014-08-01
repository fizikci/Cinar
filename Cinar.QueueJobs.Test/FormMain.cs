using Cinar.QueueJobs.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinar.QueueJobs.Test
{
    public partial class FormMain : Form
    {
        Database.Database db = null;
        MyWorkerProcess wp = null;

        public FormMain()
        {
            InitializeComponent();

            workersFarm.WorkerProcessType = typeof(MyWorkerProcess);
            workersFarm.Log = (msg) => {
                Console.Items.Add(msg);
            };
            wp = (MyWorkerProcess)Activator.CreateInstance(typeof(MyWorkerProcess));
            db = wp.GetNewDatabaseInstance();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (db.Tables["Worker"] == null || db.GetInt("select count(*) from Worker") == 0)
            {
                db.Save(new Worker() { Name = "Worker 1" });
                db.Save(new Worker() { Name = "Worker 2" });
                db.Save(new Worker() { Name = "Worker 3" });
                db.Save(new Worker() { Name = "Worker 4" });
            }

            workersFarm.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            workersFarm.Stop();
        }
    }
}
