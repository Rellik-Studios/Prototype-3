using System;
using System.Collections;
using System.Collections.Generic;
using Bolt;
using Cinemachine;
using UnityEngine;

namespace Himanshu
{
    public class HidingSpot : MonoBehaviour, IInteract
    {

        private List<Transform> m_hidingSpots;
        private int m_hidingIndex;
        private PlayerInteract m_player;
        public bool m_cupboard;
        
        private Animator m_animator;
        private bool aInfect
        {
            get => m_animator.GetBool("infect");
            set => m_animator.SetBool("infect", value);
        }
        private bool aDisInfect
        {
            get => m_animator.GetBool("disinfect");
            set => m_animator.SetBool("disinfect", value);
        }
        
        private float aSpeed
        {
            get => m_animator.GetFloat("speed");
            set => m_animator.SetFloat("speed", value);
        }

        public bool aOpen
        {
            get => m_animator.GetBool("open");
            set => m_animator.SetBool("open", value);
        }
        
        public bool aClose
        {
            get => m_animator.GetBool("close");
            set => m_animator.SetBool("close", value);
        }
        
        public bool isActive
        {
            get;
            set;
        }
        
        public int hidingIndex
        {
            get => m_hidingIndex;
            set
            {
                if(m_hidingSpots.Count < 4)
                    m_hidingIndex = value < 0 ? 0 : value > m_hidingSpots.Count - 1 ? m_hidingSpots.Count - 1 : value;
                else
                    m_hidingIndex = value < 0 ? m_hidingSpots.Count - 1 : value > m_hidingSpots.Count - 1 ? 0 : value;

                if (m_player != null)
                {
                    m_player.SetPositionAndRotation(m_hidingSpots[m_hidingIndex]);
                    m_player.GetComponent<CharacterController>().enabled = false;
                }
            }
        }

        public bool infectStared { get; set; }

        private void Start()
        {
            m_animator = GetComponent<Animator>();
            isActive = true;
            m_hidingSpots = new List<Transform>();

            var hidingSpots = transform.Find("HidingSpots");
            for (int i = 0; i < hidingSpots.childCount; i++)
            {
                m_hidingSpots.Add(hidingSpots.GetChild(i));
            }
        }

        public void Execute(PlayerInteract _player)
        {
            if (isActive)
            {
                m_player = _player;
                m_player.SetPositionAndRotation(m_hidingSpots[hidingIndex], m_cupboard ? 1.0f : 0f);
                _player.Hide(this);
            }

            else
            {
                if (_player.timeReverse)
                {
                    _player.timeReverse = false;
                    DisInfect();
                }
            }
        }

        private void DisInfect()
        {
            infectStared = false;
            
            aDisInfect = true;
            aInfect = false;
            aSpeed = 1f / 2f;
            
            StartCoroutine(eInfect(true, 2f));
        }

        public void Disable()
        {
            m_player = null;
        }

        private void Update()
        {
            if (m_player !=  null)
            {
                if (!isActive)
                {
                    m_player.Unhide();
                    return;
                }
                StartCoroutine(IndexHandler());
            }
        }

        private IEnumerator IndexHandler()
        {
            var movement = m_player.m_playerInput.movement;

            if (Input.GetKeyDown(KeyCode.A))
            {
                hidingIndex--;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                hidingIndex++;
            }

            yield return null;
        }

        public void Infect()
        {
            
            aDisInfect = false;
            aInfect = true;
            aSpeed = 1f / 3f;

            infectStared = true;
            StartCoroutine(eInfect(false, 3f));
        }

        IEnumerator eInfect(bool _state, float _time)
        {
            //Gradually apply Distortion here
            yield return new WaitForSeconds(_time);
            isActive = _state;

            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).TryGetComponent<Renderer>(out Renderer _renderer))
                    _renderer.material.color = _state? Color.white : Color.red;
            }
            
        }
    }
}