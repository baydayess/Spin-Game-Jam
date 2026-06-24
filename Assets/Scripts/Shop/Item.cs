using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [field:SerializeField] public Sprite Sprite { get; private set; } = null;
    [field:SerializeField] public Sprite SpriteOutline { get; private set; } = null;
    [field:SerializeField] public float Price { get; private set; } = 0;
    [field:SerializeField] public int ExtraBalls { get; private set; } = 0;
    [field: SerializeField] public float BlackBonusPercentage { get; private set; } = 0;
    [field: SerializeField] public float RedBonusPercentage { get; private set; } = 0;
    [field: SerializeField] public float GreenBonusPercentage { get; private set; } = 0;
}