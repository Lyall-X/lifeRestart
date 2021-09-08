using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class UserManager : BaseManager 
{
  public UserDataBase userData = new UserDataBase();
  public List<UserDataBase> recordData = new List<UserDataBase>();

  AgeManager ageManager;
  EventManager eventManager;
  TalentManager talentManager;
  SummaryManager summaryManager;

  public override void Initialize()
  {
    ageManager = ManagerCenter.GetManager<AgeManager>();
    eventManager = ManagerCenter.GetManager<EventManager>();
    talentManager = ManagerCenter.GetManager<TalentManager>();
    summaryManager = ManagerCenter.GetManager<SummaryManager>();
  }

  public TalentsTableItem TalentRandom()
  {
    return talentManager.TalentRandom();
  }
  
  public int Exclusive(HashSet<int> talents, int itemid)
  {
    return talentManager.Exclusive(talents, itemid);
  }

  public void Restart()
  {
    ageManager.Restart(userData);
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
  
  // public AgeItem AgeNext()
  // {
  //   this.ChangeProp(DefaultProp.AGE, userData.m_prop[DefaultProp.AGE] + 1);
  //   return Config[userData.m_prop[DefaultProp.AGE]];
  // }
}