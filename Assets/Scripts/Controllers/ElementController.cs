using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class ElementController : MonoBehaviour
{
    [Header("States")]
    public GameStateType[] GameStates;

    [Header("Properties")]
    public bool StayOpen = false;

    [HideInInspector]
    public UIManager UIManager;
    [HideInInspector]
    public CanvasGroup CanvasGroup;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        UIManager = GetComponentInParent<UIManager>();

        Setup();

        Connect();
    }

    private void Connect()
    {
        if (UIManager != null)
        {
            UIManager.AddElement(this);
        }
        else
        {
            Debug.LogError("There is a UI Element that does not have a reference to UIManager.");
        }
    }

    public abstract void Setup();

    public abstract void UpdateElement();

    public void TriggerClose(bool _instant = false)
    {
        CanvasGroup.interactable = false;
        CanvasGroup.blocksRaycasts = false;

        Close(_instant);
    }

    public abstract void Close(bool _instant = false);

    public void Closed(bool _instant = false)
    {
        UpdateUIManager(true);
    }

    public void TriggerOpen(bool _instant = false)
    {
        UpdateUIManager(false);

        Open(_instant);
    }

    public abstract void Open(bool _instant = false);

    public void Opened(bool _instant = false)
    {
        CanvasGroup.interactable = true;
        CanvasGroup.blocksRaycasts = true;
    }

    public void UpdateUIManager(bool closed)
    {
        foreach(Element element in UIManager.Elements)
        {
            if(element.ElementController == this)
            {
                element.Closed = closed;
                break;
            }
        }
    }
}
