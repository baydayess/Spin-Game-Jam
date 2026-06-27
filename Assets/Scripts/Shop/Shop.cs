using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop: Singleton<Shop>
{
    [SerializeField] private int numberItems = 6;
    [SerializeField] private GameObject itemsGrid;
    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private List<Item> items;
    [SerializeField] private List<Item> currentShopItems;
    [SerializeField] private List<Button> currentShopItemButtons;


    [SerializeField] private AudioClip audio;

    [SerializeField] private List<AudioSource> audioPlayers;
    [field:SerializeField] public bool IsShopOpen { get; private set; }

    private void Start()
    {
        items = Resources.LoadAll<Item>("Items").ToList();
        currentShopItems = new List<Item>();
        currentShopItemButtons = new List<Button>();
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
        int itemID = 0;
        foreach (Item item in currentShopItems) 
        {
            item.SetPrice(item.Price + (100 * GameManager.Instance.Round * Mathf.Pow(Random.Range(1f, 2f), GameManager.Instance.Round)));

            GameObject itemObj = Instantiate(itemUIPrefab, itemsGrid.transform);
            itemObj.name = item.name;
            
            Image itemSprite = itemObj.GetComponent<Image>();
            itemSprite.sprite = item.Sprite;
            
            Button itemButton = itemObj.GetComponent<Button>();
            SpriteState spriteState = itemButton.spriteState;
            spriteState.highlightedSprite = item.SpriteOutline;
            spriteState.selectedSprite = item.SpriteOutline;
            spriteState.pressedSprite = item.SpriteOutline;
            itemButton.spriteState = spriteState;
            int ID = itemID;
            itemButton.onClick.AddListener(() => { BoughtItem(ID); });
            currentShopItemButtons.Add(itemButton);
            
            TextMeshProUGUI itemPriceText = itemObj.GetComponentInChildren<TextMeshProUGUI>();
            int firstPartPrice = Mathf.RoundToInt(item.Price * 100) / 100;
            int secondPartPrice = Mathf.RoundToInt(item.Price * 100) % 100;
            itemPriceText.text = $"{firstPartPrice}.{secondPartPrice}$";

            Debug.Log(item);
            itemID++;
        }

        IsShopOpen = true;
        Debug.Log("--------------  SHOP IS OPEN  --------------");
    }

    public void BoughtItem(int itemID)
    {
        //DO SOMETHING WITH ITEM
        if (Player.Instance.Check_Money() < currentShopItems[itemID].Price) { return; }

        Player.Instance.Remove_Money(currentShopItems[itemID].Price);
        currentShopItems[itemID].BuyItem();
        currentShopItemButtons[itemID].enabled = false;
        currentShopItemButtons[itemID].gameObject.SetActive(false);

        AudioSource audioPlayer = gameObject.AddComponent<AudioSource>();
        audioPlayer.loop = false;
        audioPlayer.pitch = Random.Range(0.9f, 1.1f);
        audioPlayer.PlayOneShot(audio);
        audioPlayers.Add(audioPlayer);
    }
    private void Update()
    {
        if (audioPlayers.Count > 0)
        {
            for (int i = 0; i < audioPlayers.Count; i++)
            {
                if (!audioPlayers[i].isPlaying)
                {
                    Destroy(audioPlayers[i]);
                    audioPlayers.RemoveAt(i);
                }
            }
        }
    }

    public void CloseShop()
    {
        IsShopOpen = false;
        
        foreach (Button button in currentShopItemButtons) 
        {
            Destroy(button.gameObject);        
        }
        currentShopItemButtons.Clear();

        currentShopItems.Clear();
        Debug.Log("-------------- SHOP IS CLOSED --------------");
    }

    private void OnDestroy()
    {
        foreach (Item item in items)
        {
            item.SetPrice(item.NormalPrice);
        }
    }
}
