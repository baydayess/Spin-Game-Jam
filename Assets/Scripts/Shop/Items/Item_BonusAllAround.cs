using UnityEngine;

[CreateAssetMenu(fileName = "Item_BonusAllAround", menuName = "Scriptable Objects/Item_BonusAllAround")]
public class Item_BonusAllAround : Item
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

        Item_BonusAllAround newItem = CreateInstance<Item_BonusAllAround>();
        newItem.CreateWithValue(this);
        newItem.Multiplier = Multiplier;

        Player.Instance.Inventory.Add(newItem);
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        return Mathf.Sqrt(Amount) * (Multiplier - 1);
    }
}
