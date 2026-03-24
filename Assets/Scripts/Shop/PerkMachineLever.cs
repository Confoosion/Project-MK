using UnityEngine;
using UnityEngine.EventSystems;

public class PerkMachineLever : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Vector2 rotationLimits = new Vector2(-145, -35);
    [SerializeField] private float pullRotation = -100f;
    [SerializeField] private float maxDragDistance = 200f;
    [SerializeField] private float smoothSpeed = 10f;
    
    private RectTransform rectTransform;
    private float currentRotation;
    private float targetRotation;
    private bool leverPulled;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        leverPulled = false;
        currentRotation = rotationLimits.y;
        targetRotation = currentRotation;
        rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float dragDistance = eventData.pressPosition.y - eventData.position.y;
        Debug.Log(dragDistance);

        float updatedRotation = Mathf.Clamp01(-dragDistance / maxDragDistance);
        Debug.Log(updatedRotation);

        targetRotation = Mathf.Lerp(rotationLimits.x, rotationLimits.y, updatedRotation);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentRotation < pullRotation && !leverPulled)
        {
            UpdateLever(true);
            ShopManager.Singleton.LeverPulled();
        }
        else
        {
            UpdateLever(false);
        }
    }

    void Update()
    {
        if(Mathf.Abs(targetRotation - currentRotation) > 0.01f)
        {
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * smoothSpeed);
            rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
    }

    public void UpdateLever(bool pulled)
    {
        leverPulled = pulled;
        if(leverPulled)
        {
            targetRotation = rotationLimits.x;
        }
        else
        {
            targetRotation = rotationLimits.y;
        }
    }
}
