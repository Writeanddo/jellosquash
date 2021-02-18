using UnityEngine;
using TMPro;
public class SlimeUI : MonoBehaviour
{
  public TextMeshProUGUI massText;

  void Start()
  {
    massText.text = transform.lossyScale.x.ToString("f1");
  }

  void Update()
  {
    transform.localScale = new Vector3(
      1.0f/transform.parent.localScale.x,
      1.0f/transform.parent.localScale.y,
      1.0f/transform.parent.localScale.z);
    massText.text = transform.parent.localScale.x.ToString("f1");
  }
}
