using UnityEngine;
using System.IO;
using UObject = UnityEngine.Object;
using UnityEngine.UI;

public class UIManager : BaseManager 
{
  private Canvas _uiCanvas;
  public Canvas uiCanvas
  {
    get
    {
      if (_uiCanvas == null)
      {
        _uiCanvas = GameObject.Find("/MainGame/UICanvas").GetComponent<Canvas>();
      }
      return _uiCanvas;
    }
  }

  public override void Initialize()
  {
    this.ShowUI("LoginPanel");
  }

  public GameObject ShowUI(string uiname)
  {
    string Panel = "Prefabs/" + uiname;
    var prefab = this.LoadResAsset<GameObject>(Panel);
    if (prefab != null)
    {
      GameObject gameObject;
      gameObject = Instantiate<GameObject>(prefab);
      gameObject.transform.SetParent(uiCanvas.transform);
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localScale = Vector3.one;
      gameObject.GetComponent<RectTransform>().offsetMax = new Vector2();
      gameObject.GetComponent<RectTransform>().offsetMin = new Vector2();
      return gameObject;
    }
    return null;
  }

  public void ShowTips(string content)
  {
    GameObject o = this.ShowUI("TipsPanel");
    o.transform.Find("#prefab_item/Text").GetComponent<Text>().text = content;
    GameObject.Destroy(o, 1.1f);
  }

  public T LoadResAsset<T>(string path) where T : UObject
  {
    UObject o = Resources.Load<T>(path);
    if (o == null)
    {
      return null;
    }
    return o as T;
  }

  public T Instantiate<T>(T original) where T : UnityEngine.Object
  {
    return GameObject.Instantiate<T>(original);
  }
}