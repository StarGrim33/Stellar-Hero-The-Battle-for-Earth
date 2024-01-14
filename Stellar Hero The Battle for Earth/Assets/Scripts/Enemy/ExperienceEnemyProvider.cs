using UnityEngine;

namespace Enemy
{
    public class ExperienceEnemyProvider : MonoBehaviour
    {
        [SerializeField] private int _experienceForEnemy;

        public int ExperienceForEnemy => _experienceForEnemy;
    }
}
