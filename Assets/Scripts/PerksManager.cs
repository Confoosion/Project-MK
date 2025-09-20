using UnityEngine;

public class PerksManager : MonoBehaviour
{
    public static PerksManager Singleton { get; private set; }

    [SerializeField] private PlayerControl playerControl;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void AddJumps(int num)
    {
        playerControl.extraJumps = num;
    }

    public void IncreaseSpeed(float num)
    {
        playerControl.speed += num;
    }
}
