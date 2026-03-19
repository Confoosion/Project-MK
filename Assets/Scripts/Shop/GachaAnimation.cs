using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GachaAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform itemContainer;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Image winnerHighlight;

    [Header("Animation Settings")]
    [SerializeField] private int totalSlots = 30;
    [SerializeField] private int winnerSlotIndex = 22;
    [SerializeField] private float slotHeight = 100f;

    [Header("Speed Settings")]
    [SerializeField] private float fastScrollDuration = 0.5f;
    [SerializeField] private float slowScrollDuration = 1.5f;
    [SerializeField] private float finalSnapDuration = 0.3f;
    [SerializeField] private float initialSpeed = 3000f;

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

        float startY = slotHeight * (totalSlots - 5);
        itemContainer.anchoredPosition = new Vector2(0, -startY);

        float targetY = -(slotHeight * winnerSlotIndex);

        // Fast spinning
        yield return StartCoroutine(AnimateScroll(itemContainer.anchoredPosition.y,
                                                  targetY + (slotHeight * 10),
                                                  fastScrollDuration,
                                                  AnimationCurve.EaseInOut(0, 0, 1, 1)));

        // Slower spinning
        yield return StartCoroutine(AnimateScroll(itemContainer.anchoredPosition.y,
                                                  targetY + (slotHeight * 2),
                                                  slowScrollDuration,
                                                  AnimationCurve.EaseInOut(0, 0, 1, 1)));
        
        // Final spins
        yield return StartCoroutine(AnimateScroll(itemContainer.anchoredPosition.y,
                                                  targetY,
                                                  finalSnapDuration,
                                                  AnimationCurve.EaseInOut(0, 0, 1, 1)));

        // yield return StartCoroutine(RevealWinner());
        
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
            slotRect.anchoredPosition = new Vector2(0, -i * slotHeight);

            perkSlots.Add(slotObj);
        }
    }

    private void SetupSlotUI(GameObject slotObj, PerkSO perk)
    {
        Image icon = slotObj.transform.Find("PerkIcon")?.GetComponent<Image>();
        TextMeshProUGUI nameText = slotObj.transform.Find("PerkName")?.GetComponent<TextMeshProUGUI>();
        
        if(icon != null && perk.icon != null)
        {
            icon.sprite = perk.icon;
        }

        if(nameText != null)
        {
            nameText.text = perk.perkName;
        }

        // PerkSlotData data = slotObj.AddComponent<PerkSlotData>();
        // data.perk = perk;
        // data.background = background;
    }

    private IEnumerator AnimateScroll(float startY, float endY, float duration, AnimationCurve curve)
    {
        float elapsed = 0f;

        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float curveValue = curve.Evaluate(t);

            float currentY = Mathf.Lerp(startY, endY, curveValue);
            itemContainer.anchoredPosition = new Vector2(0, currentY);

            yield return null;
        }

        itemContainer.anchoredPosition = new Vector2(0, endY);
    }

    // private IEnumerator RevealWinner()
    // {
        
    // }

    public bool IsAnimating()
    {
        return(isAnimating);
    }
}
