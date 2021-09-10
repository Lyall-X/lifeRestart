using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperScrollView;
using UnityEngine.UI;
using Common.Data;

public class LifeItemInfo
{
  public string m_name;
}

public static class LifeView 
{
  public static List<LifeItemInfo> lifeItems= new List<LifeItemInfo>();
}

public class MainPanel : MonoBehaviour
{
  private LoopListView2 superScrollView;
  private Button nextBtn;
  private UserManager userManager;

  void Start()
  {
    nextBtn = GameObject.Find("nextBtn").GetComponent<Button>();
    nextBtn.onClick.AddListener(OnNextClick);

    superScrollView = (LoopListView2)GameObject.Find("Scroll View").GetComponent("LoopListView2");
    superScrollView.InitListView(LifeView.lifeItems.Count, OnGetItemByIndex);//实例化prefab
    LifeView.lifeItems.Clear();
    userManager = ManagerCenter.GetManager<UserManager>();
    
    GameObject.Find("restart").GetComponent<Button>().onClick.AddListener(()=>{
      userManager.userData.times ++;
      ManagerCenter.GetManager<UIManager>().ShowUI("LoginPanel");
      Destroy(gameObject);
    });
  }

  void OnDestroy() {
    LifeView.lifeItems.Clear();
    this.SetListItemCount(0);
  }
  
  private void OnNextClick(){
    userManager.Next();
    LifeItemInfo i = new LifeItemInfo();
    int eventid = userManager.userData.m_event[DefaultProp.EVT][userManager.userData.m_event[DefaultProp.EVT].Count - 1];
    i.m_name = userManager.eventManager.Config[eventid].Event;
    LifeView.lifeItems.Add(i);
    SetListItemCount(LifeView.lifeItems.Count);
    MoveToItemIndex(LifeView.lifeItems.Count - 1);

    for(int j = 0 ; j < 6; j ++)
    {
      GameObject.Find("node").transform.Find("node_"+ j).GetComponent<Text>().text = userManager.userData.m_prop[(DefaultProp)j].ToString();
    }
  }

  LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
  {
    if (index < 0 || index >= LifeView.lifeItems.Count)
    {
      return null;
    }

    LifeItemInfo itemData = LifeView.lifeItems[index];
    if (itemData == null)
    {
      return null;
    }
    
    LoopListViewItem2 item = superScrollView.NewListViewItem("lifeitem");
    item.name = index.ToString();
    item.transform.Find("Text").GetComponent<Text>().text = "第" + index + "岁 : " + itemData.m_name;
    return item;
  }
  void SetListItemCount(int itemCount, bool resetPos = true)
  {
    if (superScrollView != null)
    {
      superScrollView.SetListItemCount(itemCount, resetPos);
    }
  }  

  void MoveToItemIndex(int index)
  {
    if (superScrollView != null)
    {
      superScrollView.MovePanelToItemIndex(index, 0);
    }
  }
}
