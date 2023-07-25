using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class GridInventoryAssets : MonoBehaviour {


    public static GridInventoryAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public ItemSO[] itemSOArray;

    public ItemSO gun;
    public ItemSO pistol;
    public ItemSO sniper;
    public ItemSO apple;
    public ItemSO key1;
    public ItemSO flashlight;
    public ItemSO battery;

    public ItemSO GetItemSOFromName(string itemSOName) {
        foreach (ItemSO itemSO in itemSOArray) {
            if (itemSO.name == itemSOName) {
                return itemSO;
            }
        }
        return null;
    }


    public Sprite gridBackground;
    public Sprite gridBackground_2;
    public Sprite gridBackground_3;

    public Transform gridVisual;

}