using UnityEngine;

namespace Himanshu
{
    public class CollectableObject : MonoBehaviour, IInteract
    {
        public void Execute(PlayerInteract _player)
        {
            Debug.Log("Collect");
            
            Destroy(this.gameObject);
        }
    }
}