using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject perkMapCanvas;


    void Start()
    {
        mainMenuCanvas.SetActive(true);
        perkMapCanvas.SetActive(false);


    }

    public void menuPlayButton()
    {
        mainMenuCanvas.SetActive(false);
        perkMapCanvas.SetActive(true);
    }

    public void menuBackButton()
    {
        mainMenuCanvas.SetActive(true);
        perkMapCanvas.SetActive(false);
    }

    public void menuStartButton()
    {
        //Map stuff
        //MAP 2 on index 1
        // int mapNumber = mapDropdown.value + 1;
        // GameManager.mapNumber = mapNumber;

        SceneManager.LoadScene("StarterMap");
        // if (mapNumber == 1)
        // {
        //     //go to map 1
        //     SceneManager.LoadScene("StarterMap");
        // }
    }

    public void GoToShopScene()
    {
        SceneManager.LoadScene("ShopTest");
    }

}