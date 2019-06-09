using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

namespace DataBank
{
    public abstract class SqliteHelper<T>
    {
        private const string Tag = "SqliteHelper:\t";

        private const string database_name = "madhunt_db";

        public string db_connection_string;
        public IDbConnection db_connection;

        public SqliteHelper()
        {
            db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
            Debug.Log("db_connection_string" + db_connection_string);
            db_connection = new SqliteConnection(db_connection_string);
            db_connection.Open();
        }

        ~SqliteHelper()
        {
            db_connection.Close();
        }

        //vitual functions
        public abstract T addData(T item);
        public abstract T getDataById(string id);
        public abstract void deleteDataById(string id);
        public abstract List<T> getAllData();
        public abstract void deleteAllData();
        public abstract T toEntity(IDataReader reader);


        //helper functions
        public IDbCommand getDbCommand()
        {
            return db_connection.CreateCommand();
        }

        public T getDataById(string tableName, string keyId, string id)
        {
            Debug.Log(string.Format("{0}Getting From table: {1} with key: {2} id: {3}", Tag, tableName, keyId, id));

            var dbcmd = getDbCommand();
            dbcmd.CommandText = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", tableName, keyId, id);
            var reader = dbcmd.ExecuteReader();
            if (reader.Read())
            {
                return toEntity(reader);
            }

            throw new NoSuchDataException();
        }

        public List<T> getAllData(string table_name)
        {
            var results = new List<T>();
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "SELECT * FROM " + table_name;
            var reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(toEntity(reader));
            }

            return results;
        }

        public void deleteAllData(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            dbcmd.ExecuteNonQuery();
        }

        public int getNumOfRows(string table_name)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
            return int.Parse(dbcmd.ExecuteReader()[0].ToString());
        }

        public bool exists(string tableName, string keyId, string id)
        {
            try
            {
                var dataById = getDataById(tableName, keyId, id);
                return true;
            }
            catch (NoSuchDataException e)
            {
                return false;
            }

        }

        public void close()
        {
            Debug.Log(Tag + "DB Connection closing..");
            db_connection.Close();
        }
    }
}