using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LifeData : MonoBehaviour
{

  private Text index;
  void Start()
  {
    index = GameObject.Find("text").GetComponent<Text>();
  }
  
  public void initData(LifeItemInfo info)
  {
    // index = 
  }
}
