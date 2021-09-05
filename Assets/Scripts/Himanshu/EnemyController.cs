using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Himanshu
{
    public class EnemyController : MonoBehaviour, IEnemy
    {

        [SerializeField] private float m_attackTimer;
        private float m_defaultAttackTimer;
        public UnityEvent m_command;
        [FormerlySerializedAs("frozen")] public bool m_frozen = false;
        public bool m_commandFinished { get; set; }

        
        //Called through the Visual Script
        public void RunCommand()
        {
            m_command?.Invoke();
            m_commandFinished = true;
        }
        private void Start()
        {
            m_defaultAttackTimer = m_attackTimer;
        }


        private IEnumerator UnFreeze()
        {
            yield return new WaitForSeconds(6f);
            m_frozen = false;
        }

        //Called through the Visual Script
        public void Attack()
        {
            m_attackTimer -= Time.deltaTime;
            if (m_attackTimer < 0f)
            {
                Debug.Log("attacking");
                m_attackTimer = m_defaultAttackTimer;
            }
        }

        //Called through the Visual Script
        public void ResetAttack()
        {
            m_attackTimer = m_defaultAttackTimer;
        }

        
        //Interface Requirement
        public void Shoot(PlayerInteract _player)
        {
            if (_player.bulletCount > 0)
            {
                m_frozen = true;
                _player.Shoot();
                StartCoroutine(UnFreeze());
            }
        }
    }
}