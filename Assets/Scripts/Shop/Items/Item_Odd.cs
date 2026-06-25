using UnityEngine;

[CreateAssetMenu(fileName = "Item_Odd", menuName = "Scriptable Objects/Item_Odd")]
public class Item_Odd : Item
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
        Player.Instance.Inventory.Add(CreateInstance<Item_Odd>());
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (number % 2 == 0)
        {
            return 0;
        }
        return Mathf.Sqrt(Amount) * Multiplier;
    }
}
