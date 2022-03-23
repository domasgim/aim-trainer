using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] GameControl gameControl;

    public void Start()
    {
        DisableTarget();
    }

    void Update()
    {
        if (gameControl.gameStatus == GameControl.gameStatusEnum.STARTED)
        {
            EnableTarget();
        }
        else
        {
            DisableTarget();
        }
    }
    public int Hit(Vector3 hit)
    {
        int score = 0;
        if (TargetShootable())
        {
            float xDiff = (hit.x - transform.position.x) * 100;
            float yDiff = (hit.y - transform.position.y) * 100;

            int xRoundedDiff = Math.Abs(((int)Math.Round(xDiff / 20.0)) * 20);
            int yRoundedDiff = Math.Abs(((int)Math.Round(yDiff / 20.0)) * 20);


            Debug.Log("X diff: " + xRoundedDiff);
            Debug.Log("Y diff: " + yRoundedDiff);

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

            transform.position = TargetBounds.Instance.GetRandomPosition();
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
            return true;
        return false;
    }
}
