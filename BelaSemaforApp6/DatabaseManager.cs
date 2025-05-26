using BelaSemaforApp6.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelaSemaforApp6
{
    internal class DatabaseManager
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "belaDb.db3");
        readonly SQLiteConnection database;

        public DatabaseManager()
        {
            database = new SQLiteConnection(dbPath);
            database.CreateTable<AppSettingsModel>();
            database.CreateTable<GameSettingsModel>();
        }
    }
}
