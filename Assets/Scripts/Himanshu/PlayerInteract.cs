using UnityEngine;

namespace Himanshu
{
    public class PlayerInteract : MonoBehaviour
    {
        public bool interactHold => m_playerInput.interactHold;
        
        public bool m_hiding;
        private PlayerInput m_playerInput;
        private RaycastingTesting m_raycastingTesting;
        private void Start()
        {
            m_raycastingTesting = FindObjectOfType<RaycastingTesting>();
            m_playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (m_playerInput.interact && !m_hiding)
            {
                m_raycastingTesting.ObjectInFront?.GetComponent<IInteract>()?.Execute(this);
            }
            else
            {
                m_hiding = false;
            }
        }

        public void Hide()
        {
            Debug.Log("Hiding now");
        }

        public void Collect()
        {
            Debug.Log("Object Collected?");
        }

        public void Shoot()
        {
            Debug.Log("Dishoom");
        }
    }
}