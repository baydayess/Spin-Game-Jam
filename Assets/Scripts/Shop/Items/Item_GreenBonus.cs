using UnityEngine;

[CreateAssetMenu(fileName = "Item_GreenBonus", menuName = "Scriptable Objects/Item_GreenBonus")]
public class Item_GreenBonus : Item
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

        Item_GreenBonus newItem = CreateInstance<Item_GreenBonus>();
        newItem = this;
        Player.Instance.Inventory.Add(newItem);
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.green)
        {
            return Mathf.Sqrt(Amount) * (Multiplier - 1);
        }
        return 0;
    }
}
