using UnityEngine;
using Utils;
using Weapon;

namespace Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private BaseWeapon _weapon;

        private void Start()
        {
            transform.rotation = Quaternion.identity;
        }

        private void Update()
        {
            if (StateManager.Instance.CurrentGameState == GameStates.Paused)
            {
                return;
            }

            _weapon.PerformShot();
        }
    }
}