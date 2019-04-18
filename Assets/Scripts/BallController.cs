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
    public float ballDistance = 2f;
    public float ballThrowingforce = 5f;

    private bool holdingBall = true;
    private bool activateBall = false;

    float speed = 1F;

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
        if (holdingBall && activateBall && Input.touchCount > 1)
        {
            ballObject.transform.position = FirstPersonCamera.transform.position + FirstPersonCamera.transform.forward * ballDistance;

            Touch touch = Input.GetTouch(0);

            Debug.Log("Hello: " + touch.phase +"----"+ Input.touchCount);

            if (touch.phase == TouchPhase.Moved)
            {
                holdingBall = false;
                rb.useGravity = true;
                //need to Add force
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
        rb.AddForce(acc.x * speed, 0, acc.y * speed);
    }

    public void SetActive(bool flag)
    {
        ballObject.SetActive(flag);
    }

}
