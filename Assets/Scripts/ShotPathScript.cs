using UnityEngine;

public class ShotPathScript : MonoBehaviour
{
    public GameObject[] pathObjects;
    public GameObject Line;

    private void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            pathObjects[i] = gameObject.transform.GetChild(0).transform.GetChild(i).gameObject;
        }

        Line = gameObject.transform.GetChild(1).gameObject;
    }


}
