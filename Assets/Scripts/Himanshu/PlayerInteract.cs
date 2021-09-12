﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Himanshu
{

    public class PlayerInteract : MonoBehaviour
    {
        private bool m_spotted;
        public GameObject LoseScreen;

        private List<EnemyController> m_enemies;
        public  IEnumerator FillBar(Image _fillImage, float _time, int _dir = 1, float _waitTime = 0f)
        {
            yield return new WaitForSeconds(_waitTime);
            var time = 0f;

            while (time < _time)
            {
                time += Time.deltaTime;
                _fillImage.fillAmount += _dir * Time.deltaTime / _time;
                yield return null;
            }
        }

        private bool playingUp = false;
        private bool playingDown = false;
        private Coroutine m_fillRoutine;
        public bool spotted
        {
            get => m_spotted;
            set
            {
                if (m_spotted != value)
                {
                    if (value)
                    {
                        StartCoroutine(FillBar(m_danger, 8f));
                    }
                    else 
                    {
                        StartCoroutine(FillBar(m_danger, 3f, -1));
                    }
                    m_spotted = value;
                }
            }
        }

        [Header("Images")] 
        public Image m_timeRewind;
        public Image m_timeStop;
        public Image m_danger;


        public float dangerBarVal
        {
            get => m_danger.fillAmount;
            set => m_danger.fillAmount = value;
        }
        public bool interactHold => m_playerInput.interactHold;

        private int m_bulletCount = 1;

        public int bulletCount
        {
            get => m_bulletCount;
            private set => m_bulletCount = value;
        }

        public bool timeReverse { get; set; }

        [Header("General")]
        public bool m_hiding;
        public int m_numOfPieces = 0;
        public bool m_placedDown = false;

        public PlayerInput m_playerInput;
        private RaycastingTesting m_raycastingTesting;
        private HidingSpot m_hidingSpot;
        private PlayerFollow m_playerFollow;


        private void OnEnable()
        {
            m_enemies = GameObject.FindObjectsOfType<EnemyController>().ToList();
        }

        private void Start()
        {
            m_playerFollow = GameObject.FindObjectOfType<PlayerFollow>();
            m_raycastingTesting = FindObjectOfType<RaycastingTesting>();
            m_playerInput = GetComponent<PlayerInput>();
            timeReverse = true;

        }

        private void Update()
        {
            if (m_playerInput.interact && !m_hiding)
            {
                m_raycastingTesting.ObjectInFront?.GetComponent<IInteract>()?.Execute(this);
            }
            else if(m_playerInput.interact)
            {
                Unhide();
            }

            if (m_playerInput.interact)
            {
                m_raycastingTesting.ObjectInFront?.GetComponent<IEnemy>()?.Shoot(this);
            }

            if (dangerBarVal == 1f)
            {
                LoseScreen.SetActive(true);
                dangerBarVal = 0;
                gameObject.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                //SceneManager.LoadScene(1);
            }
            spotted = m_enemies.Any(_enemy => _enemy.m_spotted);
            
            if(Input.GetKeyDown(KeyCode.B))
                StartCoroutine(TimeHandler());
        }

        public void Unhide()
        {
            if (m_hidingSpot.m_cupboard)
            {
                StartCoroutine(eUnHide());
            }
            else
            {
                transform.Translate(-transform.forward * 2f);
                GetComponent<CharacterController>().enabled = true;
                m_playerFollow.ResetRotationLock();
                m_hidingSpot.Disable();
                m_hiding = false;
                m_hidingSpot = null;
                
            }
        }

        private IEnumerator eUnHide()
        {
            m_hidingSpot.aOpen = true;
            m_hidingSpot.aClose = false;
            yield return new WaitForSeconds(1f);
            transform.Translate(-transform.forward * 3f);
            m_hidingSpot.aOpen = false;
            m_hidingSpot.aClose = true;
            transform.Translate(transform.forward);
            GetComponent<CharacterController>().enabled = true;
            m_playerFollow.ResetRotationLock();
            m_hidingSpot.Disable();
            m_hiding = false;
            m_hidingSpot = null;            
        }

        private IEnumerator TimeHandler()
        {
            if (m_bulletCount > 0)
            {
                m_bulletCount--;
                Time.timeScale = 0.1f;

                yield return new WaitForSeconds(6f * Time.timeScale);

                Time.timeScale = 1f;
            }
        }

        public void Hide(HidingSpot _hidingSpot)
        {
            if (_hidingSpot.m_cupboard)
            {
                StartCoroutine(eHide(_hidingSpot));
            }
            else
            {
                m_hidingSpot = _hidingSpot;
                GetComponent<CharacterController>().enabled = false;
                Debug.Log("Hiding now");
                m_hiding = true;
            }
        }

        private IEnumerator eHide(HidingSpot _hidingSpot)
        {
            _hidingSpot.aOpen = true;
            _hidingSpot.aClose = false;
            yield return new WaitForSeconds(1f);
            _hidingSpot.aOpen = false;
            _hidingSpot.aClose = true;
            
            m_hidingSpot = _hidingSpot;
            GetComponent<CharacterController>().enabled = false;
            Debug.Log("Hiding now");
            m_hiding = true;            
        }

        public void SetPositionAndRotation(Transform _transform, float _delay = 0)
        {
            StartCoroutine(eSetPositionAndRotation(_transform, _delay));
        }

        private IEnumerator eSetPositionAndRotation(Transform _transform,float _delay)
        {
            yield return new WaitForSeconds(_delay);
            GetComponent<CharacterController>().enabled = false;
            transform.position = _transform.position;
            m_playerFollow.SetRotation(_transform, new Vector2(-30, 30));
            //GetComponent<CharacterController>().enabled = true;
        }

        public void Collect()
        {
            Debug.Log("Object Collected?");
            m_placedDown = true;
            m_numOfPieces++;
        }

        public void Shoot()
        {
            m_bulletCount -= 1;
                        
            this.Invoke(() => { bulletCount = 1; }, 6f);
           
            
            StartCoroutine(m_timeStop.FillBar(0.1f));
            StartCoroutine(m_timeStop.FillBar(6f, -1));
        }

        public void UnSpot()
        {
            dangerBarVal = 0f;
            foreach (var enemy in m_enemies)
            {
                enemy.m_spotted = false;
            }
        }
    }
}