using UnityEngine;
using System.Collections.Generic;

public class GlobalGameData : MonoBehaviour
{
    public static Player playerCharacter { get; set; }
    private static List<CharacterData> partyMembers;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        partyMembers = new List<CharacterData>();
    }

    public static List<CharacterData> GetPartyMembers()
    {
        return partyMembers;
    }

    public static void AddPartyMember(CharacterData member)
    {
        if (partyMembers != null)
        {
            if (member != null && !partyMembers.Contains(member))
            {
                partyMembers.Add(member);
            }
        }
    }

    public static void RemovePartyMember(CharacterData member)
    {
        if (partyMembers != null)
        {
            if (member != null && !partyMembers.Contains(member))
            {
                partyMembers.Remove(member);
            }
        }
    }
}
