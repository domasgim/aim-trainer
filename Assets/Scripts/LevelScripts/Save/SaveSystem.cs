using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    private static readonly string SAVE_PATH = Application.persistentDataPath + "/save.txt";
    public static void SaveSession(SessionData_instance sessionData_instance)
    {
        SessionData sessionData = new SessionData();
        sessionData.session_list.Add(sessionData_instance);

        if (File.Exists(SAVE_PATH))
        {
            string json_saved = File.ReadAllText(SAVE_PATH);
            SessionData sessionData_saved = JsonUtility.FromJson<SessionData>(json_saved);
            sessionData.session_list.AddRange(sessionData_saved.session_list);
        }

        string json = JsonUtility.ToJson(sessionData);

        File.WriteAllText(SAVE_PATH, json);
    }

    public static SessionData LoadSession()
    {
        if (File.Exists(SAVE_PATH))
        {
            string json_saved = File.ReadAllText(SAVE_PATH);
            SessionData sessionData_saved = JsonUtility.FromJson<SessionData>(json_saved);
            return sessionData_saved;
        } else
        {
            Debug.LogError("Save file not found in " + SAVE_PATH);
            return null;
        }
    }
}
