using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject[] pathObjects;
    public GameObject Line;
    public GameObject arrowMark;

    public GameObject foreHandLow;
    public GameObject foreHandHigh;

    public static int active, i, resetCount;
    public static bool completeCalled;

    public TextMeshProUGUI shotImpressionText;


    private void Update()
    {
        if (foreHandHigh.activeInHierarchy && !completeCalled)
        {
            for (int i = 0; i < 6; i++)
            {
                pathObjects[i] = foreHandHigh.transform.GetChild(1).transform.GetChild(i).gameObject;
            }
            arrowMark = foreHandHigh.transform.GetChild(2).gameObject;
            Line = foreHandHigh.transform.GetChild(0).gameObject;
        }
        if (foreHandLow.activeInHierarchy && !completeCalled)
        {
            for (int i = 0; i < 6; i++)
            {
                pathObjects[i] = foreHandLow.transform.GetChild(1).transform.GetChild(i).gameObject;
            }
            arrowMark = foreHandLow.transform.GetChild(2).gameObject;
            Line = foreHandLow.transform.GetChild(0).gameObject;
        }

        if (BallSpawn_Script.resetPressed || Input.GetKeyDown(KeyCode.Space))
        {
            if (completeCalled || resetCount == 3)
            {
                ResetButtonPressed();
                BallSpawn_Script.resetPressed = false;
            }
        }
        if (BatFollower.completed && !BatFollower.timeUp)
        {
            Completed();
            completeCalled = true;
            BatFollower.completed = false;
        }
        if (BatFollower.timeUp)
        {
            Completed();
            completeCalled = true;
            BatFollower.timeUp = false;
        }
    }

    public void ResetButtonPressed()
    {
        resetCount++;

        if (resetCount == 3)
        {
            shotImpressionText.text = "Congratulations";
        }

        if (resetCount == 4)
        {
            shotImpressionText.text = "Hit through the path";
            resetCount = 0;
        }

        if (resetCount < 3)
        {
            shotImpressionText.text = "Hit through again";

            for (i = 0; i < 6; i++)
            {
                if (!pathObjects[i].activeInHierarchy)
                {
                    pathObjects[i].SetActive(true);
                }
            }
            arrowMark.SetActive(true);
            Line.SetActive(true);
        }
        completeCalled = false;
        BatFollower.lastHit = 0;
    }


    public void Completed()
    {
        arrowMark.SetActive(false);
        Line.SetActive(false);
        for (i = 0; i < 6; i++)
        {
            if (!pathObjects[i].activeInHierarchy)
                active++;
        }

        if (i == 6)
        {
            if (active > 5)
            {
                shotImpressionText.text = "GREAT !!!";
            }
            else if (active > 3)
            {
                shotImpressionText.text = "GOOD !!!";
            }
            else if (active < 3)
            {
                shotImpressionText.text = "POOR !!!";
            }
            else if (active == 3)
            {
                shotImpressionText.text = "AVERAGE !!!";
            }

            shotImpressionText.text += " Press X Button On Left Controller";

            active = 0;
            BatFollower.hitStarted = false;
            BatFollower.hit = false;
            BatFollower.hitFinished = false;
        }
    }
}
