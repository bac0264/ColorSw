using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemShop : ShopManager
{
    public static ItemShop instance;
    private void Awake()
    {
        if (instance == null) instance = this;

        urlShop = name;
        count = itemList.Count;

        //Load data
        loading();
        // setup ui
        if (itemList != null)
        {
            itemList[0].bought = true;
            boughtList.Add(itemList[0]);
        }
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefItem, container, false);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = imageItemList[i];

            // add into list to manage
            itemObjectList.Add(obj);
            snap.btnn.Add(obj.GetComponent<Button>());


            // xet da mua
            if (itemList[i].bought)
            {
                itemObjectList[i].transform.GetChild(0).GetComponent<Image>().material = null;
                itemObjectList[i].transform.GetChild(0).GetComponent<Image>().color
                     = new Color(1, 1, 1, 1);
                itemObjectList[i].transform.GetChild(0).GetComponent<Image>().sprite = imageItemList[i];
                // snake chua su dung
                if (itemList[i].itemID != currentItemID)
                {
                }
                // snake da mua va su dung
                else
                {

                }

            }
            else
            {

            }
        }
        coinManager.UpdateCoin();
        UpdateUI();
        snap._setupStart(currentItemID);
    }

    // update UI
    override
    public void UpdateUI()
    {
        for (int i = 0; i < boughtList.Count; i++)
        {
            for (int j = 0; j < itemList.Count; j++)
            {

                if (itemList[j].bought == boughtList[i].bought)
                {
                    itemObjectList[j].transform.GetChild(0).GetComponent<Image>().material = null;
                    itemObjectList[j].transform.GetChild(0).GetComponent<Image>().color
                         = new Color(1, 1, 1, 1);
                    // snake item da mua nhung k su dung
                    if (itemList[j].itemID != currentItemID)
                    {
                    }
                    // snake da mua va su dung
                    else
                    {
                    }
                }
            }
        }
    }
    override
    public void loading()
    {
        SaveLoad.instance.loading(this, urlShop);
        coinManager.loadingCoin();
    }
    override
    public void saving()
    {
        SaveLoad.instance.saving(this, urlShop);
        coinManager.savingCoin();
    }
}

