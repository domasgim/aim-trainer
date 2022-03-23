using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int Hit()
    {
        if (TargetShootable())
        {
            transform.position = TargetBounds.Instance.GetRandomPosition();
            return 1;
        }
        return 0;
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
