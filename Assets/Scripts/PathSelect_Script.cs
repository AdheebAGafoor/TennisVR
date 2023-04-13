using UnityEngine;

public class PathSelect_Script : MonoBehaviour
{
    public GameObject dropDown;
    public GameObject pathForehandLow;
    public GameObject pathForehandHigh;
    public GameObject pathManagerObj;
    public GameObject editorShotParent1;
    public GameObject editorShotParent2;
    public GameObject editorShotParent3;

    private void Start()
    {
        ToggleShotEditor(false);
        ToggleShotTrainer(true);
    }

    public void ToggleShotEditor(bool isToggle)
    {
        if (isToggle)
        {
            editorShotParent1.SetActive(true);
            editorShotParent2.SetActive(true);
            editorShotParent3.SetActive(true);
        }
        else
        {
            editorShotParent1.SetActive(false);
            editorShotParent2.SetActive(false);
            editorShotParent3.SetActive(false);
        }
    }

    public void ToggleShotTrainer(bool isToggle)
    {
        if (!GameManagerScript.completeCalled)
        {
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
