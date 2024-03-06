using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Transform[] people;
    private Vector3 plus = new Vector3(5, 0, 0);
    private Animator[] Animators;
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
}
