using Staples.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Staples.DAL.Helpers
{
    public class XmlDbHelper<T> : IXmlDbHelper<T>
        where T : class
    {
        private string _databasePath;
        private XmlSerializer _serializer;

        public XmlDbHelper()
        {
            string dbName = typeof(T).Name + "Table.xml";

            _databasePath = DbDirectory + "\\" + dbName;

            if (!Directory.Exists(DbDirectory))
                Directory.CreateDirectory(DbDirectory);

            if (!File.Exists(_databasePath))
                File.Create(_databasePath);

            _serializer = new XmlSerializer(typeof(List<T>));
        }

        public async Task<int> AddAsync(T entity)
        {
            return await Task.Run(() =>
            {
                var db = GetDatabase().ToList();
                db.Add(entity);
                return SaveDatabaseAsync(db) ? 1 : 0;

            });
        }

        public async Task<int> RemoveAsync(T entity)
        {
            return await Task.Run(() =>
            {
                var db = GetDatabase().ToList();
                db = db.Where(x => x != entity).ToList();
                return SaveDatabaseAsync(db) ? 1 : 0;
            });
        }

        private IEnumerable<T> GetDatabase()
        {
            List<T> db;
            try
            {
                using (var reader = new StreamReader(_databasePath, Encoding.UTF8))
                {
                    db = (List<T>)_serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                var x = e;
                db = new List<T>();
            }
            return db;
        }

        private bool SaveDatabaseAsync(IEnumerable<T> db)
        {
            try
            {
                string dbAsXml = "";

                using (var stringWriter = new StringWriter())
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                    {
                        _serializer.Serialize(xmlWriter, db);
                        dbAsXml = stringWriter.ToString();
                    }
                }
                File.WriteAllText(_databasePath, dbAsXml);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string DbDirectory
        {
            get => AssemblyDirectory + "\\XmlDb";
        }

        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}