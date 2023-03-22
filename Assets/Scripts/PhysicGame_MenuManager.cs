using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicGame_MenuManager : MonoBehaviour
{
    public Transform head;
    public GameObject menu;
    public InputActionProperty showButton;
    public InputActionProperty menuButton;
    public GameObject mainMenu;
    private bool once;

    private void Start()
    {
        once = true;
    }

    private void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 3;
        }
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y,head.position.z));
        menu.transform.forward *= -1;

        if (once)
        {
            mainMenu.SetActive(false);
            menu.SetActive(false);
            once = false;
        }

        if (!GameManagerScript.completeCalled)
        {
            if (menuButton.action.WasPressedThisFrame() && !GameManagerScript.completeCalled)
            {
                mainMenu.SetActive(!mainMenu.activeSelf);

                mainMenu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 3;
            }
        }
        mainMenu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        mainMenu.transform.forward *= -1;
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
