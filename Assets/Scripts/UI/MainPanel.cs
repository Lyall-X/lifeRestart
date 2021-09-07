using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperScrollView;
using UnityEngine.UI;

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

  void Start()
  {
    nextBtn = GameObject.Find("next").GetComponent<Button>();
    nextBtn.onClick.AddListener(OnNextClick);

    superScrollView = (LoopListView2)GameObject.Find("Scroll View").GetComponent("LoopListView2");
    superScrollView.InitListView(LifeView.lifeItems.Count, OnGetItemByIndex);//实例化prefab
  }

  void OnDestroy() {
    LifeView.lifeItems.Clear();
    this.SetListItemCount(0);
  }
  
  private void OnNextClick(){
    LifeView.lifeItems.Add(new LifeItemInfo());
    SetListItemCount(LifeView.lifeItems.Count);
    MoveToItemIndex(LifeView.lifeItems.Count - 1);
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
    
    LoopListViewItem2 item = superScrollView.NewListViewItem("LifeItem");
    LifeData itemScript = item.GetComponent<LifeData>();
    itemScript.initData(itemData);
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
