using UnityEngine;
using UnityEngine.UI;

public class ProgressBarPrediction : MonoBehaviour
{
    [SerializeField] private Slider predictionSlider;

    void Start()
    {
        UpdatePrediction();
    }

    public void UpdatePrediction()
    {
        predictionSlider.value = LevelManager.Singleton.GetPredictionProgress();
    } 
}
