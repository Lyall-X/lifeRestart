using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class TalentManager : BaseManager 
{
  private Dictionary<int, TalentsTableItem> Config = new Dictionary<int, TalentsTableItem>();

  public override void Initialize()
  {
    // 初始化Talents配置表
    TalentsTable talentsTable = ManagerCenter.GetManager<TableManager>().talentsTable;
    foreach (TalentsTable item in talentsTable.GetItems())
    {
      if (item != null)
      {
        Config.Add(item.id, item);
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
}