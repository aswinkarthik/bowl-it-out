using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject plane;
    public BallController ballController;

    // Start is called before the first frame update
    void Start()
    {
        plane.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActive(bool flag)
    {
        plane.SetActive(flag);
        ballController.ActivateBall();
    }
}
