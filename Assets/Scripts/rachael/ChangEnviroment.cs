using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangEnviroment : MonoBehaviour
{
    public GameObject[] EnvirObject;
    private int Index = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject obj in EnvirObject)
        {
            obj.SetActive(false);
        }
        if(EnvirObject.Length !=0)
        {
            EnvirObject[0].SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Index <= (EnvirObject.Length - 2))
            {
                //disable the object
                EnvirObject[Index].SetActive(false);
                Index++;
                //after raising the index by one
                EnvirObject[Index].SetActive(true);
            }

            Debug.Log("Environment has changed");
        }
    }
}
