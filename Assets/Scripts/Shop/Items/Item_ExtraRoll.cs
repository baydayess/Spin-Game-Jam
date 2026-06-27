using UnityEngine;

[CreateAssetMenu(fileName = "Item_ExtraRoll", menuName = "Scriptable Objects/Item_ExtraRoll")]

public class Item_ExtraRoll : Item
{
    override public void BuyItem()
    {
        Player.Instance.MaxRolls++;
    }
}
