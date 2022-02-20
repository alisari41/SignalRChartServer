using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SignalRChartServer.Hubs;
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
                await _hubContext.Clients.All.SendAsync("receiveMessage", "Merhaba Babalar.");
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
