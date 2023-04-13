using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawn_Script : MonoBehaviour
{
    private Vector3 spawnPoint;
    public InputActionProperty holdGripButton;
    public InputActionProperty ballDestroyButton;
    private bool holded;
    public GameObject spawnedBall;
    public PhysicMenu_SliderScript sliderValues;
    public PhysicMaterial planePhysicMaterial;
    public Bat_PrefabScript batScript;

    public static bool resetPressed;

    private void Start()
    {
        sliderValues = GameObject.FindObjectOfType<PhysicMenu_SliderScript>();
    }

    private void Update()
    {
        if (BallScript.inHand)
        {
            UpdatePhysicValue();
        }
    }

    private void FixedUpdate()
    {
        spawnPoint = gameObject.transform.position;

        if (ballDestroyButton.action.WasPressedThisFrame())
        {
            resetPressed = true;
        }

        if (!GameManagerScript.pathActive)
        {
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
    }

    void UpdatePhysicValue()
    {
        spawnedBall.GetComponent<Rigidbody>().mass = sliderValues.massSlider.value;
        spawnedBall.GetComponent<BallScript>().ballSpeed = sliderValues.ballSpeedSlider.value;
        spawnedBall.GetComponent<BallScript>().magnusForceValue = sliderValues.ballSpeedSlider.value;
        spawnedBall.GetComponent<Collider>().material.bounciness = sliderValues.bounceSlider.value;
        //spawnedBall.GetComponent<Collider>().material.staticFriction = sliderValues.ballStaticSlider.value;
        //spawnedBall.GetComponent<Collider>().material.dynamicFriction = sliderValues.ballDynamicSlider.value;
        spawnedBall.GetComponent<Rigidbody>().drag = sliderValues.dragSlider.value;
        planePhysicMaterial.bounciness = sliderValues.groundBounceSlider.value;
        //planePhysicMaterial.staticFriction = sliderValues.groundStaticSlider.value;
        //planePhysicMaterial.dynamicFriction = sliderValues.groundDynamicSlider.value;
    }

    IEnumerator holdButton()
    {
        yield return new WaitForSeconds(0.75f);

        spawnedBall.transform.position = spawnPoint;
        spawnedBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
