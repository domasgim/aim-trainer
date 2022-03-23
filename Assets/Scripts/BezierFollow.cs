using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] GameControl_Anticipation gameControl;
    [SerializeField] private float speedModifier;

    private int routeToGo;

    private float tParam;

    private Vector3 targetPosition;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameControl.gameStatus == GameControl_Anticipation.gameStatusEnum.STARTED)
        {
            if (coroutineAllowed)
            {
                StartCoroutine(GoByTheRoutine(routeToGo));
            }
        }
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
}
