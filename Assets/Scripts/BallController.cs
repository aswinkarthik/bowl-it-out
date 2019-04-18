using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class BallController : MonoBehaviour
{
    public GameObject ballObject;
    public Camera FirstPersonCamera;
    public Rigidbody rb;
    public float thrust;
    public float ballDistance = 20f;
    public float ballThrowingforce = 400f;

    private bool holdingBall = true;
    private bool activateBall = false;

    float speed = 2F;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        //ballObject.SetActive(false);
        //rb.isKinematic = false;
        //rb.detectCollisions = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBall && activateBall && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Debug.Log("Hello: " + touch.phase +"----"+ Input.touchCount);
            //touch.phase == TouchPhase.Moved
            if (Input.GetMouseButtonDown(0))
            {
                holdingBall = false;
                rb.useGravity = true;
                ballObject.GetComponent<Rigidbody>().useGravity = true;
                ballObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 50);
                //ballObject.transform.position = FirstPersonCamera.transform.position + FirstPersonCamera.transform.forward * ballDistance;
                //need to Add force
                //ApplyForce();
            }
        }

    }

    public void ActivateBall()
    {
        activateBall = true;
    }

    public void ApplyForce()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    public void FixedUpdate()
    {
        Vector3 acc = Input.acceleration;

        //rb.AddForce(acc.x * speed, 0, acc.y * speed);
    }

    public void SetActive(bool flag)
    {
        ballObject.SetActive(flag);
    }

}
