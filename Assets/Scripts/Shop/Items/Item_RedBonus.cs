using UnityEngine;

[CreateAssetMenu(fileName = "Item_RedBonus", menuName = "Scriptable Objects/Item_RedBonus")]
public class Item_RedBonus : Item
{
    [SerializeField] private float Multiplier;

    override public void BuyItem()
    {
        foreach (Item item in Player.Instance.Inventory)
        {
            if (item.Name == Name)
            {
                item.Amount++;
                return;
            }
        }

        Item_RedBonus newItem = CreateInstance<Item_RedBonus>();
        newItem.CreateWithValue(this);
        newItem.Multiplier = Multiplier;

        Player.Instance.Inventory.Add(newItem);
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.red)
        {
            return Mathf.Sqrt(Amount) * (Multiplier - 1);
        }
        return 0;
    }
}
