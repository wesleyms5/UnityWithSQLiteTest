using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.SqliteClient;

public class DBConnection : MonoBehaviour
{

    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    private string dbFile = "URI=File:RenanSQLiteDB.db";


    void Start()
    {
        Connection();
    }

    private void Connection()
    {
        connection = new SqliteConnection(dbFile);
        command = connection.CreateCommand();
        connection.Open();

        //string testTable = "DROP TABLE IF EXISTS inventory;";
        string testTable = "CREATE TABLE IF NOT EXISTS inventory(id INTEGER PRIMARY KEY AUTOINCREMENT, idItem INT, name VARCHAR(50), quantity INT);";
        command.CommandText = testTable;
        command.ExecuteNonQuery();
    }
    
    
    public void Insert()
    {
        string query = "INSERT INTO inventory(idItem, name, quantity) VALUES(2, 'potion', 1);" +
            "INSERT INTO inventory(idItem, name, quantity) VALUES(54, 'hi-potion', 1);" +
            "INSERT INTO inventory(idItem, name, quantity) VALUES(23, 'x-potion', 1);" +
            "INSERT INTO inventory(idItem, name, quantity) VALUES(98, 'sword', 1);" +
            "INSERT INTO inventory(idItem, name, quantity) VALUES(5, 'bread', 1);";

        command.CommandText = query;
        command.ExecuteNonQuery();
    }

    public void ConsultOne()
    {
        int id2 = 3;
        string query = "SELECT id, idItem, name, quantity FROM inventory WHERE id = "+ id2 +";";
        command.CommandText = query;

        reader = command.ExecuteReader();
        while (reader.Read())
        {

            int id = reader.GetInt32(0);
            int idItem = reader.GetInt32(1);
            string name = reader.GetString(2);
            int quantity = reader.GetInt32(3);

            Debug.Log("id: "+id+" idItem: "+idItem+" name: "+name+" quantity: "+quantity+" ");

        }
    }

    public void ConsultAll()
    {
        
        string query = "SELECT id, idItem, name, quantity FROM inventory;";
        command.CommandText = query;

        reader = command.ExecuteReader();
        while (reader.Read())
        {

            int id = reader.GetInt32(0);
            int idItem = reader.GetInt32(1);
            string name = reader.GetString(2);
            int quantity = reader.GetInt32(3);

            Debug.Log("id: " + id + " idItem: " + idItem + " name: " + name + " quantity: " + quantity + " ");

        }


    }
    public void Delete()
    {
        int id = 4;
        string query = "DELETE FROM inventory WHERE id = " + id + ";";
        command.CommandText = query;
        command.ExecuteNonQuery();

    }

    public void InsertOrUpdate()
    {

        int idItem2 = 2;
        string name2 = "";
        string query = "SELECT id, idItem, name, quantity FROM inventory WHERE idItem = " + idItem2 + ";";
        command.CommandText = query;

        reader = command.ExecuteReader();
        int quantity = 0;
        int quantity2 = 1;
        int id = 0;
        while (reader.Read())
        {

            id = reader.GetInt32(0);
            name2 = reader.GetString(2);
            quantity = reader.GetInt32(3);


        }

        if(id > 0)
        {

            quantity2 += quantity;
            query = "UPDATE inventory SET quantity = " + quantity2 + "WHERE id = " + id + ";";

        }
        else
        {
            query = "INSERT INTO inventory(idItem, name, quantity) VALUES(" + idItem2 + "," + name2 + "," + quantity2 + ");";

        }

        

        quantity2 += quantity;

        query = "UPDATE inventory SET quantity = " + quantity2 + " WHERE id = " + id + ";";
        command.CommandText = query;
        command.ExecuteNonQuery();
    }


    public void UpdateColumn()
    {

        int idItem2 = 2;
        string query = "SELECT id, idItem, name, quantity FROM inventory WHERE idItem = " + idItem2 + ";";
        command.CommandText = query;

        reader = command.ExecuteReader();
        int quantity = 0;
        int quantity2 = 0;
        int id = 0;
        while (reader.Read())
        {

            id = reader.GetInt32(0);
            quantity = reader.GetInt32(3);


        }

        quantity2 += quantity;

        query = "UPDATE inventory SET quantity = " + quantity2 + " WHERE id = " + id + ";";
        command.CommandText = query;
        command.ExecuteNonQuery();

    }
}
