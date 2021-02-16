using UnityEngine;

public partial class Player : MonoBehaviour
{
  void Start()
  {
    SizeInit();
  }

  void Update()
  {
    MovementUpdate();
    SizeUpdate();
  }
}
