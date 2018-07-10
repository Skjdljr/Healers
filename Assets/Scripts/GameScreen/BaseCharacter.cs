﻿using HM_Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    public enum CHARACTER_STATE { Idle, Attacking, Blocking, Stunned, Silenced, Fainted };

    public CharacterData data { get; set; }
    public CHARACTER_STATE characterState { get; set; }
    public Slider HealthBar = null;
    public Slider ArmorBar = null;
    public Slider ResourceBar = null;
    public Text Name = null;
    public Text Level = null;
    public Image classDisplayImage = null;
    public Party.PartyType partyType;
    public float OverHealAmnt = 0;
    Coroutine routine = null;

    public Animator animator;

    public SpellEffect effectPrefab;
    public HorizontalLayoutGroup ActiveEffects;

    public delegate void CharacterSlected(BaseCharacter character);
    public CharacterSlected OnCharacterSelected;

    void Start()
    {
        //Set the buttons clicked method via code;
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        animator = GetComponent<Animator>();
        Random.InitState(221);
    }

    public void OnButtonClicked()
    {
        //log the dmg
        Debug.Log(this.data.displayName + " clicked");

        if(OnCharacterSelected != null)
            OnCharacterSelected(this);
    }

    public void Activate()
    {
        characterState = CHARACTER_STATE.Idle;

        StartCoroutine("StartingDelay");
    }

    //This is to make it so classes of the same type don't attack at the same time
    //Might want to work on an initiative system to determine who attacks when
    private IEnumerator StartingDelay()
    {
        yield return new WaitForSeconds(GetRandom(0.0f, 3.0f));

        routine = StartCoroutine("AttackLoop");
    }

    private IEnumerator AttackLoop()
    {
        while (characterState == CHARACTER_STATE.Idle)
        {
            //TODO: change this to delay between attacks
            yield return new WaitForSeconds(data.attackSpeed);

            Attack();
        }
        
        yield return null;
    }

    // Use this for initialization
    public void Init(CharacterData _char, Party.PartyType type)
    {
        partyType = type;
        data = _char;

        if (data != null)
        {
            characterState = CHARACTER_STATE.Idle;
            Name.text = data.displayName;
            HealthBar.value = (data.health / data.maxHealth);
            ArmorBar.value = (data.armor / data.maxArmor);
            ResourceBar.value = (data.resource / data.maxResource);
            SetDisplayImage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (data != null)
        {
            HealthBar.value = (data.health / data.maxHealth);
            ArmorBar.value = (data.armor / data.maxArmor);
            ResourceBar.value = (data.resource / data.maxResource);
        }
    }

    public virtual void Attack()
    {
        var battleController = FindObjectOfType<BattleController>();
        var enemy = battleController.GetRandomEnemy(partyType);

        if (enemy != null && CanAttack(battleController))
        {
            //Set our selves to attack
            characterState = CHARACTER_STATE.Attacking;

            //Animate our attack
            animator.SetBool("Attack", true);

            var dmg = CalculateDamage(enemy);

            //TODO: determine if we are attacking or casting a spell. (pass the info to CalculateDamage)
            enemy.TakeDamage(dmg);

            //log the dmg
            Debug.Log(data.displayName + " attacks " + enemy.data.displayName + " for " + dmg + " damage.");
        }
    }

    //Called at the end of the animation via trigger
    public void AttackAimFinished()
    {
        animator.SetBool("Attack", false);

        //Not sure about this done now go back to idle
        characterState = CHARACTER_STATE.Idle;

        //This could be terrible ask some 1
        routine = StartCoroutine("AttackLoop");
    }
    
    protected float GetRandom(float min, float max)
    {
        return Random.Range(min, max);
    }

    public virtual bool CanAttack(BattleController bc)
    {
        //check the battle controller for the type and if it is allowed to attack
        var canAttack = partyType == Party.PartyType.Enemy ? bc.enemyCanAttack : bc.partyCanAttack;

        Debug.Log(data.displayName + " canAttack " + canAttack);

        //TODO: may have to revist this. 
        return characterState == CHARACTER_STATE.Idle && canAttack;
    }

    public virtual void TakeDamage(float damage)
    {
        //If fainted don't let them take more damage?
        if (characterState != CHARACTER_STATE.Fainted)
        {
            //TODO: hit reaction
            data.health -= damage;

            if (data.health <= 0)
            {
                //TODO: death anim
                data.health = 0;

                //TODO: maybe grey out the background as well
                SetFainted();
            }
        }
    }

    private void AddHealth(float amount)
    {
        data.health += amount;

        if (data.health >= data.maxHealth)
        {
            OverHealAmnt += (data.health - data.maxHealth);
            data.health = data.maxHealth;
        }
    }

    public virtual void Heal(float amount)
    {
        AddHealth(amount);
        //TODO: effects, sounds
    }

    public virtual void Heal(SpellBase spell)
    {
        Heal(spell.power);
    }

    public void SetFainted()
    {
        characterState = CHARACTER_STATE.Fainted;

        classDisplayImage.sprite = Utils.LoadSprite("tombstone", SPITE_LOCATIONS.PARTY_MEMBERS);

        //Shouldn't be attacking if dead :)
        if (routine != null)
        {
            animator.SetBool("Attack", false);
            
            StopCoroutine(routine);
            routine = null;
        }
    }


    public void SetDisplayImage()
    {
        Sprite s = null;

        switch (data.classType)
        {
            case CLASS_SPECIFIC_TYPE.WARRIOR:
                s = Utils.LoadSprite("battle-axe", SPITE_LOCATIONS.PARTY_MEMBERS);
                classDisplayImage.sprite = s;
                break;
            case CLASS_SPECIFIC_TYPE.MAGE:
                s = Utils.LoadSprite("crystal-wand", SPITE_LOCATIONS.PARTY_MEMBERS);
                classDisplayImage.sprite = s;
                break;
            case CLASS_SPECIFIC_TYPE.ROGUE:
                s = Utils.LoadSprite("stiletto", SPITE_LOCATIONS.PARTY_MEMBERS);
                classDisplayImage.sprite = s;
                break;
        }
    }

    public void AddSpellEffect(SpellBase effect)
    {
        if (effect != null)
        {
            var prefab = Instantiate(effectPrefab);
            prefab.SetEffect(effect, this);
            prefab.transform.SetParent(ActiveEffects.transform);
        }
    }

    //TODO: have attack speed determine how often this happens in the battle controller
    private float CalculateDamage(BaseCharacter target)
    {
        //TODO: check the type of damage being applied (then check against the correct resistance)
        //Damagereduction float value between 0-1
        var dmg = data.damage * (1 - target.data.physyicalResist);

        //have to make sure crit chance is 0-1
        if (Random.Range(0.0f, 1.0f) >= (1 - data.critChance))
        {
            // Critical hit 1.5/3.0% extra dmg
            //TODO: have adjustable modifiers (per char) 
            dmg *= GetRandom(1.5f, 3.0f);
        }

        return dmg;
    }

    //Could be used to purge spells
    private void RemoveSpellEffect(SpellEffect effect)
    {
        Destroy(effect.gameObject);
    }
}
