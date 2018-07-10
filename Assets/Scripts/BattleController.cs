using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        parties = FindObjectsOfType<Party>();

        foreach (var party in parties)
        {
            party.OnDefeated += PartyDefeated;
        }

        StartCoroutine(IntroCountdown());
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
            SceneManager.LoadScene("LootScreen");
        }
    }
}
