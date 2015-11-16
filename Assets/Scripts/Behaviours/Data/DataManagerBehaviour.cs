using Mono.Data.Sqlite;
using ProjectXyz.Data.Interface;
using ProjectXyz.Data.Resources.Interface;
using ProjectXyz.Data.Resources.Sql;
using UnityEngine;

namespace Assets.Scripts.Behaviours.Data
{
    public sealed class DataManagerBehaviour : 
        MonoBehaviour, IDataManagerBehaviour
    {
        #region Fields
        private IDatabase _database;
        private IResourcesDataManager _resourcesDataManager;
        #endregion

        #region Properties
        public IResourcesDataManager ResourcesDataManager
        {
            get
            {
                if (_resourcesDataManager == null)
                {
                    _resourcesDataManager = CreateResourceDataManager();
                }

                return _resourcesDataManager;
            }
        }
        #endregion

        #region Methods
        private IResourcesDataManager CreateResourceDataManager()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };

            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            connection.Open();

            _database = SqlDatabase.Create(connection, true);
            return SqlResourcesDataManager.Create(_database);
        }

        private void Destroy()
        {
            if (_database != null)
            {
                _database.Close();
                _database.Dispose();
            }
        }
        #endregion
    }
}
