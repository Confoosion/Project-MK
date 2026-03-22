using UnityEngine;

[CreateAssetMenu(fileName = "Character Set", menuName = "Shop/Character Set")]
public class CharacterSetSO : ShopItem
{
    [System.Serializable]
    public class CharacterUpgrade
    {
        public CharacterSO character;
        public Sprite characterIcon;
        public int upgradePrice;
    }

    public CharacterUpgrade[] upgrades = new CharacterUpgrade[3];
    // public int currentLevel = 1;

    public override bool BuyItem()
    {
        CharacterSetData data = ShopSaveSystem.GetCharacterData(name);

        if(data == null || data.currentLevel >= upgrades.Length)
        {
            Debug.Log("Max level");
            return(false);
        }

        CharacterUpgrade currentUpgrade = upgrades[data.currentLevel - 1];
        int upgradePrice = currentUpgrade.upgradePrice;

        Debug.Log("Upgrade price is: " + upgradePrice + " and you have: " + ShopManager.Singleton.GetCharacterCurrency());

        if(ShopManager.Singleton.GetCharacterCurrency() < upgradePrice)
        {
            Debug.Log("Too broke to buy");
            return(false);
        }

        ShopManager.Singleton.AddCharacterCurrency(-upgradePrice);
        ShopSaveSystem.UpdateCharacterLevel(name, data.currentLevel + 1);

        ShopManager.Singleton.SaveShop();

        Debug.Log(name + " upgraded to level " + (data.currentLevel));
        return(true);
    }

    public int GetCurrentLevel()
    {
        CharacterSetData data = ShopSaveSystem.GetCharacterData(name);
        return (data != null ? data.currentLevel : 1);
    }

    public bool IsUnlocked()
    {
        CharacterSetData data = ShopSaveSystem.GetCharacterData(name);
        return (data != null && data.isUnlocked);
    }

    public CharacterUpgrade GetCurrentUpgrade()
    {
        int level = GetCurrentLevel();
        return(upgrades[Mathf.Clamp(level - 1, 0, upgrades.Length - 1)]);
    }

    public CharacterUpgrade GetNextUpgrade()
    {
        int level = GetCurrentLevel();
        if(level >= upgrades.Length)
            return null;
        return(upgrades[level]);
    }

    public void ChangeDisplay(int lvl)
    {
        Debug.Log(this + " " + lvl);
        price = upgrades[lvl-1].upgradePrice;
        itemIcon = upgrades[lvl-1].characterIcon;
    }
}