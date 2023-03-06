using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBall : MonoBehaviour
{
    public GameObject ball;
    private Vector3 spawnPoint;
    public InputActionProperty holdGripButton;
    public InputActionProperty ballDestroyButton;
    private bool holded;
    public GameObject spawnedBall;
    public SliderText sliderValues;
    public PhysicMaterial planePhysicMaterial;
    public BatBoxCollider batScript;

    private void Start()
    {

    }

    private void Update()
    {
        if (spawnedBall)
        {
            spawnedBall.GetComponent<Rigidbody>().mass = sliderValues.massSlider.value;
            spawnedBall.GetComponent<BallForce>().ballSpeed = sliderValues.ballSpeedSlider.value;
            spawnedBall.GetComponent<Collider>().material.bounciness = sliderValues.bounceSlider.value;
            spawnedBall.GetComponent<Collider>().material.staticFriction = sliderValues.ballStaticSlider.value;
            spawnedBall.GetComponent<Collider>().material.dynamicFriction = sliderValues.ballDynamicSlider.value;
            spawnedBall.GetComponent<Rigidbody>().drag = sliderValues.dragSlider.value;
            
        }
        planePhysicMaterial.bounciness = sliderValues.groundBounceSlider.value;
        planePhysicMaterial.staticFriction = sliderValues.groundStaticSlider.value;
        planePhysicMaterial.dynamicFriction = sliderValues.groundDynamicSlider.value;

        
    }

    private void FixedUpdate()
    {
        spawnPoint = gameObject.transform.position;

        //if (ballDestroyButton.action.WasPressedThisFrame())
        //{
        //    Destroy(spawnedBall);
        //}

        if (holdGripButton.action.ReadValue<float>() == 1)
        {
            if (!holded)
            {
                holded = true;
                StartCoroutine(holdButton());
            }
        }
        else
        {
            holded = false;
        }
    }

    IEnumerator waitBeforeDestroy()
    {
        yield return new WaitForSeconds(1);

        //Destroy(spawnedBall);
    }

    IEnumerator holdButton()
    {
        yield return new WaitForSeconds(0.5f);

        spawnedBall.transform.position = spawnPoint;
        spawnedBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void BallInstantiate()
    {
        spawnedBall = Instantiate(ball, spawnPoint, Quaternion.identity);
        holded = false;
    }
}
