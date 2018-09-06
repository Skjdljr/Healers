using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.PartyMembers.Classes.Playable;

public class Party : MonoBehaviour
{
    #region Events
    public delegate void Defeated(PartyType t);
    public event Defeated OnDefeated;
    #endregion

    #region Enums
    public enum PartyType { Enemy, Player};
    #endregion

    #region Public Variables
    public int MAX_PARTY_SIZE = 5;
    public List<BaseCharacter> _partyMembers = new List<BaseCharacter>();
    public PartyType _partyType;
    #endregion

    // Use this for initialization
    private void Start()
    {
        if (_partyType == PartyType.Player)
        {
            FillParty();
        }
        else
        {
            FillEnemies();
        }
    }

    private void FillEnemies()
    {
        for(int i = 0; i < _partyMembers.Count; ++i)
        {
            //todo: joel fix enemy health before exe
            SpawnRandomClass(i, 5, 15);
        }
    }

    private void SpawnRandomClass(int index, float minHealth = 20, float maxHealth = 100, string name = "")
    {
        CharacterData charData = null;

        var range = Random.Range(0, 3);

        string defaultName = "";

        defaultName = !string.IsNullOrEmpty(name) ? name : _partyType.ToString();

        switch (range)
        {
            case 0:
                charData = new Warrior();
                charData.displayName = defaultName + " Warrior " + index;
                charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.WARRIOR;
                break;
            case 1:
                charData = new Mage();
                charData.displayName = defaultName + " Mage " + index;
                charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.MAGE;
                break;
            case 2:
                charData = new Rogue();
                charData.displayName = defaultName + " Rogue " + index;
                charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.ROGUE;
                break;
        }

        //override their health
        charData.health = Random.Range(minHealth, maxHealth);

        _partyMembers[index].Init(charData, _partyType);
    }


    private void FillParty()
    {
        //Max party size -1 because you have to be in the group
        for (int i = 0; i < MAX_PARTY_SIZE; i++)
        {
            CharacterData charData = null;

            if (i == MAX_PARTY_SIZE - 1)
            {
                //HAXXXXXX: 
                //create the player character and add it!!!!!
                charData = new Druid();
                _partyMembers[i].Init(charData, _partyType);
            }
            else
            {
                SpawnRandomClass(i, 10, 25);
            }
        }
    }

    public void EditCharacter()
    {

    }

    public void Activate()
    {
        foreach(var member in _partyMembers)
        {
            member.Activate();
        }
    }

    private void Update()
    {
        var result = _partyMembers.Count(p => p.characterState == BaseCharacter.CHARACTER_STATE.Fainted);
        if(result == _partyMembers.Count && OnDefeated != null)
        {
            OnDefeated(_partyType);
            enabled = false;
        }
    }
}
