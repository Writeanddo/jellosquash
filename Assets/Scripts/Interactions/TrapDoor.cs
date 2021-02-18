using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
  public GameObject trapDoor;
  public GameObject ruptured;
  public float offset;
  KeyCode a = KeyCode.M;
    // Start is called before the first frame update
    // Update is called once per frame
  void Update()
  {
    // if player is heavy enuf
    // level up

    if(Input.GetKeyDown(a))
    {
      // level up
      Destroy(trapDoor);
      GetComponent<BoxCollider>().enabled = false;
      GameObject fragments = Instantiate(ruptured, transform.position + transform.localScale.x * transform.right *offset, transform.rotation);
      fragments.transform.localScale = transform.localScale;
    }
  }
}
