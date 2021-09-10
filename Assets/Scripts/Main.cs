using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Data;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Main : MonoBehaviour
{
  public TextAsset eventsTable;
  public TextAsset ageTable;
  public TextAsset talentsTable;
  public TextAsset summaryTable;

   void Awake()
  {
    
    ManagerCenter.AddManager<TableManager>();
    ManagerCenter.AddManager<AgeManager>();
    ManagerCenter.AddManager<EventManager>();
    ManagerCenter.AddManager<TalentManager>();
    ManagerCenter.AddManager<SummaryManager>();

    var eventsStream = new MemoryStream(eventsTable.bytes);
    ManagerCenter.GetManager<TableManager>().eventsTable = SerializeUtil.Deserialize<EventsTable>(eventsStream);
    
    var ageStream = new MemoryStream(ageTable.bytes);
    ManagerCenter.GetManager<TableManager>().ageTable = SerializeUtil.Deserialize<AgeTable>(ageStream);
    
    var talentsStream = new MemoryStream(talentsTable.bytes);
    ManagerCenter.GetManager<TableManager>().talentsTable = SerializeUtil.Deserialize<TalentsTable>(talentsStream);
    
    var summaryStream = new MemoryStream(summaryTable.bytes);
    ManagerCenter.GetManager<TableManager>().summaryTable = SerializeUtil.Deserialize<SummaryTable>(summaryStream);

    ManagerCenter.InitManager();
  }

  void Start()
  {
    
  }

  void Update()
  {
      
  }
}
