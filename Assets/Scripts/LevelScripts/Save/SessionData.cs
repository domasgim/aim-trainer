using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SessionData
{
    public List<SessionData_instance> session_list;

    public SessionData()
    {
        session_list = new List<SessionData_instance>();
    }

    public SessionData (SessionData sessionData)
    {
        session_list = sessionData.session_list;
    }
}
