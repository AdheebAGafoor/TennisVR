using UnityEngine;
using UnityEngine.InputSystem;

public class MenuGameManager : MonoBehaviour
{
    public Transform head;
    public GameObject menu;
    public InputActionProperty showButton;

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
            menu.SetActive(false);
            once = false;
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
