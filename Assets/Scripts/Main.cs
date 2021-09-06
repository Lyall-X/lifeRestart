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
    // Dictionary<int, EventsTableItem> aa = events.eventsTable;
    // Debug.Log(aa.Count);
  }

  void Update()
  {
      
  }
}
