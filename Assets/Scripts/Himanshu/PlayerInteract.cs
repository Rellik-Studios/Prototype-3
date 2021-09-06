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

        private void Unhide()
        {
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
            m_hidingSpot = _hidingSpot;
            GetComponent<CharacterController>().enabled = false;
            Debug.Log("Hiding now");
            m_hiding = true;
        }

        public void SetPositionAndRotation(Transform _transform)
        {
            GetComponent<CharacterController>().enabled = false;
            transform.position = _transform.position;
            m_playerFollow.SetRotation(_transform, new Vector2(-30, 30));
            GetComponent<CharacterController>().enabled = true;
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