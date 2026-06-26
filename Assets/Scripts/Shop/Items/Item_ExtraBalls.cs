using UnityEngine;

[CreateAssetMenu(fileName = "Item_ExtraBalls", menuName = "Scriptable Objects/Item_ExtraBalls")]

public class Item_ExtraBalls : Item
{
    override public void BuyItem()
    {
        Player.Instance.ballAmount++;
    }
}
