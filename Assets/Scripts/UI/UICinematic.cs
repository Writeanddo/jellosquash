using UnityEngine;
using System.Collections;
using Cinemachine;
public class UICinematic : MonoBehaviour
{
  // main cam - 10
  public CinemachineVirtualCamera menuCam; //12
  public CinemachineVirtualCamera creditCam; //11
  public GameObject menuPanel;
  KeyCode escape = KeyCode.Escape;
  public GameObject credit;
  public static bool inGame;
  public BgMusic music;
  bool BackToMenu;
  public void PressPlay()
  {
    BackToMenu = true;
    Player.move = true;
    Cursor.visible = false;
    inGame = true;
    menuCam.Priority = 8;
    creditCam.Priority = 7;
  }

  public void PressCredit()
  {
    menuCam.Priority = 7;
  }
  public void CreditOn() => StartCoroutine(CreditActive());
  IEnumerator CreditActive()
  {
    yield return new WaitForSeconds(2f);
    credit.SetActive(true);
  }

  public void BackButton()
  {
    StartCoroutine(QuitGame());
  }
  public IEnumerator QuitGame() // escape
  {
    credit.SetActive(false);
    // move to start menu 
    Player.move = false;
    Cursor.visible = true;
    inGame = false;
    menuCam.Priority = 12;
    creditCam.Priority = 11;
    yield return new WaitForSeconds(2f);
    menuPanel.SetActive(true);
  }
  public void QuitApp()
  {
    print("Quit Game");
    Application.Quit();
  }
  
  void Update()
  {
    if(Input.GetKeyDown(escape))
    {
      //reset all scenes
      if(BackToMenu)
      {
        music.GameExitMusic();
      }
      BackToMenu = false;
      StartCoroutine(QuitGame());
    }

    if (inGame) Cursor.lockState = CursorLockMode.Locked;
    else Cursor.lockState = CursorLockMode.None;
  }
}

// reload a scene
// cursor hide or not