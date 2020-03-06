using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    // Splash Screen = Help windows for main menu / Player 1 / Player 2
    public GameObject SplashScreen;
    public GameObject SplashScreen1;
    public GameObject SplashScreen2;

    // Partition Screen for Player 1 / Player 2. hides opponents cards
    public GameObject Fader1;
    public GameObject Fader2;

    // Bools for checking each player for their respective end turn call
    public bool P1;
    public bool P2;

    void Start()
    {
        // Setting for camera angle
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
    public void Close()
    {
        // Quit the game
        Application.Quit();
    }

    public void InitHelp()
    {
        // Open Help window in initial game menu
        SplashScreen.gameObject.SetActive(true);
    }
    public void Help1()
    {
        // Open Help window on Player 1 screen
        SplashScreen1.gameObject.SetActive(true);
    }
    public void Help2()
    {
        // Open Help window on Player 2 screen
        SplashScreen2.gameObject.SetActive(true);
    }

    public void StartOrcontinue()
    {
        // Loads game scene from Main Menu
        SceneManager.LoadScene("Main_scene");
    }

    public void InitDiscard()
    {
        // Close Help window on click from main menu
        SplashScreen.gameObject.SetActive(false);
    }
    public void Discard1()
    {
        // Close Help window on click for Player 1
        SplashScreen1.gameObject.SetActive(false);
    }

    public void Discard2()
    {
        // Close Help window on click for Player 1
        SplashScreen2.gameObject.SetActive(false);
    }


    public void End1()
    {
        // End turn for Player 1
        Fader1.gameObject.SetActive(true);
        Fader2.gameObject.SetActive(false);
        P1 = true;
    }


    public void End2()
    {
        // End turn for Player 2
        Fader2.gameObject.SetActive(true);
        Fader1.gameObject.SetActive(false);
        P2 = true;
    }

    void Update()
    {
        // If Player 1 and Player 2 have ended turn. prepare battle phase
        if (P1 && P2)
        {
            Fader1.gameObject.SetActive(false);
            Fader2.gameObject.SetActive(false);
            P1 = false;
            P2 = false;
        }

    }
}
