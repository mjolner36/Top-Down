using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryHolder
{
    public ItemsData InventoryData = new ItemsData();
    public List<ArtifactSO> AllArtifactSOs;
    public delegate void InventoryChanger();
    public event InventoryChanger ChangerEvent;
    public void Init()
    {
        Artifact.PickUpItem += AddItem;
        LoadInventory();
    }

    private void LoadInventory()
    {
        AllArtifactSOs = new List<ArtifactSO>(Resources.LoadAll<ArtifactSO>(""));
        var loadData = JSONData.JSONDeserialization<ItemsData>("Inventory.json");
        foreach (var item in from artifactSO in AllArtifactSOs from item in loadData.inventoryList where artifactSO.id == item.id select item)
        {
            InventoryData.inventoryList.Add(item);
        }
    }
    
    private void AddItem(string id, int amount)
    {
        var isExist = false;
        foreach (var item in InventoryData.inventoryList.Where(item => item.id == id))
        {
            item.amount += amount;
            isExist = true;
            break;
        }

        if (!isExist)
        {
            InventoryData.inventoryList.Add(new ItemData(id, amount));
        }
        ChangerEvent?.Invoke();
        SaveInventory();
    }

    public void DeleteItem(string id)
    {
        var isExist = false;
        foreach (var item in InventoryData.inventoryList.Where(item => item.id == id))
        {
            if (item.amount > 1)
            {
               item.amount--; 
            }
            else
            {
                InventoryData.inventoryList.Remove(item);
            }
            isExist = true;
            break;
        }
        ChangerEvent?.Invoke();
        SaveInventory();
    }
    
    private void SaveInventory()
    {
        JSONData.JsonSerialization(InventoryData,"Inventory.json");
    }
    
    public void OnDisable()
    {
        Artifact.PickUpItem -= AddItem;
    }
}
