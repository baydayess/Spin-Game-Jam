using UnityEngine;

[CreateAssetMenu(fileName = "Item_Second", menuName = "Scriptable Objects/Item_Second")]
public class Item_Second : Item
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

        Player.Instance.Inventory.Add(CreateInstance<Item_Second>());
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (number is > 12 and <= 24)
        {
            return Mathf.Sqrt(Amount) * Multiplier;
        }
        return 0;
    }
}
