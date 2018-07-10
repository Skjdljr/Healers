using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text ClassDescription;
    public Text ClassTitle;

    //public class Stats

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ChangeDescription(string className)
    {

    }

    public void OnContinue_Clicked()
    {
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
                    break;
                }

            case "Paladin":
                {
                    SetDescription("A healer of the justice that wants to defend the weak. Known for direct heals and buffs ");
                    break;
                }
            case "Priest":
                {
                    SetDescription("A healer of the divine who is stead fast in there beliefs. Known for great shielding");
                    break;
                }
            case "Shaman":
                {
                    SetDescription("A healer of the spirits who communes with them to bring balance. Known for fast and steady heals");
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