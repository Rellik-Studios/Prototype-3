using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeRoom : MonoBehaviour
{
    //temporary value until we fixed this on player
    bool IsSafe = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //when they enter safe room
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsSafe = true;
            Debug.Log("You have entered the safe room");
        }
    }
    //when they exit safe room
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsSafe = false;
            Debug.Log("You have exited the safe room");
        }
    }
}
