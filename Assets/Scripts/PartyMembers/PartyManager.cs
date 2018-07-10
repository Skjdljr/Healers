using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    #region Events
    public delegate void Defeated(PartyType t);
    //TODO? maybe something with this
    //public event Defeated OnDefeated;
    #endregion

    #region Enums
    public enum PartyType { Enemy, Player};
    #endregion

    #region Public Variables
    public int MAX_PARTY_SIZE = 5;
    public List<BaseCharacter> partyMembers = new List<BaseCharacter>();
    public List<BaseCharacter> enemies = new List<BaseCharacter>();
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
        //TODO: have enmey types and a set ammount
        CharacterData charData = new CharacterData();
        charData = new Warrior();
        charData.displayName = "Enemy Warrior" + 0;
        charData.classType = HM_Utils.CLASS_SPECIFIC_TYPE.WARRIOR;

        charData.health = 100;
        enemies[0].Init(charData, Party.PartyType.Enemy);
    }

    private void FillParty()
    {
        for (int i = 0; i < MAX_PARTY_SIZE; i++)
        {
            var rand = Random.Range(0, 3);

            //Get data from the global class 
            //add component of type (war/rogue/mage) to partymember
            //populate partymem with data

            CharacterData charData = new CharacterData();
            
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
            charData.health = 20;

            partyMembers[i].Init(charData, Party.PartyType.Player);
        }
    }

    public void EditCharacter()
    {

    }

    public void Activate()
    {

    }
}
