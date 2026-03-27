using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable;
    public int maxStackSize = 64;
    
    public enum ItemType { Product, Tool, Food, Quest }
    public ItemType itemType;

    [Header("Stats")]
    public int sellPrice;
    public int staminaRestore;
}