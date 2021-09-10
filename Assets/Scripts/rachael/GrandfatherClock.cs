using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Himanshu;
public class GrandfatherClock : MonoBehaviour, IInteract
{
    [SerializeField] GameObject m_triggerDoor;
    [SerializeField] GameObject m_goalDoor;

    [SerializeField] GameObject Gears;
    [SerializeField] GameObject Face;
    [SerializeField] GameObject Gong;
    [SerializeField] GameObject Hands;
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
    //when the piece of the clock is being placed
    public void Execute(PlayerInteract _player)
    {
        
        switch (_player.m_numOfPieces)
        {
            case 1:
                Gears.SetActive(true);
                m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
                break;
            case 2:
                Face.SetActive(true);
                m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
                break;
            case 3:
                Gong.SetActive(true);
                m_triggerDoor.GetComponent<DoorScript>().DoorOpening();
                break;
            case 4:
                Hands.SetActive(true);
                OpenTheDoor();
                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }
        
    }
}
