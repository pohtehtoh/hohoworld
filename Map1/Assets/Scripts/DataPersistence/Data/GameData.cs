using Inventory;
using Inventory.Model;
using UnityEngine;
using static Inventory.InventoryController;

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    [Header("Player")]
    public int playerDeathCount;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public float playerHealth;

    [Header("Enemy")]
    public int enemyDeathCount;
    // public SerializableDictionary<string,  SnakeData> snakeData;

    [Header("Weapon")]
    public ItemSO leftWeapon;
    public ItemSO rightWeapon;
    public ItemSO inHandWeapon;

    [Header("Gun Data")]
    public int gunData1Ammo;
    public int gunData2Ammo;
    public int gunData3Ammo;

    [Header("Items")]
    public SerializableDictionary<string,  ItemData> itemData;

    [Header("Inventory")]
    public ListAddItem listAddItem;
    public int inventoryWeight;

    [Header("Lock")]
    public Vector3 lockPosition;
    public Vector3 lockRotation;

    public bool snakeToggle;

    public GameData(InventoryController inventory)
    {
        playerDeathCount = 0;
        playerPosition = new Vector3(-3,6,1);
        playerHealth = 1000f;

        enemyDeathCount = 0;
        //snakeData = new SerializableDictionary<string, SnakeData>();

        leftWeapon = null;
        rightWeapon = null;
        inHandWeapon = null;

        gunData1Ammo = 30;
        gunData2Ammo = 7;
        gunData3Ammo = 5;

        itemData = new SerializableDictionary<string, ItemData>();
        
        DataPersistenceManager.instance.StartCoroutine("InitializeInventory");

        lockPosition = new Vector3((float)-9.13399982, (float)5.5710001, (float)43.8308754);
        lockRotation = new Vector3((float)324.465576, (float)88.3139496, (float)0.980110586);

        snakeToggle = true;
    }

    public int GetPercentageComplete()
    {
        int totalCollected = 0;
        int totalDead = 0;
        // foreach(SnakeData data in snakeData.Values)
        // {
        //     if (data.dead)
        //     {
        //         totalDead++;
        //     }
        // }
        foreach (ItemData data in itemData.Values)
        {
            if (data.collected)
            {
                totalCollected++;
            }
        }

        int percentageCompleted = -1;
        int itemPercentageCompleted = -1;
        int snakePercentageCompleted = -1;

        if (itemData.Count != 0)
        {
            itemPercentageCompleted = (totalCollected * 100 / itemData.Count);
        }

        // if (snakeData.Count != 0)
        // {
        //     snakePercentageCompleted = (totalDead * 100 / snakeData.Count);
        // }

        percentageCompleted = (itemPercentageCompleted + snakePercentageCompleted) / 2;

        return percentageCompleted;
    }
}
