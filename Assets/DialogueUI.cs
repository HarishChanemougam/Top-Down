using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button _startConversationButton;
    [SerializeField] UnityEngine.UI.Button _continueButton;
    [SerializeField] UnityEngine.UI.Button _quitButton;
    [SerializeField] TMPro.TextMeshProUGUI _text;

    [SerializeField] UnityEvent _onStartConversation;

    [SerializeField] string[] _texts;

    int _dialogueCursor;

    private void Start()
    {
        _startConversationButton.onClick.AddListener(StartConversation);
        _continueButton.onClick.AddListener(IncreaseCursor);
    }

    void StartConversation()
    {
        _startConversationButton.interactable = false;
        _onStartConversation.Invoke();
        _dialogueCursor = 0;
        ShowDialogue(_dialogueCursor);
    }

    void ShowDialogue(int idx)
    {
        _text.text = _texts[idx];
    }

    void IncreaseCursor()
    {
        if (_dialogueCursor == _texts.Length - 1) return;

        _dialogueCursor++;
        ShowDialogue(_dialogueCursor);
    }


}
