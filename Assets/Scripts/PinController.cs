using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter PIN CONTROLLER----" + other.gameObject.name);
        if (other.gameObject.name == "Ball")
        {
            Destroy(other.gameObject);
        }
    }
}
