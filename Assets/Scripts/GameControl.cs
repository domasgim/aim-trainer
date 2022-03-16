using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Texture2D cursorTexture;

    private Vector2 cursorHotspot;

    private Vector2 mousePos;

    [SerializeField]
    private Text getReadyText;

    [SerializeField]
    private GameObject resultsPanel;

    [SerializeField]
    private Text scoreText, targetsHitText, shotsFiredText, accuracyText;

    public static int score, targetsHit;

    private float shotsFired;
    private float accuracy;
    private int targetsAmmount;

    [SerializeField]
    private int targetsAmmountInitial;

    private Vector2 targetRandomPosition;

    private bool sessionStarted = false;

    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        //targetsAmmountInitial = 5;
        targetsAmmount = targetsAmmountInitial;
        score = 0;
        shotsFired = 0;
        targetsHit = 0;
        accuracy = 0f;

        resultsPanel.SetActive(true);
        scoreText.text = "Score: " + score;
        targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
        shotsFiredText.text = "Shots Fired: " + shotsFired;
        //accuracy = targetsHit / shotsFired * 100f;
        accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (sessionStarted)
            {
                shotsFired++;
            }
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Target target = hit.collider.gameObject.GetComponent<Target>();

                if (target != null && targetsAmmount > 0)
                {
                    target.Hit();
                    if (!sessionStarted)
                    {
                        target.EnableTarget();
                        sessionStarted = true;
                    } else
                    {
                        score += 10;
                        targetsHit++;
                        targetsAmmount--;
                    }
                } 
                else if (target != null && targetsAmmount == 0 && sessionStarted)
                {
                    shotsFired--;
                    target.DisableTarget();
                    sessionStarted = false;
                }
            }
            scoreText.text = "Score: " + score;
            targetsHitText.text = "Targets Hit: " + targetsHit + "/" + targetsAmmountInitial;
            shotsFiredText.text = "Shots Fired: " + shotsFired;
            accuracy = targetsHit / shotsFired * 100f;
            accuracyText.text = "Accuracy: " + accuracy.ToString("N2") + " %";
            Debug.Log("Shots fired: " + shotsFired);
        }
    }
}
