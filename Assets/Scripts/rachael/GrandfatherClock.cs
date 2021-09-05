using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandfatherClock : MonoBehaviour
{
    [SerializeField] GameObject m_triggerDoor;
    [SerializeField] GameObject m_goalDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnableTriggerDoor()
    {
        //if a peace is placed on clock
        m_triggerDoor.SetActive(true);
    }

    void OpenTheDoor()
    {
        //once all the piece are put in place then the door opens
        m_goalDoor.GetComponent<DoorScript>().DoorOpening();
    }
}
