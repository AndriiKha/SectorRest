using RBSector.UserClient.Models;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace RBSector.UserClient.DataAccessLayer
{
    internal static class Dal
    {
        private static string _dbPath = string.Empty;
        private static string DbPath
        {
            get
            {
                if (string.IsNullOrEmpty(_dbPath))
                {
                    _dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite");
                }

                return _dbPath;
            }
        }

        private static SQLiteConnection DbConnection => new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);

        public static async Task CreateDatabase()
        {
            // Create a new connection
            using (var db = DbConnection)
            {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();

                // Create the table if it does not exist
                db.CreateTable<User>();
                db.GetMapping(typeof(User));

                db.CreateTable<Role>();
                db.GetMapping(typeof(Role));

                if (db.Table<Role>().Select(r => r).ToList().Count == 0)
                {
                    var adminRole = new Role()
                    {
                        FullName = "Admin",
                        Name = "Admin",
                        Value = 0
                    };
                    db.Insert(adminRole);
                }
            }
        }

        public static void Delete(Model entity)
        {
            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();

                db.Delete(entity);
            }
        }

        public static List<Model> GetAll(string tableName)
        {
            var roles = new List<Model>();
            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();
                switch (tableName)
                {
                    case nameof(Role):
                        roles.AddRange(db.Table<Role>().Select(r => r).ToList());
                        break;
                    case nameof(User):
                        roles.AddRange(db.Table<User>().Select(u => u).ToList());
                        break;
                }
            }
            return roles;
        }


        public static Model GetByPrimaryKey(string tableName, int pk)
        {
            Model resModel = null;
            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();
                switch (tableName)
                {
                    case nameof(Role):
                        resModel = db.Table<Role>().FirstOrDefault(r => r.Id == pk);
                        break;
                    case nameof(User):
                        resModel = db.Table<User>().FirstOrDefault(u => u.Id == pk);
                        break;
                }
            }
            return resModel;
        }

        public static User GetUserByLogin(string login)
        {
            User resModel;
            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();
                {
                    resModel = db.Table<User>().FirstOrDefault(u => u.Login == login);
                }
            }
            return resModel;
        }

        public static void InsertOrUpdate(Model entity)
        {
            if (entity == null) return;
            // Create a new connection
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath))
            {
                // Activate Tracing
                db.TraceListener = new DebugTraceListener();
                if (entity.Id == 0)
                {
                    // New
                    db.Insert(entity);
                }
                else
                {
                    // Update
                    db.Update(entity);
                }
            }
        }

    }
}
