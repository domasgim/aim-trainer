using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Anticipation : MonoBehaviour
{
    [SerializeField] Material material;
    // Start is called before the first frame update
    void Start()
    {
        DisableTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        if (TargetShootable())
        {
            transform.position = TargetBounds.Instance.GetRandomPosition();
        }
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
