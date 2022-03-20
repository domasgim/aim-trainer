using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] GameControl gameControl;

    public void Start()
    {
        //DisableTarget();
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
    public void Hit()
    {
        transform.position = TargetBounds.Instance.GetRandomPosition();
    }

    public void EnableTarget()
    {
        material.color = Color.red;
    }

    public void DisableTarget()
    {
        material.color = Color.blue;
    }
}
