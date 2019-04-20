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
    public float ballThrowingforce = 800f;

    private bool holdingBall = true;
    private bool activateBall = false;
    private bool removeObjects = false;

    [Range(5, 50)]
    public float speed = 5f;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        originalPos = new Vector3(ballObject.transform.position.x,ballObject.transform.position.y,ballObject.transform.position.z);
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
                targetObject.GetComponent<Rigidbody>().useGravity = true;
                ApplyForce();
            }
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
        if(!holdingBall && !removeObjects)
            transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enters----"+collision.gameObject.name);

        if (collision.gameObject.name == "Pins" || collision.gameObject.CompareTag("pin"))
        {
            Debug.Log("Collision destroy----" + collision.gameObject.name);
            StartCoroutine(CollisionAfterMath());

        }
    }



    IEnumerator CollisionAfterMath()
    {
        yield return new WaitForSeconds(2);
        removeObjects = true;
        ballObject.SetActive(false);
        targetObject.SetActive(false);
        UpdateScore();
    }


    void UpdateScore()
    {
        ScoreBoard.Score += 1;
    }
}
