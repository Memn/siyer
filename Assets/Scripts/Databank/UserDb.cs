using System;
using System.Collections.Generic;
using System.Data;
using DataBank;
using UnityEngine;

public class UserDb : SqliteHelper<User>
{
    private const String Tag = "UserDb:\t";

    private const String TABLE_NAME = "Users";

    private const String KEY_ID = "id";
    private const String KEY_USERNAME = "username";
    private const String KEY_SCORE = "score";
    private const String KEY_ACHIEVEMENTS = "achievements";
    private const String KEY_LEVEL = "level";

    public UserDb()
    {
        IDbCommand dbcmd = getDbCommand();
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                            KEY_ID + " TEXT PRIMARY KEY, " +
                            KEY_USERNAME + " TEXT, " +
                            KEY_SCORE + " TEXT, " +
                            KEY_ACHIEVEMENTS + " TEXT, " +
                            KEY_LEVEL + " TEXT )";
        dbcmd.ExecuteNonQuery();
    }

    public override User addData(User user)
    {
        var dbcmd = getDbCommand();
        dbcmd.CommandText = "INSERT INTO " + TABLE_NAME
                                           + " ( "
                                           + KEY_ID + ", "
                                           + KEY_USERNAME + ", "
                                           + KEY_SCORE + ", "
                                           + KEY_ACHIEVEMENTS + ", "
                                           + KEY_LEVEL + " ) "
                                           + "VALUES ( '"
                                           + user._id + "', '"
                                           + user._username + "', '"
                                           + user._score + "', '"
                                           + user._achievements + "', '"
                                           + user._level + "' )";
        dbcmd.ExecuteNonQuery();
        return user;
    }

    public override User getDataById(string id)
    {
        Debug.Log(Tag + "Getting User: " + id);
        return base.getDataById(TABLE_NAME, KEY_ID, id);
    }

    public override void deleteDataById(string id)
    {
        Debug.Log(Tag + "Deleting User: " + id);

        var dbcmd = getDbCommand();
        dbcmd.CommandText = "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
        dbcmd.ExecuteNonQuery();
    }

    public override void deleteAllData()
    {
        Debug.Log(Tag + "Deleting Table");
        base.deleteAllData(TABLE_NAME);
    }

    public override User toEntity(IDataReader reader)
    {
        return new User(
            reader[0].ToString(),
            reader[1].ToString(),
            int.Parse(reader[2].ToString()),
            int.Parse(reader[4].ToString())
        );
    }

    public override List<User> getAllData()
    {
        return base.getAllData(TABLE_NAME);
    }

    public List<User> currentUsers()
    {
        return getAllData(TABLE_NAME);
    }

    public User CreateUser(User user)
    {
        addData(user);
        return user;
    }

    public User merge(string userId, string username)
    {
        Debug.Log("merging with guest.");
        var dbcmd = getDbCommand();
        dbcmd.CommandText = "UPDATE " + TABLE_NAME
                                      + " SET "
                                      + KEY_ID + " = '" + userId + "', "
                                      + KEY_USERNAME + " = '" + username + "'"
                                      + " WHERE " + KEY_ID + " = 'guest'";
        dbcmd.ExecuteNonQuery();
        return getDataById(userId);
    }

    public bool exists(string id)
    {
        return base.exists(TABLE_NAME, KEY_ID, id);
    }
}