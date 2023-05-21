using UnityEngine;

[CreateAssetMenu(fileName = "New Powerup", menuName = "Powerup")]
public class PowerUpScriptableObject : ScriptableObject
{
    public string powerupName;
    public Sprite icon;
    public string description;
    public Rarity rarity;
    public Effect effect;
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Unique
}

[System.Serializable]
public enum Effect
{

}
