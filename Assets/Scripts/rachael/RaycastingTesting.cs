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
        Debug.DrawRay(transform.position, transform.forward * raydist, Color.green, 0.1f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, raydist);


        foreach (var hit in hits)
        {
            //hit object doesnt detect itself
            if (hit.collider.gameObject != gameObject)
            {
                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log($"hit {hit.collider.gameObject.name}");
            }
        }
    }
}
