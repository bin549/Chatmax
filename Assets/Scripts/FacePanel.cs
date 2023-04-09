using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FacePanel : MonoBehaviour
{
    [SerializeField] private Image faceImage;
    [SerializeField] private TMP_Text moodText;
    [SerializeField] private Sprite happyFace;
    [SerializeField] private Sprite sadFace;
    [SerializeField] private Sprite scaryFace;
    [SerializeField] private string happyText_en;
    [SerializeField] private string happyText_zh;
    [SerializeField] private string sadText_en;
    [SerializeField] private string sadText_zh;
    [SerializeField] private string[] scaryTexts_en;
    [SerializeField] private string[] scaryTexts_zh;
    [SerializeField] private string scaryText_en;
    [SerializeField] private string scaryText_zh;
    [SerializeField] private int scary_index;
    [SerializeField] private CharacterWobble characterWobble;
    [SerializeField] private FaceWobble faceWobble;

    public AppSettings appSettings;

    private void Awake()
    {
        appSettings = FindObjectOfType<AppSettings>();
    }

    private void OnMouseEnter()
    {
        ChangeScaryMood();
    }

    private void OnMouseExit()
    {
        ChangeHappyMood();
    }

    public void ChangeScaryMood()
    {
        scary_index = Random.Range(0, scaryTexts_en.Length);
        Quaternion originalRot = faceImage.transform.rotation;
        faceImage.transform.rotation = originalRot * Quaternion.AngleAxis(180 * scary_index, Vector3.up);
        faceImage.sprite = scaryFace;
        if (appSettings.isEnglish)
        {
            scaryText_en = scaryTexts_en[scary_index];
            moodText.text = scaryText_en;
        }
        else
        {
            scaryText_zh = scaryTexts_zh[scary_index];
            moodText.text = scaryText_zh;
        }
    }

    public void ChangeHappyMood()
    {
        faceImage.sprite = happyFace;
        moodText.text = appSettings.isEnglish ? happyText_en : happyText_zh;
        characterWobble.SetDefault();
        faceWobble.SetSad(false);
    }

    public void ChangeSadMood()
    {
        faceImage.sprite = sadFace;
        moodText.text = appSettings.isEnglish ? sadText_en : sadText_zh;
        characterWobble.SetScary();
        faceWobble.SetSad(true);
    }
}