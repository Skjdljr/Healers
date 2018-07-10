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
    public List<BaseCharacter> partyMembers = new List<BaseCharacter>();
    public PartyType Type;
    #endregion

    // Use this for initialization
    private void Start()
    {
        if (Type == PartyType.Player)
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
        for(int i = 0; i < partyMembers.Count; ++i)
        {
            //TODO: have enmey types and a set ammount
            CharacterData charData = new Warrior();
            charData.displayName = "Enemy Warrior" + i;
            charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.WARRIOR;

            charData.health = 25;
            partyMembers[i].Init(charData, Type);
        }
    }

    private void FillParty()
    {
        for (int i = 0; i < MAX_PARTY_SIZE; i++)
        {
            var rand = Random.Range(0, 3);

            //Get data from the global class 
            //add component of type (war/rogue/mage) to partymember
            //populate partymem with data

            CharacterData charData = null;
            
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

            //TODO: this is to set there health with out taking dmg
            charData.health = Random.Range(20, 45);

            partyMembers[i].Init(charData, Type);
        }
    }

    public void EditCharacter()
    {

    }

    public void Activate()
    {
        foreach(var member in partyMembers)
        {
            member.Activate();
        }
    }

    private void Update()
    {
        var result = partyMembers.Count(p => p.characterState == BaseCharacter.CHARACTER_STATE.Fainted);
        if(result == partyMembers.Count && OnDefeated != null)
        {
            OnDefeated(Type);
            enabled = false;
        }
    }
}
