using UnityEngine;

[CreateAssetMenu(fileName = "Item_Third", menuName = "Scriptable Objects/Item_Third")]
public class Item_Third : Item
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

        Item_Third newItem = CreateInstance<Item_Third>();
        newItem = this;
        Player.Instance.Inventory.Add(newItem);
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (number is > 24 and <= 36)
        {
            return Mathf.Sqrt(Amount) * (Multiplier - 1);
        }
        return 0;
    }
}