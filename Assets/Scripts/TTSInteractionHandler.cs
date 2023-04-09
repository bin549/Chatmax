using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.WitAi;
using Meta.WitAi.Json;

using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class TTSInteractionHandler : MonoBehaviour
{
    [Header("Default States"), Multiline]
    [SerializeField] private string freshStateText = "Try to say something.";

    [Header("UI")]
    [SerializeField] private TMP_Text textArea;

    [SerializeField] private bool showJson;

    [Header("Voice")]
    [SerializeField] private Oculus.Voice.AppVoiceExperience appVoiceExperience;

    public bool IsActive => _active;

    private bool _active = false;

    private void OnEnable()
    {
        textArea.text = freshStateText;
        appVoiceExperience.events.OnRequestCreated.AddListener(OnRequestStarted);
        appVoiceExperience.events.OnPartialTranscription.AddListener(OnRequestTranscript);
        appVoiceExperience.events.OnFullTranscription.AddListener(OnRequestTranscript);
        appVoiceExperience.events.OnStartListening.AddListener(OnListenStart);
        appVoiceExperience.events.OnStoppedListening.AddListener(OnListenStop);
        appVoiceExperience.events.OnStoppedListeningDueToDeactivation.AddListener(OnListenForcedStop);
        appVoiceExperience.events.OnStoppedListeningDueToInactivity.AddListener(OnListenForcedStop);
        appVoiceExperience.events.OnResponse.AddListener(OnRequestResponse);
        appVoiceExperience.events.OnError.AddListener(OnRequestError);
    }

    private void OnDisable()
    {
        appVoiceExperience.events.OnRequestCreated.RemoveListener(OnRequestStarted);
        appVoiceExperience.events.OnPartialTranscription.RemoveListener(OnRequestTranscript);
        appVoiceExperience.events.OnFullTranscription.RemoveListener(OnRequestTranscript);
        appVoiceExperience.events.OnStartListening.RemoveListener(OnListenStart);
        appVoiceExperience.events.OnStoppedListening.RemoveListener(OnListenStop);
        appVoiceExperience.events.OnStoppedListeningDueToDeactivation.RemoveListener(OnListenForcedStop);
        appVoiceExperience.events.OnStoppedListeningDueToInactivity.RemoveListener(OnListenForcedStop);
        appVoiceExperience.events.OnResponse.RemoveListener(OnRequestResponse);
        appVoiceExperience.events.OnError.RemoveListener(OnRequestError);
    }

    private void OnRequestStarted(WitRequest r)
    {
        if (showJson) r.onRawResponse = (response) => textArea.text = response;
        _active = true;
    }

    private void OnRequestTranscript(string transcript)
    {
        textArea.text = transcript;
    }

    private void OnListenStart()
    {
        textArea.text = "Listening...";
    }

    private void OnListenStop()
    {
        textArea.text = "Processing...";
    }

    private void OnListenForcedStop()
    {
        if (!showJson)
        {
            textArea.text = freshStateText;
        }
        OnRequestComplete();
    }

    // Request response
    private void OnRequestResponse(WitResponseNode response)
    {
        if (!showJson)
        {
            if (!string.IsNullOrEmpty(response["text"]))
            {
                // textArea.text = "I heard: " + response["text"];
                textArea.text = response["text"];
            }
            else
            {
                textArea.text = freshStateText;
            }
        }
        OnRequestComplete();
    }

    private void OnRequestError(string error, string message)
    {
        if (!showJson)
        {
            textArea.text = $"<color=\"red\">Error: {error}\n\n{message}</color>";
        }
        OnRequestComplete();
    }

    private void OnRequestComplete()
    {
        _active = false;
    }

    public void ToggleActivation()
    {
        SetActivation(!_active);
    }

    // Set activation
    public void SetActivation(bool toActivated)
    {
        if (_active != toActivated)
        {
            _active = toActivated;
            if (_active)
            {
                appVoiceExperience.Activate();
            }
            else
            {
                appVoiceExperience.Deactivate();
            }
        }
    }
}