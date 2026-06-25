using UnityEngine;

[CreateAssetMenu(fileName = "Item_First", menuName = "Scriptable Objects/Item_First")]
public class Item_First : Item
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

        Player.Instance.Inventory.Add(CreateInstance<Item_First>());
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (number <= 12 && number != 0)
        {
            return Mathf.Sqrt(Amount) * Multiplier;
        }
        return 0;
    }
}
