using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject PlayerObject;
    [SerializeField] Transform m_playerTransform;
    Transform currentTransform;
    private Vector3 position;
    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        if (m_playerTransform != null)
        {
            currentTransform = m_playerTransform;
            position = currentTransform.position;
            rotation = currentTransform.rotation;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = position;
            gameObject.transform.rotation = rotation;
            GetComponent<CharacterController>().enabled = true;
        }
    }
    public void SetPosition(Transform checkpoint)
    {
        position = checkpoint.position;
        rotation = checkpoint.rotation;

    }

}
