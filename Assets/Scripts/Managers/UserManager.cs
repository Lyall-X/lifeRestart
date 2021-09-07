using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class AgeItem
{
  public int age;
  public Dictionary<int, float> events = new Dictionary<int, float>(); // < eventid, random rate>
}

public class UserManager : BaseManager 
{
  private UserDataBase userData = new UserDataBase();
  private List<UserDataBase> recordData = new List<UserDataBase>();

  private Dictionary<int, AgeItem> Config = new Dictionary<int, AgeItem>();

  public override void Initialize()
  {
    // 初始化age配置表
    AgeTable ageTable = ManagerCenter.GetManager<TableManager>().ageTable;
    foreach (AgeTableItem item in ageTable.GetItems())
    {
      if (item != null)
      {
        AgeItem value = new AgeItem();
        value.age = item.id;
        if (item.Events == null)
          continue;
        foreach(string e in item.Events)
        {
          string[] es = e.Split('*');
          if (es.Length == 1)
            value.events[int.Parse(es[0])] = 1;
          else
            value.events[int.Parse(es[0])] = float.Parse(es[1]);
        }
        Config.Add(item.id, value);
      }
    }
  }

  public void Restart()
  {
    userData.m_prop[DefaultProp.AGE] = -1;
    userData.m_prop[DefaultProp.CHR] = 0;
    userData.m_prop[DefaultProp.INT] = 0;
    userData.m_prop[DefaultProp.STR] = 0;
    userData.m_prop[DefaultProp.MNY] = 0;
    userData.m_prop[DefaultProp.SPR] = 0;
    userData.m_prop[DefaultProp.LIF] = 1;

    userData.m_ext[DefaultProp.TLT].Clear();
    userData.m_ext[DefaultProp.EVT].Clear();
    
    recordData.Clear();
  }

  public void Record()
  {
    recordData.Add((UserDataBase)userData.Clone());
  }
  
  public UserDataBase GetLastRecord()
  {
    return recordData[recordData.Count - 1];
  }

  public void ChangeProp(DefaultProp prop, int value)
  {
    switch(prop)
    {
      case DefaultProp.AGE:
      case DefaultProp.CHR:
      case DefaultProp.INT:
      case DefaultProp.STR:
      case DefaultProp.MNY:
      case DefaultProp.SPR:
      case DefaultProp.LIF:
        userData.m_prop[prop] += value;
        break;
      case DefaultProp.TLT:
      case DefaultProp.EVT:
      {
        if (value < 0)
          userData.m_ext[prop].Remove(Math.Abs(value));
        else
          userData.m_ext[prop].Add(value);
      }
      break;
    }
  }

  public void Effect(Dictionary<DefaultProp, int> effects)
  {
    foreach(var e in effects)
    {
      this.ChangeProp(e.Key, e.Value);
    }
  }

  public bool IsEnd()
  {
    return userData.m_prop[DefaultProp.LIF] < 1;
  }
  
  public AgeItem AgeNext()
  {
    this.ChangeProp(DefaultProp.AGE, userData.m_prop[DefaultProp.AGE] + 1);
    return Config[userData.m_prop[DefaultProp.AGE]];
  }
}