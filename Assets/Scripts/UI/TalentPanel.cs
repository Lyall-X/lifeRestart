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
    startBtn = GameObject.Find("starBtn").GetComponent<Button>();
    startBtn.onClick.AddListener(OnStartClick);
    
    superScrollView = (LoopListView2)GameObject.Find("Scroll View").GetComponent("LoopListView2");
    superScrollView.InitListView(TalentsView.talentItems.Count, OnGetItemByIndex);//实例化prefab
    TalentsView.talentItems.Clear();
    userManager = ManagerCenter.GetManager<UserManager>();
    for (int i = 0; i < AppConst.TalentCount; i ++)
    {
      TalentsTableItem item = userManager.TalentRandom();
      TalentsView.talentItems.Add(item);
    }
    SetListItemCount(TalentsView.talentItems.Count);
  }

  public void OnStartClick()
  {
    if (userManager.userData.m_ext[DefaultProp.TLT].Count < AppConst.TalentSelectCount)
    {
      uiManager.ShowTips("请选" + AppConst.TalentSelectCount + "个天赋");
      return;
    }
    uiManager.ShowUI("PropPanel");
    DestroyImmediate(gameObject);
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
    Toggle toggle = item.GetComponent<Toggle>();
    toggle.onValueChanged.AddListener((bool isOn) =>
    {
      // 查找独有天赋
      int exclusivId = userManager.Exclusive(userManager.userData.m_ext[DefaultProp.TLT], itemData.id);
      if (exclusivId != 0)
      {
        toggle.isOn = false;
        uiManager.ShowTips("与已选择的天赋[" + ManagerCenter.GetManager<TalentManager>().Config[exclusivId].name + "]冲突!");
        return;
      }
      // 避免天赋重复
      if (isOn)
      {
        if (userManager.userData.m_ext[DefaultProp.TLT].Count >= AppConst.TalentSelectCount)
        {
          toggle.isOn = false;
          uiManager.ShowTips("只能选" + AppConst.TalentSelectCount + "个天赋!");
        }
        if(!userManager.userData.m_ext[DefaultProp.TLT].Add(itemData.id))
        {
          toggle.isOn = false;
          uiManager.ShowTips("天赋重复!");
        }
      }
      else
      {
        userManager.userData.m_ext[DefaultProp.TLT].Remove(itemData.id);
      }
      startBtn.transform.Find("Text").GetComponent<Text>().text = (userManager.userData.m_ext[DefaultProp.TLT].Count == AppConst.TalentSelectCount) ? "开启新人生" : "请选择" + AppConst.TalentSelectCount +  "个";
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
