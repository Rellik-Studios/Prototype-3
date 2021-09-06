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
        public int hidingIndex
        {
            get => m_hidingIndex;
            set
            {
                m_hidingIndex = value < 0 ? 0 : value > m_hidingSpots.Count - 1 ? m_hidingSpots.Count - 1 : value;
                if (m_player != null)
                {
                    m_player.SetPositionAndRotation(m_hidingSpots[m_hidingIndex]);
                    m_player.GetComponent<CharacterController>().enabled = false;
                }
            }
        }

        private void Start()
        {
            m_hidingSpots = new List<Transform>();

            var hidingSpots = transform.Find("HidingSpots");
            for (int i = 0; i < hidingSpots.childCount; i++)
            {
                m_hidingSpots.Add(hidingSpots.GetChild(i));
            }
        }

        public void Execute(PlayerInteract _player)
        {
            m_player = _player;
            m_player.SetPositionAndRotation(m_hidingSpots[hidingIndex]);
            _player.Hide(this);
        }

        public void Disable()
        {
            m_player = null;
        }

        private void Update()
        {
            if (m_player != null)
            {
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
    }
}