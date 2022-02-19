using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
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

        public DatabaseSubscription(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("SQL"), tableName);
            _tableDependency.OnChanged += (o, e) =>//veritabanında değişiklik olduğunda çalışır
            {

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
