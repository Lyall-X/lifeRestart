using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
  private UIManager uiManager;
  private UserManager userManager;

  void Start()
  {
    uiManager = ManagerCenter.GetManager<UIManager>();
    GameObject.Find("start").GetComponent<Button>().onClick.AddListener(()=>{
      uiManager.ShowUI("TalentPanel");
      Destroy(gameObject);
    });
    userManager = ManagerCenter.GetManager<UserManager>();
    userManager.Restart();
    
    GameObject.Find("title").GetComponent<Text>().text = "已重开" + userManager.userData.times + "次";
  }
}
