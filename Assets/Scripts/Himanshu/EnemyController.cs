using System;
using System.Collections;
using UnityEngine;

namespace Himanshu
{
    public class EnemyController : MonoBehaviour, IInteract
    {

        [SerializeField] private float m_attackTimer;
        private float m_defaultAttackTimer;

        public bool frozen = false;

        private void Start()
        {
            m_defaultAttackTimer = m_attackTimer;
        }

        public void Execute(PlayerInteract _player)
        {
            frozen = true;

            StartCoroutine(UnFreeze());
        }

        private IEnumerator UnFreeze()
        {
            yield return new WaitForSeconds(3f);
            frozen = false;
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
    }
}