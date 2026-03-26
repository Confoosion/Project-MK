using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterShopUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject lockedOverlay;

    [SerializeField] private AudioClip buySuccessful_SFX;
    [SerializeField] private AudioClip buyFail_SFX;

    private CharacterSetSO characterSet;

    public void Initialize(CharacterSetSO _set)
    {
        characterSet = _set;
        UpdateDisplay();

        // upgradeButton.onClick.AddListener(OnUpgradeClicked);
        // CharacterSetSO.CharacterUpgrade currChar = _set.upgrades[_set.currentLevel-1];
        
        // nameText.SetText(_set.itemName);
        // UpdateUI(currChar.characterIcon, _set.currentLevel, currChar.upgradePrice);
    }

    private void UpdateDisplay()
    {
        if(characterSet == null)
            return;
        
        CharacterSetData data = ShopSaveSystem.GetCharacterData(characterSet.name);

        if(data == null)
            return;

        if(data.isUnlocked)
        {
            lockedOverlay.SetActive(false);

            levelText.SetText("Level " + data.currentLevel.ToString());

            var currentUpgrade = characterSet.GetCurrentUpgrade();
            iconImage.sprite = currentUpgrade.characterIcon;
            nameText.SetText(characterSet.itemName);

            if(data.currentLevel >= characterSet.upgrades.Length)
            {
                priceText.SetText("MAX LEVEL");
                upgradeButton.interactable = false;
                upgradeButton.gameObject.SetActive(false);
            }
            else
            {
                var nextUpgrade = characterSet.GetNextUpgrade();
                if(nextUpgrade != null)
                {
                    priceText.SetText(currentUpgrade.upgradePrice.ToString() + "c");
                    upgradeButton.interactable = true;
                }
            }
        }
        else
        {
            lockedOverlay.SetActive(true);
            nameText.SetText("???");
            levelText.SetText("LOCKED");
            priceText.SetText("???");
            upgradeButton.interactable = false;
        }
    }

    public void OnUpgradeClicked()
    {
        if(characterSet.BuyItem())
            SoundManager.Singleton.PlayUIAudio(buySuccessful_SFX);
        else
            SoundManager.Singleton.PlayUIAudio(buyFail_SFX);
            
        UpdateDisplay();
    }

    public void RefreshDisplay()
    {
        UpdateDisplay();
    }

    // public void UpdateUI(Sprite _icon, int _level, int _price)
    // {
    //     iconImage.sprite = _icon;
    //     levelText.SetText("Level " + _level.ToString());
    //     priceText.SetText("(" + _price.ToString() + " Currency)");
    // }

    // public void ButtonPressed()
    // {
    //     characterSet.BuyItem();

    //     CharacterSetSO.CharacterUpgrade currChar = characterSet.upgrades[characterSet.currentLevel - 1];
    //     UpdateUI(currChar.characterIcon, characterSet.currentLevel, currChar.upgradePrice);
    // }
}