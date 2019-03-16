using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DeathElementController : ElementController
{
    [Header("Tweening")]
    [SerializeField]
    private float easeTime = 1f;
    [SerializeField]
    private Ease openEase;
    [SerializeField]
    private Ease closeEase;

    [Header("Text")]
    [SerializeField]
    private Text carCountText;
    [SerializeField]
    [TextArea]
    private string carStringPrefix;
    [SerializeField]
    [TextArea]
    private string carStringSuffix;

    public override void Close(bool _instant = false)
    {
        CanvasGroup.DOKill();

        if(_instant)
        {
            CanvasGroup.alpha = 0f;
            Closed(_instant);
        }
        else
        {
            CanvasGroup.DOFade(0f, easeTime).SetEase(closeEase).OnComplete(() => Closed(_instant));
        }
    }

    public override void Open(bool _instant = false)
    {
        CanvasGroup.DOKill();

        if (_instant)
        {
            CanvasGroup.alpha = 1f;
            Opened(_instant);
        }
        else
        {
            CanvasGroup.DOFade(1f, easeTime).SetEase(openEase).OnComplete(() => Opened(_instant));
        }
    }

    public override void Setup()
    {
        
    }

    public override void UpdateElement()
    {
        carCountText.text = carStringPrefix + GameManager.Instance.CarNumber + carStringSuffix;
    }

    private void Update()
    {
        if(UIManager.gameState == GameStateType.IdkProbablyDead)
        {
            if(Input.GetButtonDown("Attack"))
            {
                GameManager.Instance.Restart(gameObject.scene);
            }

            if(Input.GetButtonDown("Cancel"))
            {
                //TODO Go to menu.
            }
        }
    }
}
