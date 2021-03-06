using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Target_Moving : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] GameControl_Moving gameControl;
    [SerializeField] BoxCollider col;

    Vector3 moveDirection;

    public void Start()
    {
        DisableTarget();
    }

    void Update()
    {
        if (gameControl.gameStatus == GameControl_Moving.gameStatusEnum.STARTED)
        {
            EnableTarget();
            Vector3 center = col.center;
            float minX = center.x - col.size.x / 2f;
            float maxX = center.x + col.size.x / 2f;

            float minY = center.y - col.size.y / 2f;
            float maxY = center.y + col.size.y / 2f;

            float minZ = center.z - col.size.z / 2f;
            float maxZ = center.z + col.size.z / 2f;

            if (transform.position.x > maxX)
            {
                moveDirection = -transform.right;
            } 
            else if (transform.position.x < minX)
            {
                moveDirection = transform.right;
            }
            else if (transform.position.y > maxY)
            {
                moveDirection = -transform.up;
            }
            else if (transform.position.y < minY)
            {
                moveDirection = transform.up;
            }

            transform.position += moveDirection * Time.deltaTime * 5;
        }
        else
        {
            DisableTarget();
        }

        //if (transform.position.x > col.transform.position.x)
        //{
        //    Debug.Log("ALIO");
        //}
    }
    public int Hit(Vector3 hit)
    {
        int score = 0;
        System.Random rnd = new System.Random();

        if (TargetShootable())
        {
            // 0 - center; 40 - edge; 20 - in between
            float xDiff = (hit.x - transform.position.x) * 100;
            float yDiff = (hit.y - transform.position.y) * 100;

            int xRoundedDiff = Math.Abs(((int)Math.Round(xDiff / 20.0)) * 20);
            int yRoundedDiff = Math.Abs(((int)Math.Round(yDiff / 20.0)) * 20);


            //Debug.Log("X diff: " + xRoundedDiff);
            //Debug.Log("Y diff: " + yRoundedDiff);

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
            switch (rnd.Next(4))
            {
                case 0:
                    if (moveDirection == transform.up)
                    {
                        moveDirection = -transform.up;
                    } 
                    else
                    {
                        moveDirection = transform.up;
                    }
                    break;
                case 1:
                    if (moveDirection == -transform.up)
                    {
                        moveDirection = transform.up;
                    }
                    else
                    {
                        moveDirection = -transform.up;
                    }
                    break;
                case 2:
                    if (moveDirection == transform.right)
                    {
                        moveDirection = -transform.right;
                    }
                    else
                    {
                        moveDirection = transform.right;
                    }
                    break;
                case 3:
                    if (moveDirection == -transform.right)
                    {
                        moveDirection = transform.right;
                    }
                    else
                    {
                        moveDirection = -transform.right;
                    }
                    break;
                default:
                    break;
            }
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
