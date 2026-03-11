using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkSelecter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI perkLabel;
    [SerializeField] private Image perkIcon;

    [Header("Perks")]
    [SerializeField] private PerkSO[] ALL_PERKS;
    [SerializeField] private PerkSO currentPerkSelected;
    [SerializeField] private PerkSO currentPerkHighlighted;
    private int perkIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    public void SwitchPerk(int direction)
    {
        perkIndex += direction;
        if(perkIndex > ALL_PERKS.Length)
            perkIndex = 0;
        else if(perkIndex < 0)
            perkIndex = ALL_PERKS.Length - 1;

        currentPerkHighlighted = ALL_PERKS[perkIndex];

        UpdatePerkSelectUI();
    }

    public void UpdatePerkSelectUI()
    {
        if(currentPerkHighlighted == null)
        {
            perkLabel.SetText("None");
        }
        else
        {
            perkLabel.SetText(currentPerkHighlighted.perkName);
        }
    }
}
