using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PropPanel : MonoBehaviour
{
  private UIManager uiManager;
  private UserManager userManager;
  private int propMax = 20;

  void Start()
  {
    uiManager = ManagerCenter.GetManager<UIManager>();
    userManager = ManagerCenter.GetManager<UserManager>();
    GameObject.Find("titleText").GetComponent<Text>().text = "可用属性点：" + propMax;
    GameObject.Find("btn_start").GetComponent<Button>().onClick.AddListener(()=>{
      if (propMax > 0)
      {
        uiManager.ShowTips("你还有" + propMax + "属性点没有分配完");
        return;
      }
      uiManager.ShowUI("MainPanel");
      Destroy(gameObject);
    });
    GameObject.Find("btn_random").GetComponent<Button>().onClick.AddListener(()=>{
      propMax = 20;
      int t = 20;
      int[] arr = {10, 10, 10, 10};
      while(t > 0)
      {
        int sub = Utils.Random(0, Math.Min(t, 10)) + 1;
        while(true)
        {
          int select = Utils.Random(0, 4);
          if (arr[select] - sub < 0) continue;
          arr[select] -= sub;
          t -= sub;
          break;
        }
      }
      for(int i = 0 ; i < arr.Length; i ++)
      {
        userManager.userData.m_prop[(DefaultProp)i] = 0;
        arr[i] = 10 - arr[i];
        ChangeProp((DefaultProp)i, arr[i]);
      }
    });

    for(int i = 0 ; i < 4; i ++)
    {
      int index = i;
      GameObject.Find("node").transform.Find("node_"+ i).Find("btn_del").GetComponent<Button>().onClick.AddListener(()=>{
        if (userManager.userData.m_prop[(DefaultProp)index] > 0 )
        {
          ChangeProp((DefaultProp)index, -1);
        }
      });
      GameObject.Find("node").transform.Find("node_"+ i).Find("btn_add").GetComponent<Button>().onClick.AddListener(()=>{
        if (propMax <=0 )
        {
          uiManager.ShowTips("剩余天赋不足!");
          return;
        }
        ChangeProp((DefaultProp)index, 1);
      });
    }
  }

  void ChangeProp(DefaultProp prop, int value)
  {
    propMax -= value;
    GameObject.Find("titleText").GetComponent<Text>().text = "可用属性点：" + propMax;
    userManager.userData.m_prop[prop] += value;
    GameObject.Find("node").transform.Find("node_"+ (int)prop).Find("text_value").GetComponent<Text>().text = userManager.userData.m_prop[prop].ToString();
  }
}
