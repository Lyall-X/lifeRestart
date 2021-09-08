using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class TalentManager : BaseManager 
{
  private Dictionary<int, TalentsTableItem> Config = new Dictionary<int, TalentsTableItem>(); //id , items
  private Dictionary<int, List<TalentsTableItem>> gradeConfig = new Dictionary<int, List<TalentsTableItem>>(); // grade, items

  public override void Initialize()
  {
    // 初始化Talents配置表
    TalentsTable talentsTable = ManagerCenter.GetManager<TableManager>().talentsTable;
    foreach (TalentsTableItem item in talentsTable.GetItems())
    {
      if (item != null)
      {
        Config.Add(item.id, item);
        if (!gradeConfig.ContainsKey(item.grade))
          gradeConfig.Add(item.grade, new List<TalentsTableItem>());;
        gradeConfig[item.grade].Add(item);
      }
    }
  }
  
  public bool Check(int talentId, UserDataBase prop)
  {
    if (Config.ContainsKey(talentId))
    {
      return Utils.CheckCondition(prop, Config[talentId].condition);
    }
    return false;
  }

  public TalentsTableItem TalentRandom()
  {
    float random = Utils.Random(0f, 1f);
    int grade = 0;
    if (random >= 0.11)
      grade = 0;
    else if (random >=0.011)
      grade = 1;
    else if (random >=0.001)
      grade = 2;
    else
      grade = 3;
      
    while(gradeConfig[grade].Count == 0) grade--;
    int index = Utils.Random(0, gradeConfig[grade].Count - 1);
    return gradeConfig[grade][index];
  }

  public int Exclusive(HashSet<int> talents, int itemid)
  {
    if (!Config.ContainsKey(itemid) || Config[itemid].exclusives.Length == 0) return 0;
    foreach(var talent in talents)
    {
      foreach(var e in Config[itemid].exclusives)
      {
        if (talent == e)
        {
          return talent;
        }
      }
    }
    return 0;
  }
}