using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PartyMenu : MonoBehaviour
{
    public PartyMemberDisplayInfo memberDisplay;
    public List<ClassSelectionButton> partyButtons;
    public List<CharacterData> characters = new List<CharacterData>();

    // Use this for initialization
    void Start()
    {

        CharacterData randChar = new CharacterData();

        randChar = new Warrior();
        randChar.displayName = "Warrior";
        characters.Add(randChar);

        randChar = new Mage();
        randChar.displayName = "Mage";
        characters.Add(randChar);

        randChar = new Rogue();
        randChar.displayName = "Rogue";
        characters.Add(randChar);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnContinue_Clicked()
    {
        SceneManager.LoadScene("GameScreen");
    }

    public void OnButtonTest()
    {
        print("Here you go zach");
    }


    public void OnMemberClicked()
    {
        //memberDisplay.Damage.text = characters[0]..ToString();
        memberDisplay.Health.text = characters[0].maxHealth.ToString();
        memberDisplay.Armor.text = characters[0].maxArmor.ToString();
        memberDisplay.Crit.text = characters[0].critChance.ToString();
        memberDisplay.Resource.text = characters[0].resource.ToString();
        memberDisplay.AttackSpeed.text = characters[0].attackSpeed.ToString();

        //Resis
        memberDisplay.Fire.text = characters[0].fireResist.ToString();
        memberDisplay.Ice.text = characters[0].iceResist.ToString();
        memberDisplay.Poision.text = characters[0].poisonResist.ToString();
        memberDisplay.Mind.text = characters[0].mindResist.ToString();
        memberDisplay.Lightning.text = characters[0].lightningResist.ToString();
        memberDisplay.Physical.text = characters[0].physyicalResist.ToString();

        memberDisplay.Health.text = characters[0].classType.ToString();
        print("Menu Button clicked");
    }
}
