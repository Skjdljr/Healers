using UnityEngine;
using System.Collections.Generic;
using HM_Utils;
using UnityEngine.Experimental.UIElements;

public class Player : MonoBehaviour
{ 
    public static Player instance;

    SpellSlotLogic curSelectedSpell = null;
    SpellSlotLogic nextSpell = null;
    BaseCharacter selectedCharacter = null;
    List<SpellBase> spells = null;

    public Slider manaSlider;
    //todo: healthslider
    public Slider healthSlider;

    public PLAYER_CLASS playerClass;
    public Castbar castbar;

    public float _health { get; set; }
    public float _maxHealth { get; set; }
    public float _mana { get; set; }
    public float _maxMana { get; set; }

    public PLAYER_STATE currentPlayerState { get; set; }

    private void SetTestSpells()
    {
        spells = new List<SpellBase>();

        SpellBase sb = new SpellBase();
        sb.SetSpell("Restoration", 15, 3, 1.50f, 6, 30, 2, false, "Druid.aoe", SPELL_SCHOOL.BASE, SPELL_TYPE.HEALOVERTIME, CLASS_SPECIFIC_TYPE.ALL);
        spells.Add(sb);

        sb = new SpellBase();
        sb.SetSpell("Regeneration", 25, 1.0f, 0.5f, 0, 15, 0, false, "regeneration", SPELL_SCHOOL.BASE, SPELL_TYPE.HEAL, CLASS_SPECIFIC_TYPE.ALL);
        spells.Add(sb);

        sb = new SpellBase();
        sb.SetSpell("Bloom", 10, 0, .20f, 3, 10, 1.5f, true, "Druid.Direct", SPELL_SCHOOL.BASE, SPELL_TYPE.HEALOVERTIME, CLASS_SPECIFIC_TYPE.ALL);
        spells.Add(sb);
    }

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        var parties = FindObjectsOfType<Party>();
        foreach (var p in parties)
        {
            if (p._partyType == Party.PartyType.Player)
            {
                foreach (var character in p._partyMembers)
                {
                    character.OnCharacterSelected += PartyMemberSelected;
                    //character.OnButtonClicked += PartyMemberSelected;
                }
            }
        }

        var spellSlots = FindObjectsOfType<SpellSlotLogic>();

        //TODO: Get this from the player that is set in the character select
        SetTestSpells();
        InitClass(100, 110, PLAYER_CLASS.PRIEST, spells);

        var index = 0;
        foreach (var s in spellSlots)
        {
            s.SetSpell(this.spells[index]);
            s.OnSpellSlotClicked += SpellSlotClicked;
            index++;
        }

        castbar.OnCastingComplete += CastingComplete;
        castbar.OnCastingCanceled += CastingCanceled;
    }

    private void SpellSlotClicked(SpellSlotLogic spell)
    {
        print("Player: SpellSlotClicked " + spell.GetSpellData().displayName);

        if (castbar.isCasting)
            nextSpell = spell;
        else
            curSelectedSpell = spell;

    }

    private void PartyMemberSelected(BaseCharacter character)
    {
        if (curSelectedSpell != null)
        {
            print("Player: Party member selected " + character.data.displayName);
            selectedCharacter = character;
            TryCast();
        }
    }

    private void TryCast()
    {
        if (currentPlayerState != PLAYER_STATE.CASTING && selectedCharacter.characterState != BaseCharacter.CHARACTER_STATE.Fainted) /*!castbar.isCasting*/
        {
            var spell = curSelectedSpell.GetSpellData();

            if (CheckMana(spell.resourceCost) && !CheckCooldown(curSelectedSpell))
            {
                //Check the type of spell if it is a heal do not allow casting on enemy
                if (selectedCharacter.partyType == Party.PartyType.Player)
                {
                    currentPlayerState = PLAYER_STATE.CASTING;

                    //TODO: Move this to an event?
                    castbar.Cast(spell.castTime);

                    //store a copy for lerp
                    var currentMana = _mana;

                    //use the mana
                    _mana -= spell.resourceCost;

                    if (_mana <= 0)
                    {
                        _mana = 0;
                        manaSlider.value = _mana;
                    }
                    else
                    {
                        manaSlider.value = Mathf.Lerp(currentMana, _mana, _mana / _maxMana);
                    }
                }
                else
                {
                    Debug.Log("Casting on enemy - NEED TO ADD CODE HERE");
                    //bool canCastOnEnemy = true;
                    // (spell.spellType == SPELL_TYPE.HEAL || spell.spellType == SPELL_TYPE.HEALOVERTIME || spell.spellType == SPELL_TYPE.BUFF)
                }
            }
        }
    }

    private bool CheckMana(float manaCost)
    {
        bool good = false;
        if (_mana >= manaCost)
        {
            good = true;
        }
        else
        {
            //TODO: play sound or something
            print("Player: Does not have enough mana... Needed: " + manaCost + " available " + this._mana);
        }

        return good;
    }

    private bool CheckCooldown(SpellSlotLogic spell)
    {
        if (spell.onCooldown)
            print("Player: Spell " + spell.GetSpellData().displayName + " is still on cooldown");

        return spell.onCooldown;
    }

    private void CastingCanceled()
    {
        ClearCasting();
    }

    private void ClearCasting()
    {
        currentPlayerState = PLAYER_STATE.IDLE;

        // Make it so you can select your next spell while still casting
        curSelectedSpell = nextSpell != null ? nextSpell : null;
        nextSpell = null;
        selectedCharacter = null;
    }

    private void CastingComplete()
    {
        print("Player: Casting complete");

        if (selectedCharacter != null)
        {
            curSelectedSpell.Use(selectedCharacter);
        }

        ClearCasting();
    }

    public void InitClass(float _maxHealth, float _maxMana, PLAYER_CLASS _class, List<SpellBase> _spells)
    {
        _health = _maxHealth;
        _mana = _maxMana;
        this._maxMana = _maxMana;
        spells = _spells;
        playerClass = _class;
        currentPlayerState = PLAYER_STATE.IDLE;
    }

    public void PrintSpells()
    {
        print(Utils.ConvertPlayerClassToString(playerClass) + " Spell list ");
        var index = 1;
        foreach (var spell in spells)
        {
            print("Spell " + index + ": " + spell.displayName);
            index++;
        }
    }
}