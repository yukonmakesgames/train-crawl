using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField]
    public List<Element> Elements;

    [Header("States")]
    public GameStateType gameState = GameStateType.Chilling;

    private Coroutine refreshCoroutine;

    private void Start()
    {
        refreshCoroutine = StartCoroutine(Refresh(true));
    }

    public void AddElement(ElementController _elementController)
    {
        Elements.Add(new Element(_elementController));
    }

    public void ChangeState(GameStateType _nextGameState, bool _instant = false)
    {
        if(refreshCoroutine != null)
        {
            StopCoroutine(refreshCoroutine);
        }

        gameState = _nextGameState;

        refreshCoroutine = StartCoroutine(Refresh(_instant));
    }

    private IEnumerator Refresh(bool _instant = false)
    {
        //Closing
        Close(_instant);

        bool canContinueAfterClose = false;

        while(!canContinueAfterClose)
        {
            bool elementNotReady = false;

            foreach (Element element in Elements)
            {
                bool nextStateIsAcceptable = false;

                foreach (GameStateType elementGameState in element.ElementController.GameStates)
                {
                    if (elementGameState == gameState)
                    {
                        nextStateIsAcceptable = true;
                        break;
                    }
                }

                if (!element.Closed)
                {
                    if (!nextStateIsAcceptable || !element.ElementController.StayOpen)
                    {
                        elementNotReady = true;
                        break;
                    }
                }
            }

            if(elementNotReady)
            {
                yield return new WaitForEndOfFrame();
            } else
            {
                canContinueAfterClose = true;
            }
        }

        //Updating Elements
        foreach (Element element in Elements)
        {
            foreach (GameStateType elementGameState in element.ElementController.GameStates)
            {
                if (elementGameState == gameState)
                {
                    element.ElementController.UpdateElement();
                    break;
                }
            }
        }

        //Opening
        Open(_instant);
    }

    private void Close(bool _instant = false)
    {
        foreach (Element element in Elements)
        {
            bool nextStateIsAcceptable = false;

            foreach (GameStateType elementGameState in element.ElementController.GameStates)
            {
                if (elementGameState == gameState)
                {
                    nextStateIsAcceptable = true;
                    break;
                }
            }

            if (!element.Closed)
            {
                if (!nextStateIsAcceptable || !element.ElementController.StayOpen)
                {
                    element.ElementController.TriggerClose(_instant);
                }
            }
        }
    }

    private void Open(bool _instant = false)
    {
        foreach (Element element in Elements)
        {
            bool nextStateIsAcceptable = false;

            foreach (GameStateType elementGameState in element.ElementController.GameStates)
            {
                if (elementGameState == gameState)
                {
                    nextStateIsAcceptable = true;
                    break;
                }
            }

            if (element.Closed)
            {
                if (nextStateIsAcceptable)
                {
                    element.ElementController.TriggerOpen(_instant);
                }
            }
        }
    }
}