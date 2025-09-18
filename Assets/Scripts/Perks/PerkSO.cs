using UnityEngine;

public abstract class PerkSO : ScriptableObject
{
    public string perkName;
    [TextArea] public string perkDescription;
    public Sprite icon;

    public abstract void ApplyPerk();
}
