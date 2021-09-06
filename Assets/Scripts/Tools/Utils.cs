using UnityEngine;
using System.IO;

public class Utils
{
  /// <summary>
  /// 取得数据存放目录
  /// </summary>
  public static string DataPath
  {
    get
    {
      if (Application.isMobilePlatform)
      {
        return Application.persistentDataPath + "/" + AppConst.AppName + "/";
      }
      if (AppConst.DebugMode)
      {
        return Application.dataPath + "/res/";
      }
      var dataDir = Path.GetDirectoryName(Application.dataPath);
      return dataDir + "/PersistentData/";
    }
  }
}
