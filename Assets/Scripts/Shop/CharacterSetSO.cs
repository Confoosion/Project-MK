using UnityEngine;

[CreateAssetMenu(fileName = "Character Set", menuName = "Shop/Character Set")]
public class CharacterSetSO : ShopItem
{
    [System.Serializable]
    public class CharacterUpgrade
    {
        public CharacterSO character;
        public Sprite upgradeIcon;
        public int upgradePrice;
    }

    public CharacterUpgrade[] upgrades = new CharacterUpgrade[3];
    public int currentLevel = 1;

    public override void BuyItem()
    {
        if (currentLevel >= upgrades.Length) return;

        CharacterUpgrade next = upgrades[currentLevel-1];
        price = next.upgradePrice;

        base.BuyItem();

        // e.g. GameManager.Instance.UnlockCharacter(next.character);
        currentLevel++;
    }

    public void ChangeDisplay(int lvl)
    {
        price = upgrades[lvl-1].upgradePrice;
        itemIcon = upgrades[lvl-1].upgradeIcon;
    }
}