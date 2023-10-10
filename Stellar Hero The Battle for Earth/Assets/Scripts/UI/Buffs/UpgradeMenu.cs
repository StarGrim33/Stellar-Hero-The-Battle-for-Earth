using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private BuffWidget _buffWidget;

    public void ShowUpgradePanel()
    {
        _buffWidget.gameObject.SetActive(true);
    }
}
