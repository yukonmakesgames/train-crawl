using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class JuiceElementController : ElementController
{
    [Header("References")]
    [SerializeField]
    private Image healthFill;
    [SerializeField]
    private PlayerController playerController;

    [Header("Tweening")]
    [SerializeField]
    private float easeTime = 1f;
    [SerializeField]
    private Ease openEase;
    [SerializeField]
    private Ease closeEase;

    [Header("Positions")]
    [SerializeField]
    private Vector2 openedPosition;
    [SerializeField]
    private Vector2 closedPosition;

    private RectTransform rectTransform;

    void Update()
    {
        if (playerController != null)
        {
            healthFill.fillAmount = playerController.juice;
        }
    }

    public override void Close(bool _instant = false)
    {
        rectTransform.DOKill();

        if(_instant)
        {
            rectTransform.anchoredPosition = closedPosition;
            Closed(_instant);
        }
        else
        {
            rectTransform.DOAnchorPos(closedPosition, easeTime).SetEase(closeEase).OnComplete(() => Closed(_instant));
        }
    }

    public override void Open(bool _instant = false)
    {
        rectTransform.DOKill();

        if (_instant)
        {
            rectTransform.anchoredPosition = openedPosition;
            Opened(_instant);
        }
        else
        {
            rectTransform.DOAnchorPos(openedPosition, easeTime).SetEase(openEase).OnComplete(() => Opened(_instant));
        }
    }

    public override void Setup()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public override void UpdateElement()
    {
        
    }
}
