using UnityEngine;

[CreateAssetMenu(fileName = "Item_GreenBonus", menuName = "Scriptable Objects/Item_GreenBonus")]
public class Item_GreenBonus : Item
{
    [SerializeField] private float Multiplier;

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.green)
        {
            return Mathf.Sqrt(Amount) * Multiplier;
        }
        return 0;
    }
}
