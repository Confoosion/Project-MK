using UnityEngine;

public class PerksManager : MonoBehaviour
{
    public static PerksManager Singleton { get; private set; }

    [SerializeField] private PlayerControl player;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void AddJumps(int num)
    {
        player.extraJumps = num;
    }

    public void IncreaseSpeed(float num)
    {
        player.speed += num;
    }
}
