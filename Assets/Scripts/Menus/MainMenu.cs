using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button NewGame = null;
    public Button Options = null;
    public Button Credits = null;
    public Button Resume = null;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnButton_Clicked(string btnName)
    {
        print(btnName + " button clicked");

        switch (btnName)
        {
            case "NewGame":
                {
                    SceneManager.LoadScene("CharacterSelect");
                    break;
                }
            case "Resume":
                {
                    break;
                }
            case "Credits":
                {
                    break;
                }
            case "Options":
                {
                    break;
                }
        }
    }
}