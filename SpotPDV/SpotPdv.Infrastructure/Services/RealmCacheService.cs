using Newtonsoft.Json;
using Realms;
using SpotPdv.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotPdv.Infrastructure.Services
{
    public class DataBaseService : IDataBaseService
    {
        private Realm _realm;
        private string _jsonData;
        private static RealmConfiguration _configuracaoRealm;
        private static string _dBPath;
        private string _mainFolder;
        private static readonly string _dirSeparator = Path.DirectorySeparatorChar.ToString();

        public DataBaseService()
        {
            try
            {
                _mainFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var applicattionSubFolder = "SpotPdv";
                var fullFolderPath = Path.Combine(_mainFolder, applicattionSubFolder);

                if (!Directory.Exists(fullFolderPath))
                {
                    Directory.CreateDirectory(fullFolderPath);
                }

                _dBPath = Path.Combine(fullFolderPath, "SpotPdv.realm");

                _configuracaoRealm = RealmSetup();
                _realm = Realm.GetInstance(_configuracaoRealm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing Realm database: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }
        public async Task<T> GetFromCache<T>(string key)
        {
            try
            {
                _realm = Realm.GetInstance(_configuracaoRealm);
                var _persistedObject = _realm.All<DataBaseModel>().First(p => p.Key == key);

                if (_persistedObject != null)
                {
                    _jsonData = _persistedObject.JsonData;
                    return GetObject<T>();
                }
                else
                {
                    return default(T); // Ou outra manipulação de erro, caso necessário
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
        public async Task InsertObjectToCache<T>(string key, T originalObject)
        {
            _realm = Realm.GetInstance(_configuracaoRealm);
            _realm.Write(() =>
            {
                SetObject(originalObject);

                _realm.Add(new DataBaseModel
                {
                    JsonData = _jsonData,
                    Key = key
                }, update: true);
            });


        }
        private static RealmConfiguration RealmSetup()
        {
            var config = new RealmConfiguration(_dBPath);

            config.SchemaVersion = 1; // Versão do esquema do Realm
            config.Schema = new[] { typeof(DataBaseModel) }; // Lista das classes de objetos Realm que serão gerenciadas pelo Realm
            return config;
        }
        private T GetObject<T>()
        {
            var teste = JsonConvert.DeserializeObject<T>(_jsonData);
            return teste;
        }
        private void SetObject<T>(T obj)
        {
            _jsonData = JsonConvert.SerializeObject(obj);
        }
    }

    public class DataBaseModel : RealmObject
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string JsonData { get; set; }
    }
}
