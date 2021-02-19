using UnityEngine;
using TMPro;
using Cinemachine;

public class SlimeUI : MonoBehaviour
{
  public TextMeshProUGUI massText;

  void Start()
  {
    massText.text = transform.localScale.x.ToString("f1");
  }

  void Update()
  {
    if(Player.dead) massText.enabled = false;
    transform.localScale = new Vector3(
      1.0f/transform.parent.localScale.x,
      1.0f/transform.parent.localScale.y,
      1.0f/transform.parent.localScale.z);

    transform.localPosition = new Vector3(
      transform.localPosition.x,
      10/transform.parent.localScale.y + 1.0f,
      transform.localPosition.z);
    massText.text = transform.parent.localScale.x.ToString("f1");

    transform.LookAt(Camera.main.transform);
  }
}
