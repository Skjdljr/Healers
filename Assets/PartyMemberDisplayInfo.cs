using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PartyMemberDisplayInfo : MonoBehaviour
{
    //Stats (Left of display)
    public Text Health;
    public Text Armor;
    public Text Resource;
    public Text Damage;
    public Text Crit;
    public Text AttackSpeed;

    //Resistances (Right side of display)
    public Text Fire;
    public Text Ice;
    public Text Poision;
    public Text Mind;
    public Text Lightning;
    public Text Physical;

    //Ability (Bottom of display)
    public Text Class_ability;

    void Start()
    {
    }

}
