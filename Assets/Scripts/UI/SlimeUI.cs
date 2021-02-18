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
    massText.text = transform.lossyScale.x.ToString("f1");
  }
}
