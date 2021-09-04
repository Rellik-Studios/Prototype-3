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
            if (_player.bulletCount > 0)
            {
                frozen = true;
                _player.Shoot();
                StartCoroutine(UnFreeze());
            }
        }

        private IEnumerator UnFreeze()
        {
            yield return new WaitForSeconds(6f);
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