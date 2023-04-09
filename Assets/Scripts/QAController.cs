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
    [SerializeField] private bool isAsked = true;
    private string stackString = "";
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AppSettings appSettings;
    [SerializeField] private SceneUI sceneUI;
    [SerializeField] private TTSInteractionHandler ttsInteractionHandler;
    [SerializeField] private GameObject microphoneLogo;
    [SerializeField] private GameObject keyboardLogo;
    public bool isFirstQuestion = true;
    [SerializeField] private Animator microphoneAnimator;
    public bool isSpeakng = false;
    public bool isFirstSpeaking = true;

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
        if (Input.GetKeyDown(KeyCode.Space) && !appSettings.isUsingKeyboard)
        {
            if (this.isAsked)
            {
                this.microphoneLogo.SetActive(true);
                this.microphoneAnimator.SetTrigger("Start");
                this.Ask();
                this.ttsInteractionHandler.ToggleActivation();
                isSpeakng = true;
            }
            else
            {
                if (isSpeakng)
                {
                    this.questionInputField.gameObject.SetActive(false);
                    this.microphoneLogo.SetActive(false);
                    AskQuestion();
                    isSpeakng = false;
                }
                else
                {
                    this.questionInputField.gameObject.SetActive(true);
                    this.microphoneAnimator.SetTrigger("Start");
                    if (this.isFirstSpeaking)
                    {
                        this.sceneUI.promptField.gameObject.SetActive(false);
                        this.isFirstSpeaking = false;
                    }
                    this.isSpeakng = true;
                }
                this.ttsInteractionHandler.ToggleActivation();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && appSettings.isUsingKeyboard)
        {
            if (!this.isAsked)
            {
                this.AskQuestion();
            }
        }
    }

    private void AskQuestion()
    {
        this.isAsked = true;
        this.questionInputField.gameObject.SetActive(false);
        this.answerDialogue.SetActive(false);
        this.thinkingText.gameObject.SetActive(true);
        this.audioManager.PlayThinkingSound();
        this.dialogueText.text = "";
        string words = questionInputField.text.Trim();
        Debug.Log(words);
        if (words.EndsWith("£¿") || words.EndsWith("."))
            words = words + " in sentence.";
        else
            words = words + ". in sentence.";
        Debug.Log(words);
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
        this.questionInputField.text = "";
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
                this.questionInputField.text = this.questionInputField.text.Substring(0, this.questionInputField.text.Length - 2);
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
            if (this.isFirstQuestion)
            {
                this.keyboardLogo.SetActive(false);
                this.isFirstQuestion = false;
                this.questionInputField.gameObject.SetActive(true);
                this.questionInputField.text = "";
                sceneUI.promptField.gameObject.SetActive(false);
            }
            if (this.isAsked)
            {
                this.keyboardLogo.SetActive(false);
                this.questionInputField.text = "";
                this.Ask();
            }
            this.audioManager.PlayTypeSound();
            this.questionInputField.text += c;
        }
    }

    private IEnumerator SetTextUI(string words)
    {
        yield return new WaitForSeconds(2.5f);
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