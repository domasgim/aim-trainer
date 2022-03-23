using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Anticipation : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] GameControl_Anticipation gameControl;

    [SerializeField] private Transform[] routes;
    [SerializeField] private float speedModifier;

    private int routeToGo;

    private float tParam;

    private Vector3 targetPosition;

    private bool coroutineAllowed;
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
        routeToGo = 0;
        tParam = 0;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameControl.gameStatus == GameControl_Anticipation.gameStatusEnum.STARTED)
        {
            
        }
    }

    public int Hit()
    {
        // NEREIK TIKRINT AR STARTED
        if (gameControl.targetStatus == GameControl_Anticipation.targetStatusEnum.TARGETS_DISABLED && !coroutineStarted)
        {
            StartCoroutine(GoByTheRoutine(routeToGo));
            gameControl.targetMoveStatus = GameControl_Anticipation.targetMoveStatusEnum.TARGET_START;
            coroutineStarted = true;
        } 
        else if (TargetShootable() && coroutineStarted)
        {
            DisableTarget();
            gameControl.targetStatus = GameControl_Anticipation.targetStatusEnum.TARGETS_DISABLED;
            StopAllCoroutines();
            coroutineStarted = false;
            routeToGo = 0;
            tParam = 0;
            StartCoroutine(WaitBeforeRestarting());
            coroutineStarted = true;
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
        {
            return true;
        }
        return false;
    }

    private IEnumerator GoByTheRoutine(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            targetPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = targetPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        // If set to true, after completing the bezier curve, the target's
        // potition will be restarted
        // coroutineAllowed = true;
    }

    private IEnumerator WaitBeforeRestarting()
    {
        yield return new WaitForSeconds(2);
        gameControl.currentTime = 0f;
        StartCoroutine(GoByTheRoutine(routeToGo));
    }
}
