using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Staples.DAL.Helpers
{
    public class XMLHelper<T>
        where T : class
    {
        private string _databasePath;
        private XmlSerializer _serializer;

        public XMLHelper(string dbName)
        {
            _databasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + dbName + ".xml";
            _serializer = new XmlSerializer(typeof(T));
        }

        public async Task<bool> AddAsync(T entity)
        {
            var db = GetDatabase();
            db.Add(entity);
            return await SaveDatabaseAsync(db);
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            var db = GetDatabase();
            db = db.Where(x => x != entity).ToList();
            return await SaveDatabaseAsync(db);
        }

        private List<T> GetDatabase()
        {
            List<T> db;

            using (var reader = new FileStream(_databasePath, FileMode.Open))
            {
                db = (List<T>)_serializer.Deserialize(reader);
            }

            return db;
        }

        private async Task<bool> SaveDatabaseAsync(List<T> db)
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

                using (var writer = new StreamWriter(_databasePath))
                {
                    await writer.WriteAsync(dbAsXml);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}