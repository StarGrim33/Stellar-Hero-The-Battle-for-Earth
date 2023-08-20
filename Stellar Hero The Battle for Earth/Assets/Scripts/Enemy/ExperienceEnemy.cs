using UnityEngine;

public class ExperienceEnemy : MonoBehaviour
{
    [SerializeField] private int _experienceForEnemy;

    private EnemyHealth _enemyHealth;

    public int ExperienceForEnemy => _experienceForEnemy;
}
