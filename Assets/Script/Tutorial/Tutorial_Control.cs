using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_Control : MonoBehaviour
{
    [SerializeField] private List<Tutorial_Base> tutorials;
    [SerializeField] private string nextScene;
    private Tutorial_Base currentBase = null;
    private int currentIndex = -1;    
    private void Start()
    {
        SetNextTutorial();
    }
    private void Update()
    {
        if(currentBase != null)
        {
            currentBase.Excute(this);
        }
    }
    public void SetNextTutorial(bool skip = false)
    {
        if(currentBase != null)
        {
            currentBase.Exit();
        }

        if(currentIndex >= tutorials.Count - 1)
        {
            EndTutorial();
            return;
        }
        if (skip)
            currentIndex = tutorials.Count - 3;
        currentIndex++;
        currentBase = tutorials[currentIndex];

        currentBase.Enter();
    }
    public void EndTutorial()
    {
        currentBase = null;
        DOTween.KillAll();

        SceneManager.LoadScene(nextScene);
    }
}
