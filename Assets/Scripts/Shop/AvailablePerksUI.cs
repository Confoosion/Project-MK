using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AvailablePerksUI : MonoBehaviour
{
    [SerializeField] private Transform perksList_Parent;
    [SerializeField] private GameObject perkPrefab;
    [SerializeField] private List<PerkSO> availablePerks = new List<PerkSO>();

    public void SetAvailablePerks(PerkSO[] perksList)
    {
        availablePerks.Clear();

        foreach (PerkSO perk in perksList)
        {
            availablePerks.Add(perk);
        }

        SetupUI();
    }

    private void SetupUI()
    {
        ClearParentChildren();

        foreach(PerkSO perk in availablePerks)
        {
            GameObject perkIcon = Instantiate(perkPrefab, perksList_Parent);
            perkIcon.GetComponent<Image>().sprite = perk.icon;
        }
    }

    private void ClearParentChildren()
    {
        foreach(Transform child in perksList_Parent)
        {
            Destroy(child.gameObject);    
        }
    }
}
