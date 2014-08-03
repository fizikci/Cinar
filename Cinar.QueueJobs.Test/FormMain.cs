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
        private static Type workerProcessType = typeof(MyWorkerProcess);

        public FormMain()
        {
            InitializeComponent();

            workersFarm.WorkerProcessType = workerProcessType;
            workersFarm.Log = (msg) => {
                Console.Items.Add(msg);
            };
            wp = (MyWorkerProcess)Activator.CreateInstance(workerProcessType);
            db = wp.GetNewDatabaseInstance();
        }

        private static Dictionary<int, List<SiteUrlFilter>> urlFilters = null;
        public static Dictionary<int, List<SiteUrlFilter>> GetUrlFilters()
        {
            if (urlFilters == null)
            {
                var filters = ((MyWorkerProcess)Activator.CreateInstance(workerProcessType)).GetNewDatabaseInstance().ReadList<SiteUrlFilter>("select * from SiteUrlFilter order by JobDefinitionId, Url");
                urlFilters = new Dictionary<int, List<SiteUrlFilter>>();
                foreach (var filter in filters)
                {
                    if (!urlFilters.ContainsKey(filter.JobDefinitionId))
                        urlFilters.Add(filter.JobDefinitionId, new List<SiteUrlFilter>());
                    urlFilters[filter.JobDefinitionId].Add(filter);
                }
            }
            return urlFilters;
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
