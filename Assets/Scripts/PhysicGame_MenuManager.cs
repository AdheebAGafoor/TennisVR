using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicGame_MenuManager : MonoBehaviour
{
    public Transform head;
    public GameObject physicMenu;
    public GameObject starParent;
    public InputActionProperty physicMenuButton;
    public InputActionProperty menuButton;
    public GameObject mainMenu;
    private bool once;

    public TextMeshProUGUI heightText;
    public TextMeshProUGUI armLengthText;

    public GameObject armPosObject;

    [HideInInspector]
    public static bool setButtonPressed, physicBackButtonPressed;

    private void Start()
    {
        once = true;

        if (once)
        {
            mainMenu.SetActive(false);
            physicMenu.SetActive(false);
            once = false;
        }
    }

    private void Update()
    {
        if (physicMenuButton.action.WasPressedThisFrame())
        {
            //if (!physicMenu.activeInHierarchy)
            //{
                physicMenu.SetActive(!physicMenu.activeSelf);     /// (!physicMenu.activeSelf)
            //}
            //else
            //{
            //    physicBackButtonPressed = true;
            //}
            physicMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 3;
        }
        //if (physicBackButtonPressed)
        //{
        //    physicMenu.SetActive(false);
        //}
        physicMenu.transform.LookAt(new Vector3(head.position.x, physicMenu.transform.position.y,head.position.z));
        physicMenu.transform.forward *= -1;

        if (!GameManagerScript.completeCalled)
        {
            if (menuButton.action.WasPressedThisFrame() && !GameManagerScript.completeCalled)
            {
                mainMenu.SetActive(!mainMenu.activeSelf);

                mainMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 3;
            }
        }
        mainMenu.transform.LookAt(new Vector3(head.position.x, physicMenu.transform.position.y, head.position.z));
        mainMenu.transform.forward *= -1;

        starParent.transform.position = head.position + new Vector3(head.forward.x, 0.2f, head.forward.z).normalized * 2.5f;
        starParent.transform.LookAt(new Vector3(head.position.x, physicMenu.transform.position.y, head.position.z));
        starParent.transform.forward *= -1;
    }

    public void PhysicMenuBack()
    {
        physicMenu.SetActive(false);
        physicBackButtonPressed = true;
    }

    public void SetHeightLength()
    {
        setButtonPressed = true;
        heightText.text ="Height : " + head.transform.position.y.ToString() + " m ";
        armLengthText.text = "Length : " + (armPosObject.transform.position.x - head.transform.position.x).ToString() + " m ";
    }

    public void MenuBackButton()
    {
        mainMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
