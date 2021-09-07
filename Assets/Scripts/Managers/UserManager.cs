using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class UserManager : BaseManager 
{
  public class UserDataBase
  {
    public long times = 0;
    public Dictionary<defaultProp, int> prop = new Dictionary<defaultProp, int>();
  }

  public override void Initialize()
  {
    
  }
  
}