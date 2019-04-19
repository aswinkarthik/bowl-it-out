using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class BallController : MonoBehaviour
{
    public GameObject ballObject;
    public GameObject targetObject;
    public Camera FirstPersonCamera;
    public Rigidbody rb;
    public float thrust;
    public float ballDistance = 20f;
    public float ballThrowingforce = 2000f;

    private bool holdingBall = true;
    private bool activateBall = false;
    private bool removeObjects = false;

    [Range(5, 50)]
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBall && activateBall && Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                holdingBall = false;
                ballObject.GetComponent<Rigidbody>().useGravity = true;
                ballObject.GetComponent<Rigidbody>().detectCollisions = true;
                ApplyForce();
            }
        }


        if (removeObjects)
        {
            ballObject.SetActive(false);
            targetObject.SetActive(false);
        }

    }

    public void ActivateBall()
    {
        activateBall = true;
    }

    public void ApplyForce()
    {
        float step = speed * Time.deltaTime;
        ballObject.GetComponent<Rigidbody>().AddForce((FirstPersonCamera.transform.forward) * ballThrowingforce * speed);
    }

    public void SetActive(bool flag)
    {
        ballObject.SetActive(flag);
    }

    private void FixedUpdate()
    {
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        if(!holdingBall)
        transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enters----"+collision.gameObject.name);

        if (collision.gameObject.name == "pins" || collision.gameObject.CompareTag("pin"))
        {
            Debug.Log("Collision destroy----" + collision.gameObject.name);
            removeObjects = true;
            Destroy(ballObject);
            Destroy(collision.gameObject);
            //Add score
        }
    }


}
