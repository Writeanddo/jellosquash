using UnityEngine;
using TMPro;

public class PressurePlateUI : MonoBehaviour
{
  public TextMeshProUGUI massText;
  public PressurePlate pressurePlate;

  void Start()
  {
    massText.text = Mathf.Max(0.0f, (pressurePlate.targetWeight - pressurePlate.currWeight)).ToString("f1");
  }

  void Update()
  {
    massText.text = Mathf.Max(0.0f, (pressurePlate.targetWeight - pressurePlate.currWeight)).ToString("f1");
    transform.LookAt(Camera.main.transform);
  }
}
