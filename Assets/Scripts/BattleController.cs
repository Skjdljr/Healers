using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using rds;
using Assets.Inventory;

public class BattleController : MonoBehaviour
{
    //Expose these to the editor so we can say who can atack
    public bool partyCanAttack;
    public bool enemyCanAttack;
    public Party playerParty;
    public Party enemyParty;

    //Intro length
    public float introLength;

    private bool activated = false;
    private Party[] parties;

    //RDS Tables
    RDSTable masterTable = new RDSTable();
    RDSTable commonTable = new RDSTable();
    //RDSTable setTable = new RDSTable();
    //RDSTable uniqueTable = new RDSTable();

    private void Start()
    {
        parties = FindObjectsOfType<Party>();

        foreach (var party in parties)
        {
            party.OnDefeated += PartyDefeated;
        }

        //common loot
        CreateTables();

        StartCoroutine(IntroCountdown());
    }

    //This should be pulled in via xml
    private void CreateTables()
    {
        commonTable = new RDSTable();
        commonTable.AddEntry(new Armor("Cloth Chest Piece", ARMOR_CLASS.CLOTH, ITEM_SLOT.CHEST_ARMOR));
        commonTable.AddEntry(new Armor("Cloth Helmet", ARMOR_CLASS.CLOTH, ITEM_SLOT.HELMET));
        commonTable.AddEntry(new Armor("Padded Chest Piece", ARMOR_CLASS.LEATHER, ITEM_SLOT.CHEST_ARMOR));
        commonTable.AddEntry(new Armor("Padded Helmet", ARMOR_CLASS.LEATHER, ITEM_SLOT.HELMET));
        commonTable.AddEntry(new Armor("Plate Chest Piece", ARMOR_CLASS.PLATE, ITEM_SLOT.CHEST_ARMOR));
        commonTable.AddEntry(new Armor("Plate Helment", ARMOR_CLASS.PLATE, ITEM_SLOT.HELMET));
        commonTable.AddEntry(new Weapon("Default Stick", WEAPON_CLASS.TWO_HANDED, WEAPON_TYPE.STAFF));
        commonTable.AddEntry(new Weapon("Blunt Object", WEAPON_CLASS.ONE_HANDED, WEAPON_TYPE.MACE));
        commonTable.AddEntry(new Shield("Small Shield", SHIELD_TYPE.SMALL));
        commonTable.AddEntry(new Shield("Medium Shield", SHIELD_TYPE.MEDIUM));
        commonTable.AddEntry(new Shield("Large Shield", SHIELD_TYPE.LARGE));
        commonTable.AddEntry(new Jewelry("Plain Amulet"));
        commonTable.AddEntry(new Jewelry("Silver Ring"));
        commonTable.AddEntry(new Jewelry("Copper Band"));
        commonTable.AddEntry(new Misc("Note", "A torn message"));

        masterTable.AddEntry(commonTable, 10);
        //masterTable.AddEntry(setTable, 10);
        //masterTable.AddEntry(uniqueTable, 10);

    }

    private void Update()
    {
    }

    public BaseCharacter GetRandomEnemy(Party.PartyType type)
    {
        //The party that is getting attacked (is opposite of what is passed in). Ie. if a player calls get random enemy it is looking for 
        //some 1 in the enemy party
        Party victims = type == Party.PartyType.Enemy ? playerParty : enemyParty;

        //The party that is attacking
        Party aggresors = type == Party.PartyType.Enemy ? enemyParty : playerParty;
        
        BaseCharacter enemy = null;

        // Get any party members who aren't fainted also only attack if we are not dead
        if (victims != null && victims._partyMembers != null && aggresors != null)
        {
            var members = victims._partyMembers.Where(pm => pm.characterState != BaseCharacter.CHARACTER_STATE.Fainted).ToList();

            if (members.Count > 0 )
                enemy = members[Random.Range(0, members.Count)];
        }

        return enemy;
    }

    private IEnumerator IntroCountdown()
    {
        yield return new WaitForSeconds(introLength);

        foreach (var party in parties)
        {
            party.Activate();
        }

        activated = true;
    }

    private void PartyDefeated(Party.PartyType type)
    {
        if (type == Party.PartyType.Player)
        {
            Debug.Log("Ya lost.");


            SceneManager.LoadScene("FailedScreen");
        }
        else
        {
            Debug.Log("Ya won.");

            // Tell the table we want to have 2 out of amount
            masterTable.rdsCount = 2;
            
            // First demo: Simply loot 2 out of the 6
            Debug.Log("Step 1: Just loot 2 out TOTAL table - 3 runs");
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Run " + (i + 1));
                foreach (ItemBase m in masterTable.rdsResult)
                    Debug.Log( m._itemName);
            }

            // Now set Item 6 to drop always
            //m6.rdsAlways = true;
            //Debug.Log("Step 2: Item 6 is now set to Always=true - 3 runs");
            //for (int i = 0; i < 3; i++)
            //{
            //    Debug.Log("Run " +  (i + 1));
            //    foreach (ItemBase m in t.rdsResult)
            //        Debug.Log(m._itemName);
            //}

            SceneManager.LoadScene("LootScreen");
        }
    }
}
