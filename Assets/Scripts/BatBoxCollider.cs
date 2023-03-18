using UnityEngine;
using UnityEngine.InputSystem;

public class BatBoxCollider : MonoBehaviour
{
    [SerializeField]
    private BatFollower batFollowerPrefab;
    public int count;
    public InputActionProperty batTurn;

    private void SpawnBatFollowerPrefab()
    {
        var follower = Instantiate(batFollowerPrefab);
        follower.transform.position = transform.position;
        follower.SetFollowTarget(this);
    }

    private void Awake()
    {
        SpawnBatFollowerPrefab();
    }

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