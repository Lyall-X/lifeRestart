using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class SummaryItem
{
  public int min;
  public string judge;
  public int grade;
}


public class SummaryManager : BaseManager 
{
  private Dictionary<string, List<SummaryItem>> Config = new Dictionary<string, List<SummaryItem>>(); //id , items

  public override void Initialize()
  {
    // 初始化Talents配置表
    SummaryTable summaryTable = ManagerCenter.GetManager<TableManager>().summaryTable;
    foreach (SummaryTableItem item in summaryTable.GetItems())
    {
      SummaryItem t = new SummaryItem();
      t.min = item.min;
      t.judge = item.judge;
      t.grade = item.grade;
      Config[item.type].Add(t);
    }
  }

}