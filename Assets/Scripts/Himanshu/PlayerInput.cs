﻿using System;
using System.Linq;
using UnityEngine;

namespace Himanshu
{
    public class PlayerInput : MonoBehaviour
    {
        private float m_horizontal;
        private float m_vertical;
        private bool m_jump;
        private bool m_interact;
        private bool m_interactHold;

        public bool interact => m_interact;
        public bool interactHold => m_interactHold;

        public Vector3 movement => new Vector3(m_horizontal, 0f, m_vertical);

        public bool jump => m_jump;
        private void Update()
        {
            m_horizontal = Input.GetAxis("Horizontal");
            m_vertical = Input.GetAxis("Vertical");
            m_jump = Input.GetButtonDown("Jump");
            m_interact = Input.GetButtonDown("Interact");
            m_interactHold = Input.GetButton("Interact");
        }
    }
}