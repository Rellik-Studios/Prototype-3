using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Himanshu
{
    public class Commands : MonoBehaviour
    {
        [SerializeField] private HidingSpot m_room1Table;
        [SerializeField] private HidingSpot m_room2Table;
        public void Room1Command(EnemyController _enemy)
        {
            StartCoroutine(eRoom1Command(_enemy));
        }

        private IEnumerator eRoom1Command(EnemyController _enemy)
        {
            var agent = _enemy.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 2f;
            agent.SetDestination(m_room1Table.transform.position);
            yield return new WaitForEndOfFrame();
            FindObjectOfType<CharacterController>().enabled = false;
            while (agent.remainingDistance > agent.stoppingDistance)
                yield return null;
            
            Debug.Log(agent.remainingDistance);
            m_room1Table.Infect();
            yield return new WaitForSeconds(3.5f);
            _enemy.m_commandFinished = true;
            FindObjectOfType<CharacterController>().enabled = true;
        }
        
        
        public void Room2Command(EnemyController _enemy)
        {
            StartCoroutine(eRoom2Command(_enemy));
        }

        private IEnumerator eRoom2Command(EnemyController _enemy)
        {
            var agent = _enemy.GetComponent<NavMeshAgent>();
            agent.stoppingDistance = 2f;
            agent.SetDestination(m_room2Table.transform.position);
            FindObjectOfType<CharacterController>().enabled = false;
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => agent.remainingDistance >= agent.stoppingDistance);
            m_room2Table.Infect();
            yield return new WaitForSeconds(3.5f);
            _enemy.m_commandFinished = true;
            yield return new WaitForSeconds(3.5f);
            FindObjectOfType<CharacterController>().enabled = true;
        }
    }
}
