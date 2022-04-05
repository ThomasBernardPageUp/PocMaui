using PocMaui.Repositories.Interfaces;
using SQLite;

namespace PocMaui.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly SQLiteConnection _connection;
        public Repository()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "entities.db");
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<T>();
        }

        public void Clear()
        {
            Type type = typeof(T);
            string table = type.Name;
            _connection.Execute($"DELETE FROM {table}");
        }

        public void Delete(T value)
        {
            _connection.Delete(value);
        }

        public T GetById(int id)
        {
            return _connection.Find<T>(id);
        }

        public List<T> Get()
        {
            return _connection.Table<T>().ToList();
        }

        public T Insert(T value)
        {
            _connection.Insert(value);
            return value;
        }

        public T Update(T value)
        {
            _connection.Update(value);
            return value;
        }
    }
}
