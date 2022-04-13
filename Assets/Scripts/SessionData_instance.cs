using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SessionData_instance
{
    public string level_name;
    public int score;
    public float accuracy;
    public int time_to_kill; // milliseconds
    public float kills_per_sec;
    public int targets_missed;
    public float session_time;
    public int unix_timestamp;

    public SessionData_instance()
    {
        level_name = "";
        score = 0;
        accuracy = 0;
        time_to_kill = 0;
        kills_per_sec = 0;
        targets_missed = 0;
        session_time = 0;
        unix_timestamp = 0;
    }

    public SessionData_instance(SessionData_instance sessionData)
    {
        level_name = sessionData.level_name;
        score = sessionData.score;
        accuracy = sessionData.accuracy;
        time_to_kill = sessionData.time_to_kill;
        kills_per_sec = sessionData.kills_per_sec;
        targets_missed = sessionData.targets_missed;
        session_time = sessionData.session_time;
        unix_timestamp = sessionData.unix_timestamp;
    }
}
