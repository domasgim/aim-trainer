using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PathCreation.Examples;
using PathCreation;

public class Target_Anticipation_V2 : MonoBehaviour
{
    // Color change
    [SerializeField] Material material;
    [SerializeField] GameControl_Anticipation gameControl;

    private bool coroutineStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        EnableTarget();
        gameControl.targetStatus = GameControl_Anticipation.targetStatusEnum.TARGETS_ENABLED;
    }

    // Start is called before the first frame update
    void Start()
    {
        DisableTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameControl.gameStatus == GameControl_Anticipation.gameStatusEnum.FINISHED)
        {
            StopAllCoroutines();
        }
    }

    public int Hit(Vector3 hit)
    {
        int score = 0;
        // NEREIK TIKRINT AR STARTED
        if (gameControl.targetStatus == GameControl_Anticipation.targetStatusEnum.TARGETS_DISABLED && !coroutineStarted)
        {
            
        }
        else if (TargetShootable())
        {
            float xDiff = (hit.x - transform.position.x) * 100;
            float yDiff = (hit.y - transform.position.y) * 100;

            int xRoundedDiff = Math.Abs(((int)Math.Round(xDiff / 20.0)) * 20);
            int yRoundedDiff = Math.Abs(((int)Math.Round(yDiff / 20.0)) * 20);

            if (xRoundedDiff == 40 || yRoundedDiff == 40)
            {
                score = 25;
            }
            else if (xRoundedDiff == 20 || yRoundedDiff == 20)
            {
                score = 50;
            }
            else if (xRoundedDiff == 0 || yRoundedDiff == 0)
            {
                score = 100;
            }

            DisableTarget();
            gameControl.targetStatus = GameControl_Anticipation.targetStatusEnum.TARGETS_DISABLED;
            StopAllCoroutines();
            coroutineStarted = true;
        }
        return score;
    }

    public void EnableTarget()
    {
        material.color = Color.red;
    }

    public void DisableTarget()
    {
        material.color = Color.blue;
    }

    public bool TargetShootable()
    {
        if (material.color == Color.red)
        {
            return true;
        }
        return false;
    }

    private IEnumerator WaitBeforeRestarting()
    {
        yield return new WaitForSeconds(2);
        gameControl.currentTime = 0f;
    }
}
