using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShopManager : MonoBehaviour
{
    public CoinManager coinManager;
    public Snap snap;

    public List<GameObject> itemObjectList = new List<GameObject>();
    public List<Item> itemList = new List<Item>();
    public List<Item> boughtList = new List<Item>();
    public List<Sprite> imageItemList = new List<Sprite>();

    public string urlShop;
    public int count;
    public int currentItemID = 0;
    public Transform container;
    public GameObject prefItem;

    // update UI
    public virtual void UpdateUI()
    {
    }
    public virtual void _buyItem(int i)
    {
        int requestCoin = itemList[i].itemCoin;
        if (coinManager.requestCoin(requestCoin))
        {
            itemList[i].bought = true;
           // PlayerPrefs.SetInt("currentItemID", itemList[i].itemID);
            coinManager.reduceCoin(requestCoin);
            boughtList.Add(itemList[i]);
            currentItemID = i;
            UpdateUI();
            //Destroy(Instantiate(effectBuyingItem, shopManager.itemObjectList[i].transform));
        }
        else
        {
        }
    }
    public virtual void _updateItem(int itemID)
    {
        //PlayerPrefs.SetInt("currentItemID", itemID);
        currentItemID = itemID;
        UpdateUI();
        Debug.Log("curret update: " + currentItemID);
    }
    public virtual void saving(){}
    
    public virtual void loading(){}
}

