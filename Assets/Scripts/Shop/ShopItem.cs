using UnityEngine;

public class ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int price;

    public virtual bool BuyItem()
    {
        // Buy item and deduct price from wherever currency is being stored
        return(false);
    }
}
