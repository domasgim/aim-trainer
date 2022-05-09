//using System.Collections;
//using System.Collections.Generic;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;

//public class Test1
//{
//    // A Test behaves as an ordinary method
//    [Test]
//    public void SessionData_instance_CorrectVals()
//    {
//        SessionData_instance sessionData_Instance = new SessionData_instance();
//        int assertFlag = 0;
//        if (sessionData_Instance.level_name != "" || sessionData_Instance.score != 0 || 
//            sessionData_Instance.accuracy != 0 || sessionData_Instance.time_to_kill != 0 || 
//            sessionData_Instance.kills_per_sec != 0 || sessionData_Instance.targets_missed != 0 || 
//            sessionData_Instance.session_time != 0 || sessionData_Instance.unix_timestamp != 0 || 
//            sessionData_Instance.consecutive_targets_hit != 0)
//        {
//            assertFlag = 1;
//        }
//        Assert.AreEqual(0, assertFlag);
//    }

//    [Test]
//    public void SessionData_SessionDataEmpty()
//    {
//        SessionData sessionData = new SessionData();
//        List<SessionData_instance> sessionData_list = new List<SessionData_instance>();
//        Assert.AreEqual(sessionData_list, sessionData.session_list);
//    }

//    [Test]
//    public void SessionData_SessionDataFilled()
//    {
//        int assertFlag = 0;
//        List<SessionData_instance> sessionData_list = new List<SessionData_instance>();
//        SessionData_instance newInstance = new SessionData_instance();
//        newInstance.level_name = "testLevel";
//        sessionData_list.Add(newInstance);

//        SessionData sessionData = new SessionData();
//        sessionData.session_list = sessionData_list;

//        SessionData sessionData_filled = new SessionData(sessionData);
//        if (sessionData.session_list == sessionData_filled.session_list)
//        {
//            assertFlag = 1;
//        }

//        Assert.AreEqual(1, assertFlag);
//    }
//}
