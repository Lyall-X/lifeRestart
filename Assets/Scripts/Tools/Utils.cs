using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class Utils
{
  public static bool CheckCondition(UserDataBase prop,string condition)
  {
    List<string> conditions = Utils.ParseCondition(condition);
    
    if (conditions.Count == 0) return true;

    bool ret = CheckParsedConditions(prop, conditions[0]);
    for(int i = 1; i < conditions.Count; i+=2)
    {
      switch(conditions[i])
      {
        case "&":
        {
          if(ret) ret = CheckParsedConditions(prop, conditions[i+1]);
        }
        break;
        case "|":
        {
          if(ret) return true;
          ret = CheckParsedConditions(prop, conditions[i+1]);
        }
        break;
        default:
          return false;
      }
    }
    return ret;
  }

  public static List<string> ParseCondition(string condition)
  {
    List<string> result = new List<string>();
    string stack = "";
    foreach (char c in condition)
    {
      switch(c)
      {
        case ' ': 
        case '(':
          break;
        case ')':
        {
          stack = "";
          result.Add(stack);
        }
        break;
        case '|':
        case '&':
        {
          result.Add(c.ToString());
        }
        break;
        default:
          stack += c;
          break;
      }
    }
    return result;
  }

  public static bool CheckParsedConditions(UserDataBase prop, string conditions)
  {
    // const length = condition.length;
    // let i = condition.search(/[><\!\?=]/);

    // const prop = condition.substring(0,i);
    // const symbol = condition.substring(i, i+=(condition[i+1]=='='?2:1));
    // const d = condition.substring(i, length);

    // const propData = property.get(prop);
    // const conditionData = d[0]=='['? JSON.parse(d): Number(d);

    // switch(symbol) {
    //     case '>':  return propData >  conditionData;
    //     case '<':  return propData <  conditionData;
    //     case '>=': return propData >= conditionData;
    //     case '<=': return propData <= conditionData;
    //     case '=':
    //         if(Array.isArray(propData))
    //             return propData.includes(conditionData);
    //         return propData == conditionData;
    //     case '!=':
    //         if(Array.isArray(propData))
    //             return !propData.includes(conditionData);
    //         return propData == conditionData;
    //     case '?':
    //         if(Array.isArray(propData)) {
    //             for(const p of propData)
    //                 if(conditionData.includes(p)) return true;
    //             return false;
    //         }
    //         return conditionData.includes(propData);
    //     case '!':
    //         if(Array.isArray(propData)) {
    //             for(const p of propData)
    //                 if(conditionData.includes(p)) return false;
    //             return true;
    //         }
    //         return !conditionData.includes(propData);

    //     default: return false;
    // }
    return true;
  }

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
        return Application.dataPath + "/Resources/";
      }
      var dataDir = Path.GetDirectoryName(Application.dataPath);
      return dataDir + "/PersistentData/";
    }
  }
  public static int Random(int min, int max)
  {
    return UnityEngine.Random.Range(min, max);
  }

  public static float Random(float min, float max)
  {
    return UnityEngine.Random.Range(min, max);
  }

  /// <summary>
  /// 生成一个Key名
  /// </summary>
  public static string GetKey(string key)
  {
    return AppConst.AppPrefix + "_" + key;
  }

  /// <summary>
  /// 取得整型
  /// </summary>
  public static int GetInt(string key)
  {
    string name = GetKey(key);
    return PlayerPrefs.GetInt(name);
  }

  /// <summary>
  /// 有没有值
  /// </summary>
  public static bool HasKey(string key)
  {
    string name = GetKey(key);
    return PlayerPrefs.HasKey(name);
  }

  /// <summary>
  /// 保存整型
  /// </summary>
  public static void SetInt(string key, int value)
  {
    string name = GetKey(key);
    PlayerPrefs.DeleteKey(name);
    PlayerPrefs.SetInt(name, value);
  }

  /// <summary>
  /// 取得数据
  /// </summary>
  public static string GetString(string key)
  {
    string name = GetKey(key);
    return PlayerPrefs.GetString(name);
  }

  /// <summary>
  /// 保存数据
  /// </summary>
  public static void SetString(string key, string value)
  {
    string name = GetKey(key);
    PlayerPrefs.DeleteKey(name);
    PlayerPrefs.SetString(name, value);
  }

  /// <summary>
  /// 删除数据
  /// </summary>
  public static void RemoveData(string key)
  {
    string name = GetKey(key);
    PlayerPrefs.DeleteKey(name);
  }

}
