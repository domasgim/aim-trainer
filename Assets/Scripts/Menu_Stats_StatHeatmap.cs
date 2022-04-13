using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class Menu_Stats_StatHeatmap : MonoBehaviour
{
    public CanvasRenderer chartMeshRenderer;
    public Material chartMeshMaterial;

    public TextMeshProUGUI accuracyText, scoreText, targetsHitText, timeText, kpsText, ttkText;

    private int accuracy, score, targetsHit, time, killsPerSecond, timeToKill, gamesPlayed;

    private int save_score;
    private int save_time_to_kill;

    private float save_targets_missed;
    private float save_accuracy;
    private float save_kills_per_sec;
    private float save_session_time;

    private float score_max, time_max, time_min, kps_max, kps_min, ttk_max, ttk_min, targets_max;

    public enum levelEnum
    {
        BASIC,
        ANTICIPATION,
        MOVING
    }

    public levelEnum gameStatus_Type = levelEnum.BASIC;
    private void SetLevelTypeStats()
    {
        if (gameStatus_Type == levelEnum.BASIC)
        {
            score_max = 300;
            // Slowest time
            time_max = 10;
            // Fastest time
            time_min = 1;
            kps_max = 3;
            kps_min = 0.1f;
            ttk_max = 50;
            ttk_max = 3000;
            targets_max = 3;
        }
    }
    private void ResetStats()
    {
        accuracy = 0;
        score = 0;
        targetsHit = 0;
        killsPerSecond = 0;
        timeToKill = 0;
        time = 0;
        gamesPlayed = 0;

        save_score = 0;
        save_time_to_kill = 0;
        save_targets_missed = 0;

        save_accuracy = 0;
        save_kills_per_sec = 0;
        save_session_time = 0;
    }
    private void LoadSavedStats()
    {
        SessionData sessionData = SaveSystem.LoadSession();
        foreach (SessionData_instance instance in sessionData.session_list)
        {
            if (gameStatus_Type == levelEnum.BASIC && instance.level_name == "Basic targets")
            {
                gamesPlayed++;
                save_score += instance.score;
                save_time_to_kill += instance.time_to_kill;
                save_targets_missed += instance.targets_missed;
                save_accuracy += instance.accuracy;
                save_kills_per_sec += instance.kills_per_sec;
                save_session_time += instance.session_time;
            }
        }

        // Get average vals
        save_score /= gamesPlayed;
        save_time_to_kill /= gamesPlayed;
        save_targets_missed /= gamesPlayed;
        save_accuracy /= gamesPlayed;
        save_kills_per_sec /= gamesPlayed;
        save_session_time /= gamesPlayed;
    }
    public void PrintText()
    {
        scoreText.text = "Score: " + score + "%"; 
        accuracyText.text = "Accuracy: " + (int)save_accuracy + "%";
        targetsHitText.text = "Targets hit: " + targetsHit + "%";
        timeText.text = "Time: " + time + "%";
        kpsText.text = "KPS: " + killsPerSecond + "%";
        ttkText.text = "TTK: " + timeToKill + "%";
    }
    public void DrawChart()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        float angleIncrement = 360f / 6;
        float chartSize = 168f;
        float chartSize_sides = 160f;
        Vector3 scoreVertex = Quaternion.Euler(0, 0, angleIncrement * 0) * Vector3.up * chartSize * ((float)score / 100);
        int scoreVertexIndex = 1;
        Vector3 accuracyVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * chartSize_sides * (float)(save_accuracy / 100);
        int accuracyVertexIndex = 2;

        vertices[0] = Vector3.zero;
        vertices[scoreVertexIndex] = scoreVertex;
        vertices[accuracyVertexIndex] = accuracyVertex;

        triangles[0] = 0;
        triangles[1] = scoreVertexIndex;
        triangles[2] = accuracyVertexIndex;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        chartMeshRenderer.SetMesh(mesh);
        chartMeshRenderer.SetMaterial(chartMeshMaterial, null);
    }
    public void UpdateStatsHeatmap()
    {
        ResetStats();
        SetLevelTypeStats();
        LoadSavedStats();

        // Calculate from range min / max
        score = (int)((save_score * 100) / score_max);
        // accuracy jau apskaiciuotas
        targetsHit = (int)(((targets_max - save_targets_missed) * 100) / targets_max);
        time = (int)(((save_session_time - time_min) * 100) / time_max - time_min);

        // must invert value
        time = 100 - time;
        if (time > 100)
        {
            time = 100;
        } else if (time < 0)
        {
            time = 0;
        }

        killsPerSecond = (int)(((save_kills_per_sec - kps_min) * 100) / kps_max - kps_min);
        timeToKill = (int)(((save_time_to_kill - ttk_min) * 100) / ttk_max - ttk_min);

        DrawChart();
        PrintText();
    }

}
