using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;


public class EventManager : BaseManager 
{
  public Dictionary<int, EventsTableItem> Config = new Dictionary<int, EventsTableItem>(); //id , items

  public override void Initialize()
  {
    // 初始化Talents配置表
    EventsTable eventsTable = ManagerCenter.GetManager<TableManager>().eventsTable;
    foreach (EventsTableItem item in eventsTable.GetItems())
    {
      Config.Add(item.id, item);
    }
  }

  public bool Check(int eventId, UserDataBase prop)
  {
    EventsTableItem item = Config[eventId];
    if(item.NoRandom == 1) return false;
    if (item.exclude != "" && Utils.CheckCondition(prop, item.exclude) ) return false;
    if (item.include != "") return Utils.CheckCondition(prop, item.include);
    return true;
  }
}