using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [Serializable]
    public class SaveData
    {
        public List<Item> itemList = new List<Item>();
        public List<Item> boughts = new List<Item>();
    }

    [Serializable]
    public class SaveID
    {
        public int currentItemID = 0;
    }
    [Serializable]
    public class SaveCoin
    {
        public int coin = 1000;
    }
    public void saving(ShopManager shopManager, string urlShop)
    {
        try
        {
            Debug.Log("shopManager: " + shopManager);
            SaveData saveData = new SaveData();
            // Save Data
            saveData.itemList.Clear();
            saveData.boughts.Clear();
            // Do something
            for (int i = 0; i < shopManager.itemList.Count; i++)
            {
                saveData.itemList.Add(shopManager.itemList[i]);
            }
            for (int i = 0; i < shopManager.boughtList.Count; i++)
            {
                saveData.boughts.Add(shopManager.boughtList[i]);
            }
            //
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + urlShop, FileMode.OpenOrCreate);
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        print("saved data to " + Application.persistentDataPath + urlShop);
    }
    public void loading(ShopManager shopManager, string urlShop)
    {
        Debug.Log(Application.persistentDataPath + urlShop);
        if (File.Exists(Application.persistentDataPath + urlShop))
        {
            try
            {
                SaveData saveData = new SaveData();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.persistentDataPath + urlShop, FileMode.Open);
                saveData = (SaveData)bf.Deserialize(fs);
                fs.Close();
                // do somthing
                shopManager.boughtList.Clear();
                shopManager.itemList.Clear();
                for (int i = 0; i < saveData.boughts.Count; i++)
                {
                    shopManager.boughtList.Add(saveData.boughts[i]);
                }
                for (int i = 0; i < saveData.itemList.Count; i++)
                {
                    shopManager.itemList.Add(saveData.itemList[i]);
                }
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }

    public void savingCoin(CoinManager coinManager, string urlShop)
    {
        try
        {
            SaveCoin saveData = new SaveCoin();
            // Save Data
            // Do something
            saveData.coin = coinManager.getCoin();
            //
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + urlShop, FileMode.OpenOrCreate);
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        print("saved data to " + Application.persistentDataPath + urlShop);
    }

    public void loadingCoin(CoinManager coinManager, string urlShop)
    {
        Debug.Log(Application.persistentDataPath + urlShop);
        if (File.Exists(Application.persistentDataPath + urlShop))
        {
            try
            {
                SaveCoin saveData = new SaveCoin();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.persistentDataPath + urlShop, FileMode.Open);
                saveData = (SaveCoin)bf.Deserialize(fs);
                fs.Close();
                // do somthing
                coinManager.setCoin(saveData.coin);
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }
    public void savingID(ShopManager shopManager, string urlShop)
    {
        try
        {
            Debug.Log("shopManager: " + shopManager);
            SaveID saveData = new SaveID();
            // Save Data

            // Do something
            saveData.currentItemID = shopManager.currentItemID;
            //
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(Application.persistentDataPath + urlShop, FileMode.OpenOrCreate);
            bf.Serialize(fs, saveData);
            fs.Close();
        }
        catch (Exception e)
        {
            print(e);
        }
        print("saved data to " + Application.persistentDataPath + urlShop);
    }
    public void loadingID(ref int id, string urlShop)
    {
        Debug.Log(File.Exists(Application.persistentDataPath + urlShop));
        if (File.Exists(Application.persistentDataPath + urlShop))
        {
            try
            {
                SaveID saveData = new SaveID();
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.persistentDataPath + urlShop, FileMode.Open);
                saveData = (SaveID)bf.Deserialize(fs);
                fs.Close();
                // do somthing
                id = saveData.currentItemID;
            }
            catch (Exception e)
            {
                print(e);
            }
        }
    }
}
