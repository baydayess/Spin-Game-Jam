using UnityEngine;

[CreateAssetMenu(fileName = "Item_BlackBonus", menuName = "Scriptable Objects/Item_BlackBonus")]
public class Item_BlackBonus : Item
{
    [SerializeField] private float Multiplier;

    override public void BuyItem()
    {
        foreach(Item item in Player.Instance.Inventory) 
        {
            if (item.Name == Name)
            {
                item.Amount++;
                return;
            }
        }

        Item_BlackBonus newItem = CreateInstance<Item_BlackBonus>();
        newItem = this;
        Player.Instance.Inventory.Add(newItem);
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.black)
        {
            return Mathf.Sqrt(Amount) * (Multiplier - 1);
        }
        return 0;
    }
}