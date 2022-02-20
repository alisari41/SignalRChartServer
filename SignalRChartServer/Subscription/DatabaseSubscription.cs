using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SignalRChartServer.Hubs;
using SignalRChartServer.Models;
using TableDependency.SqlClient;

namespace SignalRChartServer.Subscription
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class, new()//T personeller veya satışlar tablosu gelicek
    {
        private IConfiguration _configuration;//appsettings.json dosyasında sql yolunu alıcam
        private SqlTableDependency<T> _tableDependency;
        private IHubContext<SatisHub> _hubContext;

        public DatabaseSubscription(IConfiguration configuration, IHubContext<SatisHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("SQL"), tableName);

            _tableDependency.OnChanged += async (o, e) =>//veritabanında değişiklik olduğunda çalışır
            {
                //SQL  deki veri tabanının verileri çekiliyor
                SatisDbContext contex = new SatisDbContext();
                var data = (from personel in contex.Personellers
                            join satis in contex.Satislars
                                on personel.Id equals satis.PersonelId
                            select new { personel, satis }).ToList();


                List<object> datas = new List<object>();

                var personelIsimleri = data.Select(d => d.personel.Adi).Distinct().ToList();//Tekilleştiriyorum


                personelIsimleri.ForEach(p =>
                {
                    datas.Add(new
                    {
                        name = p,
                        data = data.Where(s => s.personel.Adi == p).Select(s => s.satis.Fiyat).ToList() //Gelicek olan satışlar
                    });
                });



                await _hubContext.Clients.All.SendAsync("receiveMessage", datas);

            };
            _tableDependency.OnError += (o, e) =>
            {

            };
            _tableDependency.Start();
        }


        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }
        //private void _tableDependency_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<T> e)
        //{
        //    throw new NotImplementedException(); 
        //}
    }
}
