using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; } = null;
    [field: SerializeField] public Sprite SpriteOutline { get; private set; } = null;
    [field: SerializeField] public float Price { get; private set; } = 0;
    [field: SerializeField] public int Amount { get; private set; } = 1;

    virtual public void BuyItem()
    {

    }

    virtual public float GetEffect(int number, ESlotColor color)
    {
        return 0;
    }
}