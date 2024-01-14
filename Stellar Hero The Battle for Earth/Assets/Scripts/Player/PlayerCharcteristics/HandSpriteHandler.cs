using UnityEngine;

namespace Player
{
    public class HandSpriteHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _hands;

        public void Enable()
        {
            foreach (var hand in _hands)
                hand.enabled = true;
        }

        public void Disable()
        {
            foreach (var hand in _hands)
                hand.enabled = false;
        }
    }
}