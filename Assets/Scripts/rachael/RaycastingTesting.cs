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
        //detecting all hits from raycast
        Debug.DrawRay(transform.position, transform.forward * raydist, Color.green, 0.1f);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, raydist);

        /*
        foreach (var hit in hits)
        {
            //hit object doesnt detect itself
            if (hit.collider.gameObject != gameObject)
            {
                //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log($"hit {hit.collider.gameObject.name}");

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Item") && Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Found item");
                }
            }
        }
        */

        //if they arent any hits
        if (hits.Length == 0)
        {
            return;
        }

        //if there is a hit take the first one as a default
        RaycastHit closestHit = hits[0];

        //find the closest hit
        foreach (var hit in hits)
        {
            if (closestHit.distance > hit.distance)
                closestHit = hit;
        }

        //when the Button down is pressed
        if (Input.GetMouseButtonDown(0))
        {
            ObjectInteraction(closestHit.collider.gameObject.layer);
            
        }

    }

    void ObjectInteraction(LayerMask layer)
    {
        if(LayerMask.NameToLayer("Item") == layer)
        {
            print("You found a Item");
            return;
            
        }
        if (LayerMask.NameToLayer("HidingSpot") == layer)
        {
            print("You found a Hiding Spot");
            return;
        }



    }
}
