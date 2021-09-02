using UnityEngine;

namespace Himanshu
{
    public class HidingSpot : MonoBehaviour, IInteract
    {
        public void Execute(PlayerInteract _player)
        {
            Debug.Log("Will Hide");
        }
    }
}