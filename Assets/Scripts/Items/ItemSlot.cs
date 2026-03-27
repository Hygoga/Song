[System.Serializable]
public class ItemSlot
{
    public ItemData item;
    public int count;

    public ItemSlot() { item = null; count = 0; }

    public void AddStack(int amount) { count += amount; }
    public bool CanItemFit(int amount) => count + amount <= item.maxStackSize;
}