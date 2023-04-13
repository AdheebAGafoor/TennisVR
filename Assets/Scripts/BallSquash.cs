using System.Collections;
using UnityEngine;

public class BallSquash : MonoBehaviour
{
    private Rigidbody rb;
    private bool pressed;
    public float radius = 0.5f;
    public float airDensity = 0.1f;
    public float angVelocity = 20;
    public float ballSpeed = 15;
    public float value = 5;
    private Vector3 prevFrameRotation;
    public float rpm;

    private Vector3 dir;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        prevFrameRotation = Vector3.zero;
    }

    private void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, ballSpeed);

        //gameObject.transform.parent.transform.position = gameObject.transform.position;
        rpm = (transform.rotation.eulerAngles - prevFrameRotation).magnitude * 60;
        prevFrameRotation = transform.rotation.eulerAngles;
    }

    private void FixedUpdate()
    {
        rb.maxAngularVelocity = angVelocity;

        var direction = Vector3.Cross(rb.angularVelocity, rb.velocity);
        var magnitude = 4 / 3f * Mathf.PI * airDensity * Mathf.Pow(radius, 3);
        rb.AddForce(magnitude * direction);

        dir = direction;

        //if (rb.angularVelocity.x > 0)
        //{
        //    rb.AddForce(0, 0, 1, ForceMode.Impulse);
        //}


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //rb.AddForce(-1, 0, 0, ForceMode.Impulse);
            rb.angularVelocity = new Vector3(angVelocity, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //rb.AddForce(1, 0, 0, ForceMode.Impulse);
            rb.angularVelocity = new Vector3(-angVelocity, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(0, 0, value, ForceMode.Impulse);

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.AddForce(0, 0, -value, ForceMode.Impulse);

        }

        //Debug.Log(" Velocity : " + gameObject.GetComponent<Rigidbody>().velocity.magnitude);
        //Debug.Log(gameObject.transform.rotation.eulerAngles);
        StartCoroutine(nameof(CheckRotation));
        Quaternion rot = transform.rotation;
        Vector3 eRot = rot.eulerAngles;

        Vector3 vel = rb.velocity;

        //Debug.Log("eRot : " + eRot);
        //Debug.Log("vel : " + vel);

        //rb.AddTorque(new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, value, 0, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!pressed)
            {
                pressed = true;
            }
            else
            {
                pressed = false;
            }
        }

        if (pressed)
        {
            ///transform.Rotate(new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z) * 100 * Time.deltaTime);

            //transform.Rotate(new Vector3(0, 0, 10) * 100 * Time.deltaTime);
            //rb.AddTorque(new Vector3(0, 0, 10) * 100 * Time.deltaTime);
        }
    }

    private IEnumerator CheckRotation()
    {
        Vector3 rot = transform.rotation.eulerAngles;

        yield return new WaitForFixedUpdate();

        //Debug.Log(rot - transform.rotation.eulerAngles);
        StopCoroutine(nameof(CheckRotation));
    }

    IEnumerator Squash()
    {
        yield return new WaitForSeconds(0.03f);

        //gameObject.GetComponent<Rigidbody>().isKinematic = false;

        //gameObject.transform.GetChild(0).transform.localScale = new Vector3(0.7292714f, 0.7292714f, 0.7292714f);
        pressed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 2)
        //{
        //    gameObject.transform.GetChild(0).transform.localScale = new Vector3(0.85f, 0.7292714f, 0.85f);
        //    StartCoroutine(Squash());
        //    pressed = true;
        //}

        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 2)
        {
            //Debug.Log(" Working ");
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //StartCoroutine(Squash());
        }

        if (rb.angularVelocity.x > 0 && rb.velocity.z > 0)   /// topspin along z axis
        {
            rb.AddForce((2 * rb.velocity.z) * -dir);
        }
        else if (rb.angularVelocity.x < 0 && rb.velocity.z > 0)   /// backspin along z axis
        {
            rb.AddForce((2 * -rb.velocity.z) * -dir);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
    }
}
