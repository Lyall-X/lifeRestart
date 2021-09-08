using UnityEngine;

public class AppConst
{
  public const string AppName = "LifeRestart";                   //应用程序名称
  public const string AppPrefix = AppName + "_";                 //应用程序前缀
  public const bool DebugMode = true;
  public const int TalentCount = 10;                             // 天赋供选择个数
  public const int TalentSelectCount = 3;                        // 天赋可选

  public static Color32[] gradeArray = {new Color32(70,70,70, 255), new Color32(100,149,237, 255), new Color32(226,167,255, 255), new Color32(255,160,122, 255)};
}