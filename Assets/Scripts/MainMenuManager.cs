using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Dropdown perkDropdown;
    [SerializeField] private Dropdown mapDropdown;


    [SerializeField] private PerkSO moreDamagePerk;
    [SerializeField] private PerkSO extraLifePerk;
    [SerializeField] private PerkSO futureTellingPerk;
    [SerializeField] private PerkSO slowEnemiesPerk;
    [SerializeField] private PerkSO doubleJumpPerk;
    [SerializeField] private PerkSO speedPerk;


    void Start()
    {




    }

    void menuStartButton()
    {
        //Map stuff
        int mapNumber = mapDropdown.value + 1;
        GameManager.mapNumber = mapNumber;

        if (mapNumber == 1)
        {
            //go to map 1
        }
        else if (mapNumber == 2)
        {
            //go to map 2
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
