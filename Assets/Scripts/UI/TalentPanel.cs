﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using Common.Data;

public static class TalentsView 
{
  public static List<TalentsTableItem> talentItems= new List<TalentsTableItem>();
}

public class TalentPanel : MonoBehaviour
{
  private Button talentBtn;
  private Button startBtn;
  private UIManager uiManager;
  private LoopListView2 superScrollView;
  private UserManager userManager;

  void Start()
  {
    uiManager = ManagerCenter.GetManager<UIManager>();
    
    talentBtn = GameObject.Find("talent").GetComponent<Button>();
    talentBtn.onClick.AddListener(OnTalentClick);
    startBtn = GameObject.Find("start").GetComponent<Button>();
    startBtn.onClick.AddListener(OnStartClick);
    
    superScrollView = (LoopListView2)GameObject.Find("Scroll View").GetComponent("LoopListView2");
    superScrollView.InitListView(TalentsView.talentItems.Count, OnGetItemByIndex);//实例化prefab
    
    userManager = ManagerCenter.GetManager<UserManager>();
    for (int i = 0; i < AppConst.TalentCount; i ++)
    {
      TalentsTableItem item = userManager.TalentRandom();
      TalentsView.talentItems.Add(item);
    }
    SetListItemCount(TalentsView.talentItems.Count);
  }
  

  private void OnTalentClick()
  {
    startBtn.gameObject.SetActive(false);
    GameObject.Find("Scroll View").gameObject.SetActive(true);
  }

  private void OnStartClick()
  {
    string mainPanel = "Prefabs/MainPanel";
    var prefab = uiManager.LoadResAsset<GameObject>(mainPanel);
    if (prefab != null)
    {
      GameObject gameObject;
      gameObject = Instantiate<GameObject>(prefab);
      gameObject.transform.SetParent(uiManager.uiCanvas.transform);
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localScale = Vector3.one;
      gameObject.GetComponent<RectTransform>().offsetMax = new Vector2();
      gameObject.GetComponent<RectTransform>().offsetMin = new Vector2();
      Destroy(gameObject);
    }
  }
  
  LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
  {
    if (index < 0 || index >= TalentsView.talentItems.Count)
    {
      return null;
    }

    TalentsTableItem itemData = TalentsView.talentItems[index];
    if (itemData == null)
    {
      return null;
    }
    
    LoopListViewItem2 item = superScrollView.NewListViewItem("talentItem");
    item.name = index.ToString();
    item.transform.Find("Text").GetComponent<Text>().text = itemData.name + " (" + itemData.description + ") ";
    item.GetComponent<Toggle>().onValueChanged.AddListener((bool isOn) =>
    {
      if (!userManager.userData.m_ext[DefaultProp.TLT].Add(itemData.id))
      {
        Debug.Log("重复添加了");
        return;
      }
        Debug.Log("23222");
    });
    item.transform.Find("Image").GetComponent<Image>().color = AppConst.gradeArray[itemData.grade];
    
    return item;
  }

  void SetListItemCount(int itemCount, bool resetPos = true)
  {
    if (superScrollView != null)
    {
      superScrollView.SetListItemCount(itemCount, resetPos);
    }
  }  
}
