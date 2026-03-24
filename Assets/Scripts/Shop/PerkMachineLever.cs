using UnityEngine;
using UnityEngine.EventSystems;

public class PerkMachineLever : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float pullPower = .2f;
    [SerializeField] private Vector2 rotationLimits = new Vector2(-35, -145);
    
    private RectTransform rectTransform;
    private float currentRotation;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        currentRotation = rotationLimits.y;
        rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragDistance = eventData.pressPosition - eventData.position;
        Debug.Log(dragDistance.y);
        float updatedRotation = currentRotation - (dragDistance.y * pullPower * Time.deltaTime);
        Debug.Log(updatedRotation);
        currentRotation = Mathf.Clamp(updatedRotation, rotationLimits.x, rotationLimits.y);
        
        rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        // if(updatedRotation > rotationLimits.x && updatedRotation < rotationLimits.y)
        // {
        //     currentRotation = updatedRotation;
        //     rectTransform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        // }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        return;
    }
}
