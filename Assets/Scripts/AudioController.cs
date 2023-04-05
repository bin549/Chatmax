using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.TTS.Utilities;
using System.Linq;

public class AudioController : MonoBehaviour
{
    [SerializeField] private TTSSpeaker _speaker;
    [SerializeField] private InputField _input;
    [SerializeField] private InputField dialogueText;
    [SerializeField] private GameObject thinkingText;
    [SerializeField] private ChatGPTResponse lastChatGPTResponseCache;
    public AudioSource deathAudio;
    public float textSpeed;
    private string gptPrompt;

    public string ChatGPTMessage
    {
        get
        {
            return (lastChatGPTResponseCache.Choices.FirstOrDefault()?.Message?.Content ?? null) ?? string.Empty;
        }
    }

    private void Awake()
    {
        _input.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _input.gameObject.SetActive(true);
            dialogueText.gameObject.SetActive(false);
            _speaker.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _input.gameObject.SetActive(false);
            dialogueText.gameObject.SetActive(false);
            thinkingText.gameObject.SetActive(true);
            dialogueText.text = "";
            string words = _input.text.Substring(0, _input.text.Length) + "answer me in short.";
            _input.text = "";
            Debug.Log(words);
            StartCoroutine(ChatGPTClient.Instance.Ask(words, (response) =>
            {
                lastChatGPTResponseCache = response;
                _speaker.Speak(ChatGPTMessage);
                // StopAllCoroutines();
                StartCoroutine(SetTextUI(ChatGPTMessage));
                Debug.Log($"Time: {response.ResponseTotalTime} ms");
            }));
        }
    }

    private IEnumerator SetTextUI(string words)
    {
        yield return new WaitForSeconds(3);
        thinkingText.SetActive(false);
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "";
        foreach (char letter in words.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}

