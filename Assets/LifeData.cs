using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LifeItemInfo
{
  public string m_name;
  /// <summary>
  /// 被选中点击
  /// </summary>
  public bool OnClick;
}

public class LifeData : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

  public Action<LifeData> OnClick;
  public Action<LifeData> OnEnter;
  public Action<LifeData> OnExit;

  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
  {
    if(this.gameObject!=null) OnClick(this);
  }

  void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
  {
    if (this.gameObject != null) OnEnter(this);
  }

  void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
  {
    if (this.gameObject != null) OnExit(this);
  }
}
