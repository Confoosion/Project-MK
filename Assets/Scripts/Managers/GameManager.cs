using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    [SerializeField] private List<GameObject> normalMaps;
    [SerializeField] private List<GameObject> fantasyMaps;
    [SerializeField] private List<GameObject> allMaps;
    public static int mapNumber;
    public static PerkSO perk;

    void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Singleton = this;
        DontDestroyOnLoad(this.gameObject);
    }

}
