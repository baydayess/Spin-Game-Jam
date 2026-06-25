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

        Player.Instance.Inventory.Add(CreateInstance<Item_BlackBonus>());
    }

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.black)
        {
            return Mathf.Sqrt(Amount) * Multiplier;
        }
        return 0;
    }
}