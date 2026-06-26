using UnityEngine;

[CreateAssetMenu(fileName = "Item_Even", menuName = "Scriptable Objects/Item_Even")]
public class Item_Even : Item
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
        Item_Even newItem = CreateInstance<Item_Even>();
        newItem = this;
        Player.Instance.Inventory.Add(newItem);
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (number % 2 == 0)
        {
            return Mathf.Sqrt(Amount) * (Multiplier - 1);
        }
        return 0;
    }
}
