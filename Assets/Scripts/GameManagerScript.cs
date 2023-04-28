using System.Collections;
using TMPro;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject[] pathObjects;
    public GameObject Line;
    //public GameObject arrowMark;
    public GameObject ballPrefab;

    private Vector3 initialBallPosHigh;
    private Vector3 initialBallPosLow;

    public GameObject foreHandLow;
    public GameObject foreHandHigh;

    public static int active, i, resetCount, successfulHits, drillOne;
    public static bool completeCalled, pathActive;

    public TextMeshProUGUI shotImpressionText;

    public GameObject[] stars;

    public Transform head;
    public Transform batNetPos;

    private float height, length, forward;

    //public TextMeshProUGUI heightText;
    //public TextMeshProUGUI lengthText;
    //public TextMeshProUGUI forwardText;

    private void Start()
    {
        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);
        initialBallPosHigh = foreHandHigh.transform.GetChild(3).gameObject.transform.position;
        initialBallPosLow = foreHandLow.transform.GetChild(3).gameObject.transform.position;
    }

    private void Update()
    {
        Debug.Log("Successful Hits:- " + successfulHits + " & reset count:- " + resetCount);

        if (!foreHandHigh.activeInHierarchy && !foreHandLow.activeInHierarchy)
        {
            shotImpressionText.text = " ";
        }

        if (PhysicGame_MenuManager.setButtonPressed)
        {
            foreHandHigh.transform.position = new Vector3(batNetPos.transform.position.x - 3f, head.transform.position.y - 1.25f, foreHandHigh.transform.position.z);
            foreHandLow.transform.position = new Vector3(batNetPos.transform.position.x - 1.8f, head.transform.position.y - 1.5f, foreHandLow.transform.position.z);
            PhysicGame_MenuManager.setButtonPressed = false;
        }
        if (foreHandHigh.transform.parent.gameObject.activeInHierarchy)
        {
            pathActive = true;
        }
        if (!foreHandHigh.transform.parent.gameObject.activeInHierarchy)
        {
            pathActive = false;
        }

        if (foreHandHigh.activeInHierarchy && !completeCalled)
        {
            for (int i = 0; i < 6; i++)
            {
                pathObjects[i] = foreHandHigh.transform.GetChild(1).transform.GetChild(i).gameObject;
            }
            //arrowMark = foreHandHigh.transform.GetChild(2).gameObject;
            Line = foreHandHigh.transform.GetChild(0).gameObject;
            ballPrefab = foreHandHigh.transform.GetChild(3).gameObject;
        }
        if (foreHandLow.activeInHierarchy && !completeCalled)
        {
            for (int i = 0; i < 6; i++)
            {
                pathObjects[i] = foreHandLow.transform.GetChild(1).transform.GetChild(i).gameObject;
            }
            //arrowMark = foreHandLow.transform.GetChild(2).gameObject;
            Line = foreHandLow.transform.GetChild(0).gameObject;
            ballPrefab = foreHandLow.transform.GetChild(3).gameObject;
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
        else if (BatFollower.timeUp)
        {
            Completed();
            completeCalled = true;
            BatFollower.timeUp = false;
        }
    }

    public void ResetButtonPressed()
    {
        resetCount++;

        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);


        if (successfulHits < 6)
        {
            shotImpressionText.text = "Hit through again";

            for (i = 0; i < 6; i++)
            {
                if (!pathObjects[i].activeInHierarchy)
                {
                    pathObjects[i].SetActive(true);
                    pathObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                }
            }
            //arrowMark.SetActive(true);
            ballPrefab.SetActive(false);
            Line.SetActive(true);
        }

        completeCalled = false;
        BatFollower.lastHit = 0;

        if (successfulHits >= 6 && successfulHits < 11)
        {
            ballPrefab.SetActive(true);
            shotImpressionText.text = "Hit the ball again";

            if (foreHandHigh.activeInHierarchy)
            {
                ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ballPrefab.GetComponent<Rigidbody>().useGravity = false;
                ballPrefab.transform.position = initialBallPosHigh;
            }
            else if (foreHandLow.activeInHierarchy)
            {
                ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ballPrefab.GetComponent<Rigidbody>().useGravity = false;
                ballPrefab.transform.position = initialBallPosLow;
            }

            for (i = 0; i < 6; i++)
            {
                if (!pathObjects[i].activeInHierarchy)
                {
                    pathObjects[i].SetActive(true);
                    pathObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                }
            }
            Line.SetActive(true);
        }

        if (successfulHits >= 11 && successfulHits < 21)
        {
            ballPrefab.SetActive(true);
            shotImpressionText.text = "Hit the ball again";

            if (foreHandHigh.activeInHierarchy)
            {
                ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ballPrefab.GetComponent<Rigidbody>().useGravity = false;
                ballPrefab.transform.position = initialBallPosHigh;
            }
            else if (foreHandLow.activeInHierarchy)
            {
                ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ballPrefab.GetComponent<Rigidbody>().useGravity = false;
                ballPrefab.transform.position = initialBallPosLow;
            }

            for (i = 0; i < 6; i++)
            {
                if (!pathObjects[i].activeInHierarchy)
                {
                    pathObjects[i].SetActive(true);
                    pathObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                }
            }
            Line.SetActive(false);
        }

        //if (successfulHits >= 21)
        //{
        //    successfulHits = 0;

        //    shotImpressionText.text = "Hit through again";

        //    if (foreHandHigh.activeInHierarchy)
        //    {
        //        ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //        ballPrefab.GetComponent<Rigidbody>().useGravity = false;
        //        ballPrefab.transform.position = initialBallPosHigh;
        //    }
        //    else if (foreHandLow.activeInHierarchy)
        //    {
        //        ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //        ballPrefab.GetComponent<Rigidbody>().useGravity = false;
        //        ballPrefab.transform.position = initialBallPosLow;
        //    }
        //    ballPrefab.SetActive(false);

        //    for (i = 0; i < 6; i++)
        //    {
        //        if (!pathObjects[i].activeInHierarchy)
        //        {
        //            pathObjects[i].SetActive(true);
        //            pathObjects[i].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        //        }
        //    }
        //    Line.SetActive(true);
        //}

        if (successfulHits >= 21)
        {
            if (foreHandHigh.activeInHierarchy)
            {
                ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ballPrefab.GetComponent<Rigidbody>().useGravity = false;
                ballPrefab.transform.position = initialBallPosHigh;
            }
            else if (foreHandLow.activeInHierarchy)
            {
                ballPrefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ballPrefab.GetComponent<Rigidbody>().useGravity = true;
                ballPrefab.transform.position = new Vector3(initialBallPosLow.x, 1.8f, initialBallPosLow.z);
            }
        }
    }

    IEnumerator StarsTimer()
    {
        yield return new WaitForSeconds(1f);

        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);
    }

    public void Completed()
    {
        //arrowMark.SetActive(false);
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
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(true);
                successfulHits++;
            }
            else if (active > 3)
            {
                shotImpressionText.text = "GOOD !!!";
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(false);
            }
            else if (active < 3)
            {
                shotImpressionText.text = "POOR !!!";
                stars[0].SetActive(false);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
            }
            else if (active == 3)
            {
                shotImpressionText.text = "AVERAGE !!!";
                stars[0].SetActive(true);
                stars[1].SetActive(false);
                stars[2].SetActive(false);
            }

            shotImpressionText.text += " Press X Button On Left Controller";

            if (stars[0].activeInHierarchy || stars[1].activeInHierarchy || stars[2].activeInHierarchy)
            {
                StartCoroutine(StarsTimer());
            }

            active = 0;
            BatFollower.hitStarted = false;
            BatFollower.hit = false;
            BatFollower.hitFinished = false;
        }
    }

    public void HeightNve()
    {
        height -= 0.1f;
        //heightText.text = height.ToString();
        if (foreHandHigh.activeInHierarchy)
        {
            foreHandHigh.transform.position = new Vector3(foreHandHigh.transform.position.x, foreHandHigh.transform.position.y - 0.1f, foreHandHigh.transform.position.z);
        }
        if (foreHandLow.activeInHierarchy)
        {
            foreHandLow.transform.position = new Vector3(foreHandLow.transform.position.x, foreHandLow.transform.position.y - 0.1f, foreHandLow.transform.position.z);
        }
    }

    public void HeightPve()
    {
        height += 0.1f;
        //eightText.text = height.ToString();
        if (foreHandHigh.activeInHierarchy)
        {
            foreHandHigh.transform.position = new Vector3(foreHandHigh.transform.position.x, foreHandHigh.transform.position.y + 0.1f, foreHandHigh.transform.position.z);
        }
        if (foreHandLow.activeInHierarchy)
        {
            foreHandLow.transform.position = new Vector3(foreHandLow.transform.position.x, foreHandLow.transform.position.y + 0.1f, foreHandLow.transform.position.z);
        }
    }

    public void LengthNve()
    {
        length -= 0.1f;
        //lengthText.text = length.ToString();
        if (foreHandHigh.activeInHierarchy)
        {
            foreHandHigh.transform.position = new Vector3(foreHandHigh.transform.position.x - 0.1f, foreHandHigh.transform.position.y, foreHandHigh.transform.position.z);
        }
        if (foreHandLow.activeInHierarchy)
        {
            foreHandLow.transform.position = new Vector3(foreHandLow.transform.position.x - 0.1f, foreHandLow.transform.position.y, foreHandLow.transform.position.z);
        }
    }

    public void LengthPve()
    {
        length += 0.1f;
        //lengthText.text = length.ToString();
        if (foreHandHigh.activeInHierarchy)
        {
            foreHandHigh.transform.position = new Vector3(foreHandHigh.transform.position.x + 0.1f, foreHandHigh.transform.position.y, foreHandHigh.transform.position.z);
        }
        if (foreHandLow.activeInHierarchy)
        {
            foreHandLow.transform.position = new Vector3(foreHandLow.transform.position.x + 0.1f, foreHandLow.transform.position.y, foreHandLow.transform.position.z);
        }
    }

    public void ForwardNve()
    {
        forward -= 0.1f;
        //forwardText.text = forward.ToString();
        if (foreHandHigh.activeInHierarchy)
        {
            foreHandHigh.transform.position = new Vector3(foreHandHigh.transform.position.x, foreHandHigh.transform.position.y, foreHandHigh.transform.position.z - 0.1f);
        }
        if (foreHandLow.activeInHierarchy)
        {
            foreHandLow.transform.position = new Vector3(foreHandLow.transform.position.x, foreHandLow.transform.position.y, foreHandLow.transform.position.z - 0.1f);
        }
    }

    public void ForwardPve()
    {
        forward += 0.1f;
        //forwardText.text = forward.ToString();
        if (foreHandHigh.activeInHierarchy)
        {
            foreHandHigh.transform.position = new Vector3(foreHandHigh.transform.position.x, foreHandHigh.transform.position.y, foreHandHigh.transform.position.z + 0.1f);
        }
        if (foreHandLow.activeInHierarchy)
        {
            foreHandLow.transform.position = new Vector3(foreHandLow.transform.position.x, foreHandLow.transform.position.y, foreHandLow.transform.position.z + 0.1f);
        }
    }
}
