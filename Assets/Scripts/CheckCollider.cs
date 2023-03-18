using UnityEngine;

public class CheckCollider : MonoBehaviour
{
    public bool leftHand;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            leftHand = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            leftHand = false;
        }
    }
}
