using UnityEngine;

[CreateAssetMenu(menuName = "Source/Units/BuffView", fileName = "BuffView", order = 0)]

public class BuffView : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    public Sprite Icon => _icon;
    public string Description => _description;
}
