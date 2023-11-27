using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Control : MonoBehaviour
{
    [SerializeField] Transform[] areaPanel;//각 스테이지의 구역정보를 갱신하여 보여줌
    [SerializeField] Transform[] vipPanel;//vip의 정보를 보여줌
    [SerializeField] Text time;// 스테이지의 시간을 보여줌
    [SerializeField] Transform[] errorControl;//그냥 연출용
    private Sequence error;

    void Start()
    {
        error = DOTween.Sequence();
        error.Append(errorControl[0].DOShakePosition(1)).Join(errorControl[1].GetComponent<Image>().DOColor(Color.red, 0.15f)
            .SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo)).SetUpdate(true).SetAutoKill(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isStop)
            {
                errorControl[0].gameObject.SetActive(false);
                GameManager.Instance.Resume();
            }
            else
            {
                Init();
                errorControl[0].gameObject.SetActive(true);
                GameManager.Instance.Stop();
            }
        }
    }
    private void Init()
    {
        int hour = Mathf.FloorToInt(GameManager.Instance.time / GameManager.Instance.maxTime[GameManager.Instance.stageIndex] * 24);
        int min = Mathf.FloorToInt(GameManager.Instance.time * (GameManager.Instance.maxTime[GameManager.Instance.stageIndex] / 60));
        time.text = $"{hour} : {min}";
        if (GameManager.Instance.timeView)
            time.gameObject.SetActive(true);
        for(int index = 0; index < 4; index++)
        {
            areaPanel[index].GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.areaSprites[GameManager.Instance.portals[index].GetAreaID()];
            if(index < GameManager.Instance.maxDestination)
                areaPanel[index].gameObject.SetActive(true);
            if (index < GameManager.Instance.vipIndex)
                vipPanel[index].gameObject.SetActive(true);
        }
    }
    public void Area(GameObject areaPanel)
    {
        if (GameManager.Instance.areaView)
            areaPanel.SetActive(true);
        else
            Error();
    }
    private void Error()//그냥 채우기용으로 만들어놓은 버튼을 누르면 실행
    {
        SoundManager.Instance.PlaySfx("Error");
        error.Restart();
    }
}
