using UnityEngine;

namespace Himanshu
{
    public class PlayerFollow : MonoBehaviour
    {

        [SerializeField] private Transform m_playerTransform;
        private float m_mouseX;
        private float m_mouseY;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            transform.position = m_playerTransform.position;

            m_mouseX += Input.GetAxis("Mouse X");
            m_mouseY += Input.GetAxis("Mouse Y");

            m_mouseY =  Mathf.Clamp(m_mouseY, -30f, 30f);

            transform.rotation = Quaternion.Euler(m_mouseY, m_mouseX, 0f);
            m_playerTransform.forward = new Vector3(transform.forward.x, 0f, transform.forward.z);

        }
    }
}
