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

        public void InsertInto(MarketData data,string pair)
        {
            InitConnection();
            SQLiteTransaction trans;
            string sqlQuery = "INSERT INTO records (ccy,last,high,timestamp,bid,vwap,volume,low,ask,open) VALUES";
            sqlQuery += "(@ccy,@last,@high,@timestamp,@bid,@vwap,@volume,@low,@ask,@open)";

            SQLiteCommand cmd = new SQLiteCommand(sqlQuery);
            cmd.Parameters.AddWithValue("@ccy", pair);
            cmd.Parameters.AddWithValue("@last", data.last);
            cmd.Parameters.AddWithValue("@high", data.high);
            cmd.Parameters.AddWithValue("@timestamp", data.timestamp);
            cmd.Parameters.AddWithValue("@bid", data.bid);
            cmd.Parameters.AddWithValue("@vwap",data.vwap );
            cmd.Parameters.AddWithValue("@volume", data.volume);
            cmd.Parameters.AddWithValue("@low", data.low);
            cmd.Parameters.AddWithValue("@ask", data.ask);
            cmd.Parameters.AddWithValue("@open", data.open);

            cmd.Connection = _conn;
            _conn.Open();
            trans = _conn.BeginTransaction();
            int retval = 0;

            try
            {
                retval = cmd.ExecuteNonQuery();
                if (retval == 1)
                {
                    System.Console.WriteLine("Row inserted");
                }
                else
                {
                    System.Console.WriteLine("Row not inserted");
                }
            }
            catch(Exception ex)
            {
                trans.Rollback();
            }
            finally
            {
                trans.Commit();
                cmd.Dispose();
                _conn.Close();
            }
        }

        public void InitConnection()
        {
            if(_conn == null)
                _conn = new SQLiteConnection("Data Source=database.sqlite;Version=3");
        }

        public void Dispose()
        {
            _conn.Close();
        }
    }
}
