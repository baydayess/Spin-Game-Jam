using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; } = null;
    [field: SerializeField] public Sprite SpriteOutline { get; private set; } = null;
    [field: SerializeField] public string Name { get; private set; } = "Item";
    [field: SerializeField] public float Price { get; private set; } = 0;
    [field: SerializeField] public int Amount { get; set; } = 1;

    public void CreateWithValue(Item item)
    {
        Sprite = item.Sprite;
        SpriteOutline = item.SpriteOutline;
        Name = item.Name;
        Price = item.Price;
    }

    public void SetPrice(float newPrice)
    {
        Price = newPrice;
    }

    virtual public void BuyItem()
    {

    }

    virtual public float GetEffect(int number, ESlotColor color)
    {
        return 0;
    }
}