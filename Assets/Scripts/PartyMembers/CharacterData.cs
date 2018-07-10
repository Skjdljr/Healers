using HM_Utils;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterData
{
    public string displayName { get; set; }
    public Image IconImage { get; set; }
    public List<Ability> Abilities;

    //basics
    public float health { get; set; }
    public float maxHealth { get; set; }
    public float armor { get; set; }
    public float maxArmor { get; set; }
    public float resource { get; set; }
    public float maxResource { get; set; }

    //Resistances
    public float physyicalResist { get; set; }
    public float fireResist { get; set; }
    public float lightningResist { get; set; }
    public float iceResist { get; set; }
    public float poisonResist { get; set; }
    public float mindResist { get; set; }

    //damage
    public float critChance { get; set; }
    public float attackSpeed { get; set; }
    public float damage { get; set; }

    public CLASS_SPECIFIC_TYPE classType { get; set; }

    public CharacterData()
    {
        Start();
    }

    // Use this for initialization
    protected virtual void Start()
    {
        Abilities = new List<Ability>();

        SetBaseStats();
        SetResistances();
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="health"></param>
    /// <param name="armor"></param>
    public void SetBaseStats(string _name = "Base", int _health = 100, int _armor = 100, int _resource = 100, float dmg = 1.0f, float _attackSpeed = 2.0f, CLASS_SPECIFIC_TYPE _class = CLASS_SPECIFIC_TYPE.ALL)
    {
        displayName = _name;
        health = _health;
        maxHealth = _health;
        armor = _armor;
        maxArmor = _armor;
        damage = dmg;
        resource = _resource;
        maxResource = _resource;
        classType = _class;
        attackSpeed = _attackSpeed;
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetResistances(float _fireResist = .10f, float _lightningResist = .10f, float _iceResist = .10f, float _poisonResist = .10f, float _mindResist = .10f, float _physyical = .10f)
    {
        fireResist = _fireResist;
        lightningResist = _lightningResist;
        iceResist = _iceResist;
        poisonResist = _poisonResist;
        mindResist = _mindResist;
        physyicalResist = _physyical;
    }

	// Update is called once per frame
	virtual public void Update()
    {
	    
	}
}
