using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;
using System.Windows;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Data.Common;

namespace Market.Model
{
    internal class DataBaseConnection
    {
        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqliteConnection;

            sqliteConnection = new SQLiteConnection("Data Source = Data//MarketDB.db; Version = 3; New = True; Compress = True; ");

            try
            {
                sqliteConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sqliteConnection;
        }

        public void ReadData(ObservableCollection<Waifu> waifu)
        {
            SQLiteDataReader reader;
            SQLiteCommand sqliteCommand;

            SQLiteConnection sqliteConnection = CreateConnection();

            sqliteCommand = sqliteConnection.CreateCommand();
            sqliteCommand.CommandText = "SELECT * FROM Waifu";

            reader = sqliteCommand.ExecuteReader();

            if ( reader.HasRows)
            {
                while ( reader.Read() )
                {
                    byte[] img = (byte[])reader["waifuImg"];
                    waifu.Add(new Waifu(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetFloat(3), reader.GetString(4), (byte[])reader.GetValue(5)));
                }
            }

            sqliteConnection.Close();
        }
    }
}
