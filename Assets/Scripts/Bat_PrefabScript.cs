using UnityEngine;


public class Bat_PrefabScript : MonoBehaviour
{
    [SerializeField]
    private BatFollower batFollowerPrefab;
    public int count;
    

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
}