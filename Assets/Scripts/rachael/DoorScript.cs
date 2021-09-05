using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;
    [SerializeField] private bool DoneOnce = true;
    [SerializeField] private bool isOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isOpened)
        {
            myDoor.SetBool("IsOpening", true);
        }
       
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //old anim

            //if(openTrigger && DoneOnce)
            //{
            //    myDoor.Play("DoorOpening", 0, 0.0f);
            //    DoneOnce = false;

            //}
            //if (closeTrigger && DoneOnce)
            //{
            //    myDoor.Play("DoorClosing", 0, 0.0f);
            //    DoneOnce = false;

            //}
            if(openTrigger)
                myDoor.SetBool("IsOpening", true);
            if(closeTrigger)
                myDoor.SetBool("IsOpening", false);


        }
    }

    public void DoorOpening()
    {
        myDoor.SetBool("IsOpening", true);
    }
    public void DoorClosing()
    {
        myDoor.SetBool("IsOpening", false);
    }

}
