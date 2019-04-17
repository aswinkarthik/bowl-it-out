using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class BallController : MonoBehaviour
{
    public GameObject ballObject;
    public Rigidbody rb;
    public float thrust;
    //float speed = 1F;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        //rb.detectCollisions = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Translate(Vector3.back * Time.deltaTime);
    }

    public void ApplyForce()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    //public void FixedUpdate()
    //{
    //    Vector3 acc = Input.acceleration;
    //    rb.AddForce(acc.x * speed, 0, acc.y * speed);
    //}
}
