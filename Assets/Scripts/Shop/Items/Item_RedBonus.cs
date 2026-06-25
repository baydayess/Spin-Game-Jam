using UnityEngine;

[CreateAssetMenu(fileName = "Item_RedBonus", menuName = "Scriptable Objects/Item_RedBonus")]
public class Item_RedBonus : Item
{
    [SerializeField] private float Multiplier;

    override public float GetEffect(int number, ESlotColor color)
    {
        if (color == ESlotColor.red)
        {
            return Mathf.Sqrt(Amount) * Multiplier;
        }
        return 0;
    }
}
