using System.Collections.Generic;
using UnityEngine;

public class BuffWidget : MonoBehaviour
{
    [SerializeField] private int _buffCount = 3;
    [SerializeField] private BuffTemaplate _temaplate;
    [SerializeField] private List<CharacterUpgrade> _characterUpgradeList;
    [SerializeField] private List<BulletSpawner> _bulletSpawnerList;
    [SerializeField] private BulletParams _params;

    private BulletSpawner _targetBulletSpawner;

    private void OnEnable()
    {
        _targetBulletSpawner = _bulletSpawnerList[0];

        ShuffleList(_characterUpgradeList);

        for (int i =0; i < _buffCount; i++)
        {
            var tamplate = Instantiate(_temaplate);
            tamplate.transform.SetParent(transform);
            tamplate.SetUpgrade(_characterUpgradeList[i]);
        }
    }

    public void SetSpawner(int number)
    {
        _targetBulletSpawner = _bulletSpawnerList[number];
    }

    public void UpgradeParams(int damageModify, float attackSpeedModify, float bulletSpeedModify)
    {
        _params.Upgrade(damageModify, attackSpeedModify, bulletSpeedModify);
    }

    public void UpgradeSpawner(int count, int typePower)
    {
        _targetBulletSpawner.Upgrade(count, typePower);
    }

    public void SetBulletType(BulletType type)
    {
        _targetBulletSpawner.SetType(type);
    }

    public void ChangeUpgradeList(CharacterUpgrade upgrade)
    {
        if(upgrade.NextUpgrades.Count > 0)
            foreach (CharacterUpgrade nextUpgrade in upgrade.NextUpgrades)
                _characterUpgradeList.Add(nextUpgrade);

        _characterUpgradeList.Remove(upgrade);
    }

    public void HideWidget()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = 0; i < n; i++)
        {
            int randomIndex = Random.Range(i, n);

            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
