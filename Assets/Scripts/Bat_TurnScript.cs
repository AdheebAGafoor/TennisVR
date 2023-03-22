using UnityEngine;
using UnityEngine.InputSystem;

public class Bat_TurnScript : MonoBehaviour
{
    public InputActionProperty batTurn;

    private void FixedUpdate()
    {
        if (batTurn.action.ReadValue<Vector2>().x < 0)
        {
            transform.Rotate(0, 0, 2, Space.Self);
        }
        if (batTurn.action.ReadValue<Vector2>().x > 0)
        {
            transform.Rotate(0, 0, -2, Space.Self);
        }
    }
}
