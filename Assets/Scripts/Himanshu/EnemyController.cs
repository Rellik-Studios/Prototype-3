using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Himanshu
{
    public class EnemyController : MonoBehaviour, IEnemy
    {
        [Header("Attack")]
        [SerializeField] private float m_attackTimer;
        private float m_defaultAttackTimer;

        [Header("Patrol")] 
        [SerializeField] private float m_defaultPatrolWaitTime;

        [Header("Infect")] 
        [SerializeField] private List<HidingSpot> m_hidingSpotsToInfect;

        private HidingSpot m_hidingSpotToInfect;
        
        public UnityEvent m_command;
        [FormerlySerializedAs("frozen")] public bool m_frozen = false;
        public bool m_commandFinished { get; set; }

        

        private List<Transform> m_patrolPoints = new List<Transform>();
        private int m_index;
        private NavMeshAgent m_agent;

        
        
        private RaycastHit[] m_hits = new RaycastHit[3];
        
        private int index
        {
            get => m_index;
            set => m_index = value > m_patrolPoints.Count - 1 ? 0 : value < 0 ? m_patrolPoints.Count - 1 : value;
        }
        
        //Called through the Visual Script
        public void RunCommand()
        {
            m_command?.Invoke();
            m_commandFinished = true;
        }
        private void Start()
        {
            m_agent = GetComponent<NavMeshAgent>();
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

        public void PatrolStart()
        {

            if (m_patrolPoints.Count == 0)
            {
                var patrolPointsParent = transform.Find("PatrolPoints");

                if (patrolPointsParent == null)
                {
                    throw new Exception($"Cannot Find Patrol Points in Enemy: {name}");
                }

                for (int i = 0; i < patrolPointsParent.childCount; i++)
                {
                    if(patrolPointsParent.GetChild(i).gameObject.activeInHierarchy)
                        m_patrolPoints.Add(patrolPointsParent.GetChild(i));
                }

                patrolPointsParent.SetParent(null);
            }
        }

        public void PatrolUpdate()
        {
            if (m_agent.remainingDistance < 0.1f)
            {
                StartCoroutine(SetDestination());
            }
            
            Physics.Raycast(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward, out m_hits[0], 20f);
            Physics.Raycast(transform.position, transform.forward, out m_hits[1], 20f);
            Physics.Raycast(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward, out m_hits[2], 20f);
            
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(30f, transform.up) * transform.forward * 18f);
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(0f, transform.up) * transform.forward * 20f);
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(-30f, transform.up) * transform.forward * 18f);

        }

        IEnumerator SetDestination()
        {

            yield return new WaitForSeconds(m_defaultPatrolWaitTime);
            
            if (m_agent.remainingDistance < 0.1f)
                m_agent.SetDestination(m_patrolPoints[index++].position);
            //Debug.Log(index);
        }

        public bool PatrolToChaseTransition()
        {
           
            
            for (int i = 0; i <= 2; i++)
            {
                if (m_hits[i].collider != null && m_hits[i].collider.gameObject.CompareTag("Player"))
                {
                    return true;
                }
            }
            return false;
        }

        public bool PatrolToInfectTransition()
        {

            for (int i = 0; i <= 2; i++)
            {
                
                if (m_hits[i].collider != null &&
                    m_hidingSpotsToInfect.Any(t => t.gameObject.transform == m_hits[i].collider.transform))
                {
                    m_hidingSpotToInfect = m_hits[i].collider.GetComponent<HidingSpot>();                    
                    return m_hidingSpotToInfect.isActive;
                }
            }
            
            return false;
        }

        public bool InfectToPatrolTransition()
        {
            return !m_hidingSpotToInfect.isActive;
        }

        public void InfectUpdate()
        {
            m_agent.stoppingDistance = 4;
            m_agent.SetDestination(m_hidingSpotToInfect.gameObject.transform.position);
            
            if (m_agent.remainingDistance <= m_agent.stoppingDistance && !m_hidingSpotToInfect.infectStared)
            {
                
                m_hidingSpotToInfect.Infect();
            }
        }
    }
    
}