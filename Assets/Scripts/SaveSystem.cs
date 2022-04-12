using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    private static readonly string SAVE_PATH = Application.dataPath + "/save.txt";
    public static void SaveSession(SessionData_instance sessionData_instance)
    {
        SessionData sessionData = new SessionData();
        //if(!File.Exists(SAVE_PATH))
        //{
        //    sessionData.session_list[0] = sessionData_instance;
        //}
        sessionData.session_list.Add(sessionData_instance);

        string json = JsonUtility.ToJson(sessionData);

        Debug.Log(json);

        //File.WriteAllText(path, json);
    }

    public static SessionData LoadSession()
    {
        string path = Application.persistentDataPath + "/session.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SessionData data = formatter.Deserialize(stream) as SessionData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
