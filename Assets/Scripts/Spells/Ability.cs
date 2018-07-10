using UnityEngine;

public abstract class Ability
{
    public string displayName { get; set; }
    public float power { get; set; }
    public float coolDown { get; set; }
    public string imagePath { get; set; }
    public Texture tImage { get; set; }

    public void SetAbility(string name, float _power, float cd, string _imagePath)
    {
        displayName = name;
        power = _power;
        coolDown = cd;
        imagePath = _imagePath;
    }
}
