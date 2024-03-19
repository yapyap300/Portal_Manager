using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartControl : MonoBehaviour
{
    [SerializeField] private Text localeText;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Transform[] people;
    private Vector3 plus = new Vector3(5, 0, 0);
    private Animator[] Animators;
    private int localeIndex = 0;
    void Awake()
    {
        startPosition = new Vector3(-12, 0, 0);
        Animators = new Animator[5];
        for (int index = 0; index < 5; index++)
            Animators[index] = people[index].GetComponent<Animator>();
    }
    void Start()
    {
        SoundManager.Instance.SetBGM("Start");
        SoundManager.Instance.PlayBGM();
        StartCoroutine(Play());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.DOKill();
        collision.transform.position = startPosition;
        SoundManager.Instance.PlaySfx("Entry");
    }
    public void StartGame()
    {
        DOTween.KillAll();
        SoundManager.Instance.StopBGM();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PointSound()
    {
        SoundManager.Instance.PlaySfx("Swap");
    }
    public void ChangeLocale()
    {
        if (localeIndex == 0)
            localeText.text = "En";
        else
            localeText.text = "Ko";
        StartCoroutine(Change(localeIndex = 1 - localeIndex));
    }
    IEnumerator Play()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            for(int index = 0; index < 5; index++)
            {
                Animators[index].SetBool("Walk", true);
                people[index].DOMove(people[index].transform.position + plus, 1f).SetEase(Ease.Linear);
            }
            yield return new WaitForSeconds(1f);
            for (int index = 0; index < 5; index++)
            {
                Animators[index].SetBool("Walk", false);
            }
        }
    }
    IEnumerator Change(int index)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
