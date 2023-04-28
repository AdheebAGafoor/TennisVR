using System.Collections;
using UnityEngine;

public class BatFollower : MonoBehaviour
{
    private Bat_PrefabScript _batFollower;
    private Rigidbody rb;
    private Vector3 velocity;

    public float batSpeed = 17;
    private PhysicMenu_SliderScript sliderValues;
    public float sensitivity = 85f;
    private PhysicMaterial batPhysicMat;

    private GameManagerScript gameManager;

    [HideInInspector]
    public static bool hitStarted, hitFinished, timeUp, hit, completed;

    [HideInInspector]
    public static int hitNumber;
    public static int lastHit = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sliderValues = GameObject.FindObjectOfType<PhysicMenu_SliderScript>();
        batPhysicMat = gameObject.GetComponent<Collider>().material;
        gameManager = GameObject.FindObjectOfType<GameManagerScript>();
    }

    private void Update()
    {
        if (PhysicGame_MenuManager.physicBackButtonPressed)
        {
            UpdatePhysicValue();
            PhysicGame_MenuManager.physicBackButtonPressed = false;
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < batSpeed * 0.8f)
        {
            rb.transform.rotation = _batFollower.transform.rotation;
        }
        else
        {
            rb.transform.Rotate(_batFollower.transform.rotation.x, _batFollower.transform.rotation.y, _batFollower.transform.rotation.z - (2f * rb.velocity.normalized.z)/0.75f);
        }

        Vector3 dest = _batFollower.transform.position;
        velocity = (dest - rb.transform.position) * sensitivity;
        rb.velocity = velocity;
    }

    public void SetFollowTarget(Bat_PrefabScript batFollower)
    {
        _batFollower = batFollower;
    }

    public void UpdatePhysicValue()
    {
        batSpeed = sliderValues.batSpeedSlider.value;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, batSpeed);
        batPhysicMat.bounciness = sliderValues.batBounceSlider.value;

        //sensitivity = sliderValues.sensitivitySlider.value;
        //batPhysicMat.staticFriction = sliderValues.batStaticSlider.value;
        //batPhysicMat.dynamicFriction = sliderValues.batDynamicSlider.value;
    }

    IEnumerator PathHit()
    {
        yield return new WaitForSeconds(1);

        timeUp = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManagerScript.completeCalled)
        {
            if (other.CompareTag("Start"))
            {
                if (!hit)
                {
                    hitStarted = true;
                    other.gameObject.transform.parent.gameObject.SetActive(false);
                    StartCoroutine(PathHit());
                }
            }

            if (other.CompareTag("End"))
            {
                hitFinished = true;
                completed = true;
                other.gameObject.transform.parent.gameObject.SetActive(false);
            }

            if (other.CompareTag("Serve"))
            {
                if (!hit && !hitStarted)
                {
                    StartCoroutine(PathHit());
                }

                for (int i = lastHit; i < 6; i++)
                {
                    if (gameManager.pathObjects[i].name == other.transform.parent.name)
                    {
                        lastHit = i;
                        hit = true;
                        other.gameObject.transform.parent.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
