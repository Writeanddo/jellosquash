using UnityEngine;
using Cinemachine;
public class UICinematic : MonoBehaviour
{
  public CinemachineVirtualCamera menuCam; //12
  public CinemachineVirtualCamera creditCam; //11
    // Start is called before the first frame update

    // Update is called once per frame
  public void PressPlay()
  {
    menuCam.Priority = 1;
  }
  
  public void PressCredit()
  {
    creditCam.Priority = 1;
  }

  //test
  // void Update()
  // {
  //   if(Input.GetKey("space"))
  //   {
  //     menuCam.Priority = 1;
  //   }
  // }
}
