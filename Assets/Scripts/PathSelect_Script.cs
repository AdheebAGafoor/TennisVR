using UnityEngine;

public class PathSelect_Script : MonoBehaviour
{
    public GameObject dropDown;
    public GameObject pathForehandLow;
    public GameObject pathForehandHigh;
    public GameObject pathManagerObj;

    //public static bool isShotTrainer;

    public void ToggleShotTrainer(bool isToggle)
    {
        if (!GameManagerScript.completeCalled)
        {
            //isShotTrainer = isToggle;
            if (isToggle)
            {
                dropDown.SetActive(true);
                pathManagerObj.SetActive(true);
            }
            else
            {
                GameManagerScript.resetCount = 0;
                dropDown.SetActive(false);
                pathManagerObj.SetActive(false);
            }
        }
    }

    public void DropDownShots(int index)
    {
        if (!GameManagerScript.completeCalled)
        {
            switch (index)
            {
                case 0:
                    pathForehandLow.SetActive(false);
                    pathForehandHigh.SetActive(true);
                    break;

                case 1:
                    pathForehandHigh.SetActive(false);
                    pathForehandLow.SetActive(true);
                    break;
            }
        }
    }
}
