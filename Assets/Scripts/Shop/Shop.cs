using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Shop: Singleton<Shop>
{
    [SerializeField] private int numberItems = 6;
    [SerializeField] private GameObject itemsGrid;
    [SerializeField] private List<Item> items;
    [SerializeField] private List<Item> currentShopItems;
    [field:SerializeField] public bool IsShopOpen { get; private set; }

    private void Start()
    {
        items = Resources.LoadAll<Item>("Items").ToList();
    }

    public void OpenShop()
    {
        List<Item> itemsPicked = new List<Item>();

        for (int i = 0; i < numberItems; i++)
        {
            int randItemIndex = Random.Range(0, items.Count);

            if (itemsPicked.Contains(items[randItemIndex]))
            {
                i--;
                continue;
            }

            itemsPicked.Add(items[randItemIndex]);
        }

        DisplayItems(itemsPicked);
    }

    private void DisplayItems(List<Item> itemsPicked)
    {
        currentShopItems = itemsPicked;
        foreach (Item item in currentShopItems) 
        { 
            Debug.Log(item);
            
        }
        IsShopOpen = true;
        Debug.Log("--------------  SHOP IS OPEN  --------------");
    }

    public void CloseShop()
    {
        IsShopOpen = false;
        currentShopItems.Clear();
        Debug.Log("-------------- SHOP IS CLOSED --------------");
    }
}
