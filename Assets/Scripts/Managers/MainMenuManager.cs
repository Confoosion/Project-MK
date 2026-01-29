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

    [Header("Perk/Map")]
    [SerializeField] private TMP_Dropdown perkDropdown;
    [SerializeField] private TMP_Dropdown mapDropdown;


    [SerializeField] private PerkSO moreDamagePerk;
    [SerializeField] private PerkSO extraLifePerk;
    [SerializeField] private PerkSO futureTellingPerk;
    [SerializeField] private PerkSO slowEnemiesPerk;
    [SerializeField] private PerkSO doubleJumpPerk;
    [SerializeField] private PerkSO speedPerk;


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
        int mapNumber = mapDropdown.value + 1;
        GameManager.mapNumber = mapNumber;

        if (mapNumber == 1)
        {
            //go to map 1
            SceneManager.LoadScene("Map2");
        }




        //Perk stuff
        int perkNumber = perkDropdown.value;

        if (perkNumber == 0)
        {
            //moreDamage 
            GameManager.perk = moreDamagePerk;
        }
        else if (perkNumber == 1)
        {
            //Extra Life
            GameManager.perk = extraLifePerk;
        }
        else if (perkNumber == 2)
        {
            //Future Telling
            GameManager.perk = futureTellingPerk;

        }
        else if (perkNumber == 3)
        {
            //Slow enemies
            GameManager.perk = slowEnemiesPerk;
        }
        else if (perkNumber == 4)
        {
            //Double Jump
            GameManager.perk = doubleJumpPerk;
        }
        else if (perkNumber == 5)
        {
            //Speed
            GameManager.perk = speedPerk;
        }



    }


}

//More Damage       0
//Extra Life        1
//Future Telling    2
//Slow enemies      3
// Double Jump      4
//Speed             5
