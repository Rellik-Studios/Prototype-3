using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastingTesting : MonoBehaviour
{
    [SerializeField] private float raydist = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.forward * raydist, Color.green, 0.1f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.forward, raydist);


        for (int i = 0; i < hits.Length; i++)
        {
            //hit object doesnt detect itself
            if (hits[i].collider.gameObject != gameObject)
            {
                Debug.Log("did hit");
            }

        }
    }
}
