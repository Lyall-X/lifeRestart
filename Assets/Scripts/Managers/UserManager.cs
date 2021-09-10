using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class UserManager : BaseManager 
{
  public UserDataBase userData = new UserDataBase();
  public List<UserDataBase> recordData = new List<UserDataBase>();

  public AgeManager ageManager;
  public EventManager eventManager;
  public TalentManager talentManager;
  public SummaryManager summaryManager;

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

  public void Next()
  {
    userData.m_prop[DefaultProp.AGE] += 1;
    List<int> ageList = ageManager.Config[userData.m_prop[DefaultProp.AGE]];
    if (ageList.Count == 0) Next();
    int index = Utils.Random(0, ageList.Count);
    userData.m_event[DefaultProp.EVT].Add(ageList[index]);

    EventsTableItem value;
    if (eventManager.Config.TryGetValue(ageList[index], out value) && value.id != 0)
    {
      userData.m_prop[DefaultProp.CHR] += value.CHR;
      userData.m_prop[DefaultProp.INT] += value.INT;
      userData.m_prop[DefaultProp.STR] += value.STR;
      userData.m_prop[DefaultProp.MNY] += value.MNY;
      userData.m_prop[DefaultProp.SPR] += value.SPR;
      userData.m_prop[DefaultProp.LIF] += value.LIF;
    }
  }
}