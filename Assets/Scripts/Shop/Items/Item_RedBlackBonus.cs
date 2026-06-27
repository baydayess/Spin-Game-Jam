using UnityEngine;

[CreateAssetMenu(fileName = "Item_RedBlackBonus", menuName = "Scriptable Objects/Item_RedBlackBonus")]
public class Item_RedBlackBonus : Item
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

        Item_RedBlackBonus newItem = CreateInstance<Item_RedBlackBonus>();
        newItem.CreateWithValue(this);
        newItem.Multiplier = Multiplier;

        Player.Instance.Inventory.Add(newItem);
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.red || color == ESlotColor.black)
        {
            return Mathf.Sqrt(Amount) * (Multiplier - 1);
        }
        return 0;
    }
}
