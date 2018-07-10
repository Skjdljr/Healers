using UnityEngine;
using HM_Utils;

public class SpellBase : Ability 
{
    //Spell details
    public bool isStackable { get; set; }
    public int radius { get; set; }
    public float castTime { get; set; }
    public float duration { get; set; }
    public float resourceCost { get; set; }
    public float interval { get; set; }
    //Move all of the spell types to there own class
    public SPELL_TYPE spellType { get; set; }
    public SPELL_SCHOOL spellSchool { get; set; }
    public CLASS_SPECIFIC_TYPE classType {get; set;}
    public SPELL_EFFECT_TYPE spellEffectType { get; set; }

    //No idea about this
    //public ParticleSystem spellEffect { get; set; }

	// Use this for initialization
	void Start () 
    {
        Debug.Log("Spellbase::Start()");
        SetSpell();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void SetSpell(string name = "Base Spell", float _power=0,float _coolDown = .1f, float _castTime = .1f, float _duration = 1, float _resourceCost = 10, float _interval = 0.0f,
                                    bool _isStackable = false,  string _path="", SPELL_SCHOOL _school = SPELL_SCHOOL.BASE, SPELL_TYPE _spellType = SPELL_TYPE.HEAL, 
                                          CLASS_SPECIFIC_TYPE _classType = CLASS_SPECIFIC_TYPE.ALL)
    {
        power = _power;
        displayName = name;
        coolDown = _coolDown;
        castTime = _castTime;
        duration = _duration;
        interval = _interval;
        resourceCost = _resourceCost;
        isStackable = _isStackable;
        spellSchool = _school;
        spellType = _spellType;
        classType = _classType;
        imagePath = _path;
    }
}
