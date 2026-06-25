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
        Player.Instance.Inventory.Add(CreateInstance<Item_Even>());
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (number % 2 == 0)
        {
            return Mathf.Sqrt(Amount) * Multiplier;
        }
        return 0;
    }
}
