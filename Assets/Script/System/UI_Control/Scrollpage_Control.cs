using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Scrollpage_Control : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private float swapTime = 0.2f;
    [SerializeField] private float minDistance = 50f;

    private float[] pageValue;
    private float distance;
    private float startClick;
    private float endClick;
    private int currentPage;
    private int maxPage;
    private bool isPlay;
    void Awake()
    {
        pageValue = new float[transform.childCount];
        distance = 1f / (pageValue.Length - 1);
        for (int index = 0; index < pageValue.Length; index++)
            pageValue[index] = distance * index;
        maxPage = transform.childCount;
    }
    void Update()
    {
        UpdatePage();
    }
    private void UpdatePage()
    {
        if (isPlay) return;
        if(Input.GetMouseButtonDown(0))
        {
            startClick = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endClick = Input.mousePosition.x;
            Swipe();
        }
    }
    private void Swipe()
    {
        if(Mathf.Abs(startClick- endClick) < minDistance)
        {
            StartCoroutine(MovePage(currentPage));
            return;
        }

        bool isLeft = startClick < endClick? true: false;
        if (isLeft)
        {
            if (currentPage == 0) return;
            currentPage--;
        }
        else
        {
            if (currentPage == maxPage - 1) return;
            currentPage++;
        }

        StartCoroutine(MovePage(currentPage));
    }
    IEnumerator MovePage(int index)
    {
        float start = scrollbar.value;
        float current = 0;
        float per = 0;

        isPlay = true;
        while(per < 1)
        {
            current += Time.unscaledDeltaTime;
            per = current / swapTime;
            scrollbar.value = Mathf.Lerp(start, pageValue[index], per);
            yield return null;
        }
        isPlay = false;
    }
    public void StartPage()
    {
        currentPage = 0;
        scrollbar.value = pageValue[0];
    }
}
