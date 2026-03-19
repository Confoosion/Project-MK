using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GachaAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform itemContainer;
    [SerializeField] private GameObject itemSlotPrefab;

    [Header("Animation Settings")]
    [SerializeField] private int totalSlots = 30;
    [SerializeField] private int winnerSlotIndex = 22;
    [SerializeField] private float slotHeight = 100f;
    [SerializeField] private float slotOffset;

    [Header("Timing Settings")]
    [SerializeField] private Vector2 spinDuration = new Vector2(2f, 3f);

    private List<GameObject> perkSlots = new List<GameObject>();
    private PerkSO winnerPerk;
    private int winnerSlotObjectIndex = -1;
    private bool isAnimating = false;

    public void StartSpin(PerkSO winner, PerkSO[] availablePerks)
    {
        if(isAnimating)
            return;

        winnerPerk = winner;
        StartCoroutine(SpinAnimation(availablePerks));

    }

    private IEnumerator SpinAnimation(PerkSO[] availablePerks)
    {
        isAnimating = true;

        SetupPerkStrip(availablePerks);

        float startY = 0;
        itemContainer.anchoredPosition = new Vector2(0, -startY);

        float targetY = slotHeight * winnerSlotIndex + slotOffset + Random.Range(-slotHeight * 0.4f, slotHeight * 0.4f);

        yield return StartCoroutine(AnimateScroll(itemContainer.anchoredPosition.y,
                                                  targetY,
                                                  Random.Range(spinDuration.x, spinDuration.y)));
        
        isAnimating = false;
    }

    private void SetupPerkStrip(PerkSO[] availablePerks)
    {
        foreach(var slot in perkSlots)
        {
            Destroy(slot);
        }
        perkSlots.Clear();

        for(int i = 0; i < totalSlots; i++)
        {
            GameObject slotObj = Instantiate(itemSlotPrefab, itemContainer);

            PerkSO perk;;
            if(i == winnerSlotIndex)
            {
                perk = winnerPerk;
                winnerSlotObjectIndex = i;
            }
            else
            {
                perk = availablePerks[Random.Range(0, availablePerks.Length)];
            }

            SetupSlotUI(slotObj, perk);

            RectTransform slotRect = slotObj.GetComponent<RectTransform>();
            slotRect.anchoredPosition = new Vector2(0, 0);

            perkSlots.Add(slotObj);
        }
    }

    private void SetupSlotUI(GameObject slotObj, PerkSO perk)
    {
        Image icon = slotObj.GetComponent<Image>();
        
        if(icon != null && perk.icon != null)
        {
            icon.sprite = perk.icon;
        }
    }

    private IEnumerator AnimateScroll(float startY, float endY, float duration)
    {
        float timer = 0f;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            
            float t = Mathf.Clamp01(timer / duration);
            float easedT = 1f - Mathf.Pow(1f - t, 3f);
            float currentY = Mathf.Lerp(startY, endY, easedT);
            itemContainer.anchoredPosition = new Vector2(0, currentY);

            yield return null;
        }

        itemContainer.anchoredPosition = new Vector2(0, endY);
    }

    public bool IsAnimating()
    {
        return(isAnimating);
    }
}
