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

        private void btnAddFindLinksJobs_Click(object sender, EventArgs e)
        {
            db.Execute(() =>
            {
                List<int> workerIds = db.GetList<int>("select Id from BaseWorker order by Id");
                int counter = 0;
                foreach (string url in URLS.Replace("\r", "").Split('\n'))
                    wp.AddJob(db, workerIds[counter++ % workerIds.Count], "FindLinks", url);
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (db.Tables["BaseWorker"] == null || db.GetInt("select count(*) from BaseWorker") == 0)
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


        private static string URLS = @"http://www.haber7.com
http://www.ntvmsnbc.com
http://www.haberturk.com
http://www.cnnturk.com.tr
http://www.ahaber.com.tr
http://www.ensonhaber.com
http://www.objektifhaber.com
http://www.haberx.com
http://www.gazeteport.com
http://www.internethaber.com
http://www.t24.com.tr
http://www.aktifhaber.com
http://www.abhaber.com
http://www.ajanshaber.com
http://www.acikgazete.com
http://www.aygazete.com
http://www.bianet.org
http://www.bigpara.com
http://www.gazeteci.tv
http://www.gercekgundem.com
http://www.eurovizyon.co.uk
http://www.f5haber.com
http://seksihaber.com
http://www.haber3.com
http://www.haberler.com
http://www.eurovizyon.co.uk
http://www.f5haber.com
http://www.habervitrini.com
http://www.habervaktim.com
http://www.hurhaber.com
http://www.imedya.tv
http://www.iyibilgi.com
http://www.kanaldhaber.com.tr
http://www.samanyoluhaber.com
http://www.mansethaber.com
http://www.aljazeera.com.tr
http://www.moralhaber.net
http://haber.mynet.com
http://www.memurlar.net
http://www.odatv.com
http://www.netgazete.com
http://www.rotahaber.com
http://www.dipnot.tv
http://www.pressturk.com
http://www.aa.com.tr
http://www.ihlassondakika.com
http://www.dha.com.tr
http://www.cihan.com.tr
http://www.sansursuz.com
http://www.sonsayfa.com
http://www.sondakika.com
http://www.timeturk.com
http://www.tgrthaber.com.tr
http://www.turktime.com
http://www.yazete.com
http://www.wsj.com.tr
http://www.haberedikkat.com
http://www.gazetea24.com
http://www.ulkehaber.com
http://www.yirmidorthaber.com
http://www.agos.com.tr
http://www.aksam.com.tr
http://www.anayurtgazetesi.com
http://www.cumhuriyet.com.tr
http://www.birgun.net
http://www.bugun.com.tr
http://www.dunyagazetesi.com.tr
http://efsanefotospor.com
http://www.evrensel.net
http://www.fanatik.com.tr
http://www.fotomac.com.tr
http://www.gunes.com
http://htgazete.com
http://www.hurriyet.com.tr
http://www.turkishdailynews.com
http://www.milligazete.com.tr
http://www.milliyet.com.tr
http://www.ortadogugazetesi.net
http://www.posta.com.tr
http://www.radikal.com.tr
http://www.sabah.com.tr
http://www.sozcu.com.tr
http://www.stargazete.com
http://www.takvim.com.tr
http://www.turkiyegazetesi.com
http://www.taraf.com.tr
http://www.ticaretsicil.gov.tr
http://www.todayszaman.com
http://www.gazetevatan.com
http://www.yeniakit.com.tr
http://www.yeniasir.com.tr
http://www.yeniasya.com.tr
http://www.yenimesaj.com.tr
http://www.yenicaggazetesi.com.tr
http://www.yenisafak.com.tr
http://www.zaman.com.tr";
    }
}
