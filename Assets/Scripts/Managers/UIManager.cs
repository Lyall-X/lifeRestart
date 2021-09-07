using UnityEngine;
using System.IO;
using UObject = UnityEngine.Object;

public class UIManager : BaseManager 
{
  private Canvas _uiCanvas;
  protected Canvas uiCanvas
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
    string loginPanel = "Prefabs/LoginPanel";
    var prefab = this.LoadResAsset<GameObject>(loginPanel);
    if (prefab != null)
    {
      GameObject gameObject;
      gameObject = Instantiate<GameObject>(prefab);
      gameObject.name = "LoadingPanel";
      gameObject.transform.SetParent(uiCanvas.transform);
      gameObject.transform.localPosition = Vector3.zero;
      gameObject.transform.localScale = Vector3.one;
      gameObject.GetComponent<RectTransform>().offsetMax = new Vector2();
      gameObject.GetComponent<RectTransform>().offsetMin = new Vector2();
    }
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