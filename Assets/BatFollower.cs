using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFollower : MonoBehaviour
{
    private BatBoxCollider _batFollower;
    private Rigidbody rb;
    private Vector3 velocity;

    public float batSpeed = 10;
    private SliderText sliderValues;
    private GameObject rightHand;
    public float sensitivity = 85f;
    private Quaternion orgRotate;
    private bool rotateBack;
    private bool rotatedBack;
    private bool ballHit;
    private bool batMove;
    private int count;
    private PhysicMaterial batPhysicMat;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sliderValues = GameObject.Find("Canvas").GetComponent<SliderText>();
        rightHand = GameObject.Find("HandRight");
        batPhysicMat = gameObject.GetComponent<Collider>().material;
    }

    private void Update()
    {
        sensitivity = sliderValues.sensitivitySlider.value;
        batSpeed = sliderValues.batSpeedSlider.value;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, batSpeed);
        batPhysicMat.bounciness = sliderValues.batBounceSlider.value;
        batPhysicMat.staticFriction = sliderValues.batStaticSlider.value;
        batPhysicMat.dynamicFriction = sliderValues.batDynamicSlider.value;
    }

    private void FixedUpdate()
    {


        if (rb.velocity.magnitude < batSpeed * 0.8f)
        {
            rb.transform.rotation = _batFollower.transform.rotation;
        }
        else
        {
            rb.transform.Rotate(_batFollower.transform.rotation.x, _batFollower.transform.rotation.y, _batFollower.transform.rotation.z - (2f * rb.velocity.normalized.z));
        }

        Vector3 dest = _batFollower.transform.position;
        //rb.transform.rotation = _batFollower.transform.rotation;
        velocity = (dest - rb.transform.position) * sensitivity;

        rb.velocity = velocity;

    }

    public void SetFollowTarget(BatBoxCollider batFollower)
    {
        _batFollower = batFollower;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
