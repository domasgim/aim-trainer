using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Material material;

    public void Start()
    {
        //DisableTarget();
    }
    public void Hit()
    {
        transform.position = TargetBounds.Instance.GetRandomPosition();
        //if (TargetShootable())
        //{
        //    transform.position = TargetBounds.Instance.GetRandomPosition();
        //}
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
}
