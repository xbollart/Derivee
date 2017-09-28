using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Derivee.TickRecorder.Console
{
    public class DataBaseConnector : IDisposable
    {
        SQLiteConnection _conn;

        public void CreateDb()
        {
            SQLiteConnection.CreateFile("CryptoTick.db");
            string sql = "create table records (ccy text,high real,last real, timestamp int,bid real,vwap real,volume real,low real,ask real,open real)";
            InitConnection();
            var cmd = new SQLiteCommand(sql, _conn);
            cmd.ExecuteNonQuery();
        }

        public void InsertInto()
        {

        }

        public void InitConnection()
        {
            if(_conn == null)
                _conn = new SQLiteConnection("Data Source=CryptoTick.db;Version=3");
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}
