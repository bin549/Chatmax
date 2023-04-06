using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.TTS.Utilities;
using System.Linq;
using TMPro;

public class QAController : MonoBehaviour
{
    [SerializeField] private TTSSpeaker _speaker;
    [SerializeField] private TMP_Text questionInputField;
    [SerializeField] private GameObject answerDialogue;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject thinkingText;
    [SerializeField] private ChatGPTResponse lastChatGPTResponseCache;
    public AudioSource deathAudio;
    public float textSpeed;
    private string gptPrompt;
    [SerializeField] private string inputChatStr;
    [SerializeField] private bool isAsked = false;
    private string stackString = "";
   [SerializeField] private AudioManager audioManager;
   [SerializeField] private AppSettings appSettings;
   [SerializeField] private SceneUI sceneUI;


    public string ChatGPTMessage
    {
        get
        {
            return (lastChatGPTResponseCache.Choices.FirstOrDefault()?.Message?.Content ?? null) ?? string.Empty;
        }
    }

    private void Awake()
    {
        this.questionInputField.gameObject.SetActive(true);
        this.audioManager = GameObject.FindObjectOfType<AudioManager>();
        this.appSettings = GameObject.FindObjectOfType<AppSettings>();
        this.sceneUI = GameObject.FindObjectOfType<SceneUI>();
    }

    private void Update()
    {
        if (!sceneUI.isUIActive && appSettings.isUsingKeyboard)
        {
            GetQuestionChar();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {  
            this.Ask();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            this.questionInputField.gameObject.SetActive(false);
            this.answerDialogue.SetActive(false);
            this.thinkingText.gameObject.SetActive(true);
            this.audioManager.PlayThinkingSound();
            this.dialogueText.text = "";
            string words = this.questionInputField.text.Substring(0, this.questionInputField.text.Length) + "answer me in short.";
            this.questionInputField.text = "";
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

    public void Clear()
    {
        this.Ask();
        this.questionInputField.text = "";
        this.questionInputField.gameObject.SetActive(false);
        thinkingText.SetActive(false);
    }

    public void Show()
    {
        this.questionInputField.gameObject.SetActive(true);
    }

    private void Ask()
    {
        this.questionInputField.gameObject.SetActive(true);
        this.answerDialogue.SetActive(false);
        _speaker.Stop();
        this.isAsked = false;
    }

   private void GetQuestionChar()
   {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (this.questionInputField.text.Length != 1)
            {
                this.stackString = this.questionInputField.text;
                this.questionInputField.text = this.questionInputField.text.Substring(0, this.questionInputField.text.Length-2);
                // if (this.stackString == this.questionInputField.text)
                // {
                //     this.questionInputField.text = this.questionInputField.text.Substring(0, this.questionInputField.text.Length-2);
                //     this.questionInputField.text = this.questionInputField.text.Substring(0, this.questionInputField.text.Length-1);
                // }
                // Debug.Log(this.stackString);
                // Debug.Log(this.questionInputField.text);
            }
        }

        foreach (char c in Input.inputString)
        {
            if (this.isAsked) {
                this.Ask();
                     this.questionInputField.text = "";
                } 
                this.audioManager.PlayTypeSound();
            this.questionInputField.text += c;
        }
   }

    private IEnumerator SetTextUI(string words)
    {
        yield return new WaitForSeconds(2.5f);
        this.isAsked = true;
        this.thinkingText.SetActive(false);
        // this.audioManager.PlayThinkedSound();
        this.answerDialogue.SetActive(true);
        this.dialogueText.text = "";
        foreach (char letter in words.ToCharArray())
        {
            this.dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}

