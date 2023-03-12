using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Meta.WitAi.TTS.Utilities;

public class AudioController : MonoBehaviour
{
    public AudioSource deathAudio;
    [SerializeField] private TTSSpeaker _speaker;
    [SerializeField] private InputField _input;
    [SerializeField] private InputField dialogueText;
public float textSpeed;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            string words = _input.text.Substring(0, _input.text.Length-1);
             _speaker.Speak(words);
              StopAllCoroutines();
              StartCoroutine(SetTextUI(words));
            _input.text = "";
            // deathAudio.Play();
        }
    }

    private IEnumerator SetTextUI(string words)
    {   
        dialogueText.text = "";
            foreach (char letter in words.ToCharArray()) {
             dialogueText.text += letter;
                        yield return new WaitForSeconds(textSpeed);
 
            }
    }
}

