using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using AimTrainer.Utils;

public class Menu_Stats_RadarGraph : MonoBehaviour
{
    public CanvasRenderer chartMeshRenderer;
    public Material chartMeshMaterial;
    public Texture2D chartTexture;

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
            score_max = LevelStats.BASIC_SCORE_MAX;
            time_max = LevelStats.BASIC_TIME_MAX;
            time_min = LevelStats.BASIC_TIME_MIN;
            kps_max = LevelStats.BASIC_KPS_MAX;
            kps_min = LevelStats.BASIC_KPS_MIN;
            ttk_min = LevelStats.BASIC_TTK_MIN;
            ttk_max = LevelStats.BASIC_TTK_MAX;
            targets_max = LevelStats.BASIC_TARGETS_MAX;
        } 
        else if (gameStatus_Type == levelEnum.MOVING)
        {
            score_max = LevelStats.MOVING_SCORE_MAX;
            time_max = LevelStats.MOVING_TIME_MAX;
            time_min = LevelStats.MOVING_TIME_MIN;
            kps_max = LevelStats.MOVING_KPS_MAX;
            kps_min = LevelStats.MOVING_KPS_MIN;
            ttk_min = LevelStats.MOVING_TTK_MIN;
            ttk_max = LevelStats.MOVING_TTK_MAX;
            targets_max = LevelStats.MOVING_TARGETS_MAX;
        }
        else if (gameStatus_Type == levelEnum.ANTICIPATION)
        {
            score_max = LevelStats.ANTICIPATION_SCORE_MAX;
            time_max = LevelStats.ANTICIPATION_TIME_MAX;
            time_min = LevelStats.ANTICIPATION_TIME_MIN;
            kps_max = LevelStats.ANTICIPATION_KPS_MAX;
            kps_min = LevelStats.ANTICIPATION_KPS_MIN;
            ttk_min = LevelStats.ANTICIPATION_TTK_MIN;
            ttk_max = LevelStats.ANTICIPATION_TTK_MAX;
            targets_max = LevelStats.ANTICIPATION_TARGETS_MAX;
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
    private int LoadSavedStats()
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
            else if (gameStatus_Type == levelEnum.MOVING && instance.level_name == "Moving targets")
            {
                gamesPlayed++;
                save_score += instance.score;
                save_time_to_kill += instance.time_to_kill;
                save_targets_missed += instance.targets_missed;
                save_accuracy += instance.accuracy;
                save_kills_per_sec += instance.kills_per_sec;
                save_session_time += instance.session_time;
            }
            else if (gameStatus_Type == levelEnum.ANTICIPATION && instance.level_name == "Anticipation targets")
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

        if (gamesPlayed > 0)
        {
            save_score /= gamesPlayed;
            save_time_to_kill /= gamesPlayed;
            save_targets_missed /= gamesPlayed;
            save_accuracy /= gamesPlayed;
            save_kills_per_sec /= gamesPlayed;
            save_session_time /= gamesPlayed;
            return 0;
        }
        return 1;
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

        Vector3[] vertices = new Vector3[7];
        Vector2[] uv = new Vector2[7];
        int[] triangles = new int[3 * 6];

        float angleIncrement = 360f / 6;
        float chartSize = 169f;
        float chartSize_sides = 169f;

        //score = 100;
        //save_accuracy = 100;
        //targetsHit = 100;

        Vector3 scoreVertex = Quaternion.Euler(0, 0, angleIncrement * 0) * Vector3.up * chartSize * ((float)score / 100);
        int scoreVertexIndex = 1;
        Vector3 accuracyVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * chartSize_sides * (float)(save_accuracy / 100);
        int accuracyVertexIndex = 2;
        Vector3 targetsHitVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * Vector3.up * chartSize_sides * ((float)targetsHit / 100);
        int targetsHitVertexIndex = 3;
        Vector3 timeVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * Vector3.up * chartSize * ((float)time / 100);
        int timeVertexIndex = 4;
        Vector3 killsPerSecondVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * Vector3.up * chartSize_sides * ((float)killsPerSecond / 100);
        int killsPerSecondVertexIndex = 5;
        Vector3 timeToKillVertex = Quaternion.Euler(0, 0, -angleIncrement * 5) * Vector3.up * chartSize_sides * ((float)timeToKill / 100);
        int timeToKillVertexIndex = 6;

        vertices[0]                         = Vector3.zero;
        vertices[scoreVertexIndex]          = scoreVertex;
        vertices[accuracyVertexIndex]       = accuracyVertex;
        vertices[targetsHitVertexIndex]     = targetsHitVertex;
        vertices[timeVertexIndex]           = timeVertex;
        vertices[killsPerSecondVertexIndex] = killsPerSecondVertex;
        vertices[timeToKillVertexIndex]     = timeToKillVertex;

        uv[0]                           = Vector2.zero;
        uv[scoreVertexIndex]            = Vector2.one;
        uv[accuracyVertexIndex]         = Vector2.one;
        uv[targetsHitVertexIndex]       = Vector2.one;
        uv[timeVertexIndex]             = Vector2.one;
        uv[killsPerSecondVertexIndex]   = Vector2.one;
        uv[timeToKillVertexIndex]       = Vector2.one;

        triangles[0] = 0;
        triangles[1] = scoreVertexIndex;
        triangles[2] = accuracyVertexIndex;

        triangles[3] = 0;
        triangles[4] = accuracyVertexIndex;
        triangles[5] = targetsHitVertexIndex;

        triangles[6] = 0;
        triangles[7] = targetsHitVertexIndex;
        triangles[8] = timeVertexIndex;

        triangles[9] = 0;
        triangles[10] = timeVertexIndex;
        triangles[11] = killsPerSecondVertexIndex;

        triangles[12] = 0;
        triangles[13] = killsPerSecondVertexIndex;
        triangles[14] = timeToKillVertexIndex;

        triangles[15] = 0;
        triangles[16] = timeToKillVertexIndex;
        triangles[17] = scoreVertexIndex;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        chartMeshRenderer.SetMesh(mesh);
        chartMeshRenderer.SetMaterial(chartMeshMaterial, chartTexture);
    }
    public void UpdateStatsHeatmap()
    {
        ResetStats();
        SetLevelTypeStats();
        if (LoadSavedStats() == 1)
        {
            return;
        }

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
        timeToKill = (int)((save_time_to_kill - ttk_min) * 100 / (ttk_max - ttk_min));

        DrawChart();
        PrintText();
    }

}
