using System;
using System.Collections.Generic;
using UnityEngine;
using Common.Data;

public enum DefaultProp
{       
  CHR,      // 颜值 charm CHR
  INT,      // 智力 intelligence INT
  STR,      // 体质 strength STR
  MNY,      // 家境 money MNY

  SPR,      // 快乐 spirit SPR
  LIF,      // 生命 life LIF
  AGE,      // 年龄
  
  TLT,      // 天赋 talent TLT
  EVT,      // 事件 event EVT
}


public class UserDataBase : ICloneable
{
  public int times = 0;
  // 属性
  public Dictionary<DefaultProp, int> m_prop = new Dictionary<DefaultProp, int>();
  public Dictionary<DefaultProp, HashSet<int>> m_ext = new Dictionary<DefaultProp, HashSet<int>>();
  public Dictionary<DefaultProp, List<int>> m_event = new Dictionary<DefaultProp, List<int>>();

  public object Clone()
  {
    return this.MemberwiseClone();
  }
}