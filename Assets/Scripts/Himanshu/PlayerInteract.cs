using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Himanshu
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool interactHold => m_playerInput.interactHold;

        private int m_bulletCount = 1;

        public int bulletCount => m_bulletCount;
        public bool timeReverse { get; set; }

        public bool m_hiding;
        public PlayerInput m_playerInput;
        private RaycastingTesting m_raycastingTesting;
        private HidingSpot m_hidingSpot;
        private PlayerFollow m_playerFollow;
        
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
                transform.Translate(transform.forward);
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
        }

        public void Shoot()
        {
            m_bulletCount -= 1;
        }
    }
}