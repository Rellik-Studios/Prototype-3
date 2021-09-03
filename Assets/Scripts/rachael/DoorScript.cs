using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private bool DoneOnce = true;
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
        if(other.CompareTag("Player"))
        {
            if(openTrigger && DoneOnce)
            {
                myDoor.Play("DoorOpen", 0, 0.0f);
                DoneOnce = false;

            }
            if (closeTrigger && DoneOnce)
            {
                myDoor.Play("DoorClose", 0, 0.0f);
                DoneOnce = false;

            }
        }
    }
}
