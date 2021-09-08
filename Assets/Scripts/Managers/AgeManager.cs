using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class AgeManager : BaseManager 
{
  private Dictionary<int, Dictionary<int, float>> Config = new Dictionary<int, Dictionary<int, float>>(); // age, <eventid, rate>

  public override void Initialize()
  {
    // 初始化age配置表
    AgeTable ageTable = ManagerCenter.GetManager<TableManager>().ageTable;
    foreach (AgeTableItem item in ageTable.GetItems())
    {
      if (item != null && item.Events != null)
      {
        Dictionary<int, float> value = new Dictionary<int, float>();
        foreach(string e in item.Events)
        {
          string[] es = e.Split('*');
          if (es.Length == 1)
            value[int.Parse(es[0])] = 1;
          else
            value[int.Parse(es[0])] = float.Parse(es[1]);
        }
        Config.Add(item.id, value);
      }
    }
  }

  public void Restart(UserDataBase userData)
  {
    userData.m_prop[DefaultProp.AGE] = -1;
    userData.m_prop[DefaultProp.CHR] = 0;
    userData.m_prop[DefaultProp.INT] = 0;
    userData.m_prop[DefaultProp.STR] = 0;
    userData.m_prop[DefaultProp.MNY] = 0;
    userData.m_prop[DefaultProp.SPR] = 0;
    userData.m_prop[DefaultProp.LIF] = 1;

    userData.m_ext[DefaultProp.TLT] = new HashSet<int>();
    userData.m_ext[DefaultProp.EVT] = new HashSet<int>();
  }

  public void ChangeProp(UserDataBase userData, int value)
  {
    foreach(var prop in userData.m_prop)
    {
      userData.m_prop[prop.Key] += value;
    }
    foreach(var ext in userData.m_ext)
    {
      if (value < 0)
        userData.m_ext[ext.Key].Remove(Math.Abs(value));
      else
        userData.m_ext[ext.Key].Add(value);
    }
  }

  // public void Effect(Dictionary<DefaultProp, int> effects)
  // {
  //   foreach(var e in effects)
  //   {
  //     this.ChangeProp(e.Key, e.Value);
  //   }
  // }

  // public bool IsEnd()
  // {
  //   return userData.m_prop[DefaultProp.LIF] < 1;
  // }
  
  // public AgeItem AgeNext()
  // {
  //   this.ChangeProp(DefaultProp.AGE, userData.m_prop[DefaultProp.AGE] + 1);
  //   return Config[userData.m_prop[DefaultProp.AGE]];
  // }
  
}