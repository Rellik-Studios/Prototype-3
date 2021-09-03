using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Himanshu
{
    public class HidingSpot : MonoBehaviour, IInteract
    {
        public void Execute(PlayerInteract _player)
        {
            _player.Hide();
        }
    }
}