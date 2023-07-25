using System;
using Inventory.Model;
using UnityEngine;

public class Item : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    [field: SerializeField]
    public ItemSO InventoryItem { get; private set; }
    [field: SerializeField]
    public ItemData itemData;
    public enum ItemType { food, weapon, item }
    [field: SerializeField]
    public ItemType itemType;
    [field: SerializeField]
    public int Quantity { get; set; } = 1;
    [SerializeField]
    private AudioClip pickupClip;
    private float duration = 0.3f;
    public bool collected = false;
    public bool useOtherAction = false;

    private void Start()
    {
        itemData = new ItemData(this.gameObject.transform.position, this.gameObject.transform.rotation, this.collected, this.useOtherAction);
    }
    public void PickUpItem()
    {
        collected = true;

        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(pickupClip);
        SetParent();
        gameObject.SetActive(false);
        itemData.ChangeData(this.gameObject.transform.position, this.gameObject.transform.rotation, this.collected, this.useOtherAction);
    }

    private void SetParent()
    {
        gameObject.transform.position = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSwitch>().gameObject.transform, false);
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void DropItem(InventoryItem inventoryItem)
    {
        collected = false;
        
        UnSetParent();
        gameObject.SetActive(true);
        itemData.ChangeData(this.gameObject.transform.position, this.gameObject.transform.rotation, this.collected, this.useOtherAction);
    }

    private void UnSetParent()
    {
        gameObject.transform.SetParent(null);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    // private IEnumerator AnimateItemPickup()
    // {
    //     // audioSource.PlayOneShot(audioSource.clip);
    //     Vector3 startScale = transform.localScale;
    //     Vector3 endScale = GameObject.FindGameObjectWithTag("Player").transform.position;
    //     float currentTime = 0;
    //     while(currentTime < duration)
    //     {
    //         currentTime += Time.deltaTime;
    //         transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
    //         yield return null;
    //     }
    //     GetComponent<Collider>().enabled = true;
    //     // Destroy(gameObject);
    // }

    public void LoadData(GameData data)
    {
        data.itemData.TryGetValue(id, out itemData);
        if (itemData != null)
        {
            this.gameObject.transform.position = itemData.itemPosition;
            this.gameObject.transform.rotation= itemData.itemRotation;
            this.useOtherAction = itemData.useOtherAction;
            if(itemData.collected)
            {
                SetParent();
                gameObject.SetActive(false);
            }
            else
            {
                UnSetParent();
            }
        }
    }

    public void SaveData(GameData data)
    {
        if (data.itemData.ContainsKey(id))
        {
            data.itemData.Remove(id);
        }
        data.itemData.Add(id, itemData);
    }
}

[Serializable]
public class ItemData
{
    public Vector3 itemPosition;
    public Quaternion itemRotation;
    public bool collected;
    public bool useOtherAction;

    public ItemData(Vector3 itemPosition, Quaternion itemRotation, bool collected, bool useOtherAction)
    {
        this.itemPosition = itemPosition;
        this.itemRotation = itemRotation;
        this.collected = collected;
        this.useOtherAction = useOtherAction;
    }
    public void ChangeData(Vector3 itemPosition, Quaternion itemRotation, bool collected, bool useOtherAction)
    {
        this.itemPosition = itemPosition;
        this.itemRotation = itemRotation;
        this.collected = collected;
        this.useOtherAction = useOtherAction;
    }
}
