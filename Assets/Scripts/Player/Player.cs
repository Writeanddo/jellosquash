using UnityEngine;

public partial class Player : MonoBehaviour
{
  void Start()
  {
    MovementInit();
    SizeInit();
  }

  void Update()
  {
    MovementUpdate();
    SizeUpdate();
  }
}
