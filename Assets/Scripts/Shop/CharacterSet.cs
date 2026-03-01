using UnityEngine;

[System.Serializable]
public class CharacterUpgrade
{
    public Sprite upgradeIcon;
    public CharacterSO characterSO;
}

[CreateAssetMenu(fileName = "CharacterSet", menuName = "Shop Item/Character Set")]
public class CharacterSet : ScriptableObject
{
    public string characterName;
    public CharacterUpgrade[] characterUpgrade = new CharacterUpgrade[3];
}
