using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Common.Data;

public class TableManager : BaseManager 
{
  public EventsTable eventsTable;
  public AgeTable ageTable;
  public TalentsTable talentsTable;

  public override void Initialize()
  {
    eventsTable = LoadData<EventsTable>("Tables/EventsTable.bytes");
    eventsTable.Initialize();
    
    ageTable = LoadData<AgeTable>("Tables/AgeTable.bytes");
    ageTable.Initialize();

    talentsTable = LoadData<TalentsTable>("Tables/TalentsTable.bytes");
    talentsTable.Initialize();
  }

  public T LoadData<T>(string path) where T : class
  {
    var fullPath = Utils.DataPath + path;
    return SerializeUtil.Deserialize<T>(fullPath);
  }
}