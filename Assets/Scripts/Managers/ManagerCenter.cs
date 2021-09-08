using System;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCenter
{
  static Dictionary<string, BaseManager> Managers = new Dictionary<string, BaseManager>();

  /// <summary>
  /// 初始化管理器
  /// </summary>
  public static void InitManager()
  {
    AddManager<TableManager>();
    AddManager<UIManager>();
    
    AddManager<AgeManager>();
    AddManager<UserManager>();
    AddManager<EventManager>();
    AddManager<TalentManager>();
    AddManager<SummaryManager>();
    
    foreach (var mgr in Managers)
    {
      if (mgr.Value != null)
      {
        mgr.Value.Initialize();
      }
    }
  }


  static T AddManager<T>() where T : BaseManager, new()
  {
    var type = typeof(T);
    var obj = new T();
    Managers.Add(type.Name, obj);
    return obj;
  }

  public static T GetManager<T>() where T : class
  {
    var type = typeof(T);
    if (!Managers.ContainsKey(type.Name))
    {
      return null;
    }
    return Managers[type.Name] as T;
  }
}