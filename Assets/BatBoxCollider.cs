using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


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

    private void Start()
    {
        SpawnBatFollowerPrefab();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (batTurn.action.ReadValue<Vector2>().x < 0)
        {
            transform.Rotate(0, 2, 0, Space.Self);
        }
        if (batTurn.action.ReadValue<Vector2>().x > 0)
        {
            transform.Rotate(0, -2, 0, Space.Self);
        }
    }
}