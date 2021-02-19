using UnityEngine;
using Cinemachine;
public class UICinematic : MonoBehaviour
{
  // main cam - 10
  public CinemachineVirtualCamera menuCam; //12
  public CinemachineVirtualCamera creditCam; //11
    // Start is called before the first frame update
  public GameObject menuPanel;
  public KeyCode escape = KeyCode.Escape;

  public static bool inGame;

  public void PressPlay()
  {
    Player.move = true;
    Cursor.visible = false;
    inGame = true;
    menuCam.Priority = 9;
    creditCam.Priority = 8;
  }
  
  public void PressCredit()
  {
    menuCam.Priority = 7;
  }

  public void QuitGame() // escape
  {
    // move to start menu 
    Player.move = false;
    Cursor.visible = true;
    inGame = false;
    menuCam.Priority = 12;
    creditCam.Priority = 11;
    menuPanel.SetActive(true);
    // switch music back to menuMusic
  }

  public void QuitApp()
  {
    print("Quit Game");
    Application.Quit();
  }
  //test
  void Update()
  {
    if(Input.GetKey(escape))
    {
      //save checkpoint
      // print("Quit!");
      QuitGame();
    }

    if (inGame) Cursor.lockState = CursorLockMode.Locked;
    else Cursor.lockState = CursorLockMode.None;
  }
}

// reload a scene
// cursor hide or not