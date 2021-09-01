using System;
using UnityEngine;

namespace Himanshu
{
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController m_characterController;
        private Rigidbody m_rigidbody;

        private void Start()
        {
            m_rigidbody = transform.Find("GFX").gameObject.GetComponent<Rigidbody>();
            m_characterController = GetComponent<CharacterController>();
        }


        private void Update()
        {
            
        }
    }
}