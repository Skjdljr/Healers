using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
            //TODO: have enmey types and a set ammount
            CharacterData charData = new Warrior();
            charData.displayName = "Enemy Warrior" + i;
            charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.WARRIOR;

            charData.health = 1;
            _partyMembers[i].Init(charData, _partyType);
        }
    }

    private void FillParty()
    {
        //Max party size -1 because you have to be in the group
        for (int i = 0; i < MAX_PARTY_SIZE; i++)
        {
            var rand = Random.Range(0, 3);

            //Get data from the global class 
            //add component of type (war/rogue/mage) to partymember
            //populate partymem with data

            CharacterData charData = null;

            if (i == MAX_PARTY_SIZE)
            {
                //create the player character!!!!!

                charData = new CharacterData();
                
                //todo: base stats different per class
                charData.SetBaseStats("Healer", 100, 100, 100, 1, 1, HM_Utils.CLASS_SPECIFIC_TYPE.ALL);

                //todo: resistances different per class
                charData.SetResistances();
            }
            else
            {
                switch (rand)
                {
                    case 0:
                        charData = new Warrior();
                        charData.displayName = "Warrior" + i;
                        charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.WARRIOR;
                        break;
                    case 1:
                        charData = new Mage();
                        charData.displayName = "Mage" + i;
                        charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.MAGE;
                        break;
                    case 2:
                        charData = new Rogue();
                        charData.displayName = "Rogue" + i;
                        charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.ROGUE;
                        break;
                }

                //override their health
                charData.health = Random.Range(20, 45);
            }

            _partyMembers[i].Init(charData, _partyType);
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
