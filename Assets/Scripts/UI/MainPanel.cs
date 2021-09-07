using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperScrollView;

public static class LifeView 
{
  public static List<LifeItemInfo> lifeItems= new List<LifeItemInfo>();
}

public class MainPanel : MonoBehaviour
{
  private LoopListView2 superScrollView;
  // Start is called before the first frame update
  void Start()
  {
    superScrollView = (LoopListView2)GameObject.Find("Scroll View").GetComponent("LoopListView2");
    superScrollView.InitListView(LifeView.lifeItems.Count, OnGetItemByIndex);//实例化prefab
    
    this.OnJumpBtnClicked(0);//调到顶置位置
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
    LoopListViewItem2 item = listView.NewListViewItem("LifeItem");
    LifeData itemScript = item.GetComponent<LifeData>();
    // if (item.IsInitHandlerCalled == false)
    // {
      
      item.IsInitHandlerCalled = true;
      itemScript.OnClick += OnClick;
      itemScript.OnEnter += OnEnter;
      itemScript.OnExit += OnExit;
    // }
    return item;
  }

  void OnJumpBtnClicked(int i)
  {
    if (i < 0)
      return;
    superScrollView.MovePanelToItemIndex(i, 0);
  }

  private void  OnClick(LifeData item)
  {
    Debug.Log("点击！！");
  }
  private void OnEnter(LifeData item)
  {
    Debug.Log("移入！！");
  }
  private void OnExit(LifeData item)
  {
    Debug.Log("移出！！");
  }
}
