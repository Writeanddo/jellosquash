using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameUI : MonoBehaviour
{
  public TextMeshProUGUI massText;
  public Image JellyPng;
  public int size;
  private float currentScale;
  private float nextScale;
  private float movingScale;
  public float scaleLerpSpeed;
  public float scaleAdd;
  public float minScale, maxScale;

  void Start()
  {
    currentScale = JellyPng.rectTransform.localScale.x;
    currentScale = JellyPng.rectTransform.localScale.y;
    nextScale = currentScale;
  }

  void Update()
  {
    massText.text = size.ToString();
    currentScale = Mathf.Lerp(currentScale, nextScale, scaleLerpSpeed);
    JellyPng.rectTransform.localScale = new Vector3(currentScale, currentScale, 0);
    if(Input.GetKey("space"))
    {
      ScaleUp();
    }

    // scale up when player size increase in game
    //put image change in range so png wont expand out of UI
  }

  private void ScaleDown()
  {
    size -= 10;
    nextScale = currentScale - scaleAdd;
  }
  private void ScaleUp()
  {
    size += 10;
    nextScale = currentScale + scaleAdd;
  }
}
