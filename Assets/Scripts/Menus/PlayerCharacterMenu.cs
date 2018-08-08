using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HM_Utils;

public class PlayerCharacterMenu : MonoBehaviour
{
    public Text ClassDescription;
    public Text ClassTitle;

    private List<Player> playerClasses = new List<Player>();
    private Player selectedClass = null;

    void Start()
    {
        AddPlayerClasses();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddPlayerClasses()
    {
        playerClasses.Add(CreatePlayerClass(PLAYER_CLASS.DRUID, 100, 100, CreateDruidSpells()));
        playerClasses.Add(CreatePlayerClass(PLAYER_CLASS.PALADIN, 100, 100, CreatePaladinSpells()));
        playerClasses.Add(CreatePlayerClass(PLAYER_CLASS.PRIEST, 100, 110, CreatePriestSpells()));
        playerClasses.Add(CreatePlayerClass(PLAYER_CLASS.SHAMAN, 100, 100, CreateShamanSpells()));
    }

    private List<SpellBase> CreateDruidSpells()
    {
        List<SpellBase> _spells = new List<SpellBase>();

        SpellBase sb = new SpellBase();
        sb.SetSpell("Bloom", 5, 0, 0.1f, 3, 10, 1, true, "Druid.Direct", SPELL_SCHOOL.BASE, SPELL_TYPE.HEALOVERTIME, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        sb = new SpellBase();
        sb.SetSpell("Restoration", 25, 3, 1.0f, 6, 30, 0, false, "Druid.aoe", SPELL_SCHOOL.BASE, SPELL_TYPE.HEALOVERTIME, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        return _spells;
    }

    private List<SpellBase> CreatePaladinSpells()
    {
        List<SpellBase> _spells = new List<SpellBase>();

        SpellBase sb = new SpellBase();
        sb.SetSpell("Cure Wounds", 15, 0.1f, 0.5f, 0, 15, 0,false, "Druid.Direct", SPELL_SCHOOL.BASE, SPELL_TYPE.HEAL, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        sb = new SpellBase();
        sb.SetSpell("Virtue", 45, 10, 1.0f, 10, 30, 0, false, "Druid.aoe", SPELL_SCHOOL.PYHSICAL, SPELL_TYPE.BUFF, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        return _spells;
    }

    private List<SpellBase> CreatePriestSpells()
    {
        List<SpellBase> _spells = new List<SpellBase>();

        SpellBase sb = new SpellBase();
        sb.SetSpell("Mending", 15, 0.1f, 0.5f, 0, 15, 0, false, "Druid.Direct", SPELL_SCHOOL.BASE, SPELL_TYPE.HEAL, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        sb = new SpellBase();
        sb.SetSpell("Shield of Faith", 45, 10, 1.0f, 10, 30, 0, false, "Druid.aoe", SPELL_SCHOOL.PYHSICAL, SPELL_TYPE.BUFF, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        return _spells;
    }

    private List<SpellBase> CreateShamanSpells()
    {
        List<SpellBase> _spells = new List<SpellBase>();

        SpellBase sb = new SpellBase();
        sb.SetSpell("Soothe spirit", 10, 0.1f, 0.5f, 0, 15, 0, false, "Druid.Direct", SPELL_SCHOOL.BASE, SPELL_TYPE.HEAL, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        sb = new SpellBase();
        sb.SetSpell("Spirit of one", 45, 10, 1.0f, 10, 30, 0, false, "Druid.aoe", SPELL_SCHOOL.WILL, SPELL_TYPE.BUFF, CLASS_SPECIFIC_TYPE.ALL);
        _spells.Add(sb);

        return _spells;
    }

    private Player CreatePlayerClass(PLAYER_CLASS _class, int _maxHealth, float _maxMana, List<SpellBase> _spells)
    {
        Player pc = new Player();

        pc.InitClass(_maxHealth, _maxMana, _class, _spells);

        return pc;
    }

    private void ChangeDescription(string className)
    {

    }

    public void OnContinue_Clicked()
    {
        GlobalGameData.playerCharacter = selectedClass;
        print("Continue selected: CharacterMenu");
        SceneManager.LoadScene("PartyMenu");
    }

    public void OnButton_Clicked(string btnName)
    {
        print("Changed current selected class to:" + btnName);

        SetTitle(btnName);

        switch (btnName)
        {
            case "Druid":
            {
                SetDescription("A healer of nature that sees the balance of life and death. Known for great healing over time");
                selectedClass = playerClasses[(int)PLAYER_CLASS.DRUID];
                selectedClass.PrintSpells();
                break;
            }

            case "Paladin":
            {
                SetDescription("A healer of the justice that wants to defend the weak. Known for direct heals and buffs ");
                selectedClass = playerClasses[(int)PLAYER_CLASS.PALADIN];
                selectedClass.PrintSpells();
                break;
            }
            case "Priest":
            {
                SetDescription("A healer of the divine who is stead fast in there beliefs. Known for great shielding");
                selectedClass = playerClasses[(int)PLAYER_CLASS.PRIEST];
                selectedClass.PrintSpells();
                break;
            }
            case "Shaman":
            {
                SetDescription("A healer of the spirits who communes with them to bring balance. Known for fast and steady heals");
                selectedClass = playerClasses[(int)PLAYER_CLASS.SHAMAN];
                selectedClass.PrintSpells();
                break;
            }
        }

        ChangeDescription(btnName);
    }

    private void SetDescription(string desc)
    {
        ClassDescription.text = desc;
    }

    private void SetTitle(string title)
    {
        ClassTitle.text = title;
    }
}