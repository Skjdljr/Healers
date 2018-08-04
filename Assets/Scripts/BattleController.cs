using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using rds;

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

    //RDS Table
    RDSTable t = new RDSTable();

    private void Start()
    {
        parties = FindObjectsOfType<Party>();

        foreach (var party in parties)
        {
            party.OnDefeated += PartyDefeated;
        }

        //test loot
        CreateTestLootTable();

        StartCoroutine(IntroCountdown());
    }

    private void CreateTestLootTable()
    {
        RDSTable subtable1 = new RDSTable();
        RDSTable subtable2 = new RDSTable();
        RDSTable subtable3 = new RDSTable();
        t.AddEntry(subtable1, 10); // we add a table to a table thanks to the interfaces
        t.AddEntry(subtable2, 10);
        t.AddEntry(subtable3, 10);

        subtable1.AddEntry(new ItemBase("Table 1 - Item 1"), 10);
        subtable1.AddEntry(new ItemBase("Table 1 - Item 2"), 10);
        subtable1.AddEntry(new ItemBase("Table 1 - Item 3"), 10);
        subtable2.AddEntry(new ItemBase("Table 2 - Item 1"), 10);
        subtable2.AddEntry(new ItemBase("Table 2 - Item 2"), 10);
        subtable2.AddEntry(new ItemBase("Table 2 - Item 3"), 10);
        subtable3.AddEntry(new ItemBase("Table 3 - Item 1"), 10);
        subtable3.AddEntry(new ItemBase("Table 3 - Item 2"), 10);
        subtable3.AddEntry(new ItemBase("Table 3 - Item 3"), 10);

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
        if (victims != null && victims.partyMembers != null && aggresors != null)
        {
            var members = victims.partyMembers.Where(pm => pm.characterState != BaseCharacter.CHARACTER_STATE.Fainted).ToList();

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

            ItemBase m6 = new ItemBase("Item 6"); // We need this item later
            t.AddEntry(m6, 10);
            
            // Tell the table we want to have 2 out of 6
            t.rdsCount = 2;
            
            // First demo: Simply loot 2 out of the 6
            Debug.Log("Step 1: Just loot 2 out 6 - 3 runs");
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Run " + (i + 1));
                foreach (ItemBase m in t.rdsResult)
                    Debug.Log( m._itemName);
            }

            // Now set Item 6 to drop always
            m6.rdsAlways = true;
            Debug.Log("Step 2: Item 6 is now set to Always=true - 3 runs");
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Run " +  (i + 1));
                foreach (ItemBase m in t.rdsResult)
                    Debug.Log(m._itemName);
            }

            SceneManager.LoadScene("LootScreen");
        }
    }
}
