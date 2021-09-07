using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
  private Button loginBtn;
  private UIManager uiManager;

  void Start()
  {
    uiManager = ManagerCenter.GetManager<UIManager>();
    loginBtn = GameObject.Find("start").GetComponent<Button>();
    loginBtn.onClick.AddListener(OnLoginClick);
  }
  
  private void OnLoginClick()
  {
    gameObject.SetActive(false);
    string talentPanel = "Prefabs/TalentPanel";
    var prefab = uiManager.LoadResAsset<GameObject>(talentPanel);
    if (prefab != null)
    {
      GameObject gameObject;
      gameObject = Instantiate<GameObject>(prefab);
      gameObject.transform.SetParent(uiManager.uiCanvas.transform);
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localScale = Vector3.one;
      gameObject.GetComponent<RectTransform>().offsetMax = new Vector2();
      gameObject.GetComponent<RectTransform>().offsetMin = new Vector2();
    }
  }

}
