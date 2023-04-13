using UnityEngine;
using System.Collections;
using TMPro;

public class BallScript : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 addForceVector;
    private bool ballStill, ballTouchedGround;
    public static bool inHand;
    public bool leftHand;
    public bool batHit;
    private int count;

    public float radius = 0.5f;
    public float airDensity = 0.1f;
    public float angVelocity = 15;

    public float ballSpeed = 15;
    public float magnusForceValue = 3;
    public Transform head;
    public TextMeshProUGUI countTextMesh;
    public GameObject CountCanvas;
    private BallSpawn_Script spawnBallScript;

    private LeftHand_ColliderScript checkColliderScript;
    private bool slowMotion;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        count = 0;
        CountCanvas = GameObject.Find("Count");
        countTextMesh = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();
        head = GameObject.Find("Main Camera").GetComponent<Transform>();
        spawnBallScript = GameObject.Find("Left Hand Model").GetComponent<BallSpawn_Script>();
        checkColliderScript = GameObject.Find("Left Hand").GetComponent<LeftHand_ColliderScript>();
        rb.maxAngularVelocity = 10;
    }

    void Update()
    {

        if (slowMotion)
        {
            StartCoroutine(SlowTime());
            Time.timeScale = 0.15f;
        }
        else
        {
            Time.timeScale = 1;
        }

        leftHand = checkColliderScript.leftHand;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, ballSpeed);
    }

    IEnumerator waitBeforeDestroy()
    {
        yield return new WaitForSeconds(2);

        ballStill = true;
    }

    void FixedUpdate()
    {
        if (leftHand)
        {
            if (spawnBallScript.holdGripButton.action.ReadValue<float>() > 0)
            {
                count = 0;
                inHand = true;
            }
        }

        if (count > 2)
        {
            CountCanvas.SetActive(true);
            countTextMesh.text = count.ToString();

            CountCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * 2;
            CountCanvas.transform.LookAt(new Vector3(head.position.x, CountCanvas.transform.position.y, head.position.z));
            CountCanvas.transform.forward *= -1;
        }
        else
        {
            CountCanvas.SetActive(false);
        }


        if (!leftHand)
        {
            if (inHand)
            {
                if (rb.velocity.y > (2 * rb.velocity.x) && rb.velocity.y > (2 * rb.velocity.z))
                {
                    /// Either limit velocity to y axis OR add force as impulse ---------
                    rb.velocity = new Vector3(0, rb.velocity.y + (rb.velocity.y/20), 0);
                    inHand = false;
                    //rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
                }
            }
        }
        if (ballStill && ballTouchedGround)
        {
            count = 0;
        }

        /// For Spins --------
        if (batHit)
        {
            var direction = Vector3.Cross(rb.angularVelocity, rb.velocity);         
            var magnitude = 4 / 3f * Mathf.PI * airDensity * Mathf.Pow(radius, 3);      
            rb.AddForce(((magnitude/100)* magnusForceValue) * direction);       
        }
    }

    IEnumerator SlowTime()
    {
        yield return new WaitForSeconds(0.25f);

        slowMotion = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Untagged"))
        {
            inHand = false;
            batHit = false;
        }

        if (collision.collider.CompareTag("Bat"))
        {
            inHand = false;
            batHit = true;
        }
        
        if (collision.collider.CompareTag("Finish"))
        {
            batHit = false;
            count = 0;
            inHand = false;
        }

        if (collision.collider.CompareTag("Respawn"))
        {
            batHit = false;
            inHand = false;
            count = 0;
            ballTouchedGround = true;

            if (rb.velocity.magnitude < 0.001f)
            {
                StartCoroutine(waitBeforeDestroy());
            }
            else
            {
                ballStill = false;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Respawn"))
        {
            ballTouchedGround = false;
        }

        if (collision.collider.CompareTag("Bat"))
        {
            count++;
        }

        if (rb.useGravity == false)
        {
            if (collision.collider.CompareTag("Bat"))
            {
                rb.useGravity = true;
                slowMotion = true;
            }
        }
    }
}
