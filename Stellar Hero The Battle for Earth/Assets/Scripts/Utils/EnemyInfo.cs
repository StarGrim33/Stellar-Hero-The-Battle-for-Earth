using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class EnemyInfo : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _amount;
        [SerializeField] private List<GameObject> _list;

        public GameObject Prefab => _prefab;

        public int Amount => _amount;

        public List<GameObject> List => _list;
    }
}