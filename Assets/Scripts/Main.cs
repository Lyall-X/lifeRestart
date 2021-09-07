using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Data;

public class Main : MonoBehaviour
{
   void Awake()
  {
    ManagerCenter.InitManager();
  }

  void Start()
  {
    TableManager events = ManagerCenter.GetManager<TableManager>();
    List<AgeTableItem> aa = events.ageTable.GetItems();
    Debug.Log(aa.Count);
    
    List<EventsTableItem> bb = events.eventsTable.GetItems();
    Debug.Log(bb.Count);
    
    List<TalentsTableItem> cc = events.talentsTable.GetItems();
    Debug.Log(cc.Count);
  }

  void Update()
  {
      
  }
}
