using UnityEngine;

[CreateAssetMenu(fileName = "Item_BlackBonus", menuName = "Scriptable Objects/Item_BlackBonus")]
public class Item_BlackBonus : Item
{
    [SerializeField] private float Multiplier;

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.black)
        {
            return Mathf.Sqrt(Amount) * Multiplier;
        }
        return 0;
    }
}