using UnityEngine;

namespace Himanshu
{
    public class PlayerInteract : MonoBehaviour
    {
        private PlayerInput m_playerInput;
        private RaycastingTesting m_raycastingTesting;
        private void Start()
        {
            m_raycastingTesting = FindObjectOfType<RaycastingTesting>();
            m_playerInput = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (m_playerInput.interact)
            {
                m_raycastingTesting.ObjectInFront.GetComponent<IInteract>()?.Execute(this);
            }
        }
    }
}