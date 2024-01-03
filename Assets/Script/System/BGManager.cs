using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    [SerializeField] Sprite[] BackGrounds;
    [SerializeField] SpriteRenderer Bg;
    private bool morningChange = false;
    private bool afternoonChange = false;
    private bool nightChange = false;
    void Awake()
    {
        Bg = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (GameManager.Instance.isStop)
            return;
        float time = GameManager.Instance.time;

        if(!morningChange && time < GameManager.Instance.afterNoon)
        {
            morningChange = true;
            afternoonChange = false;
            nightChange = true;
            Morning();
        }
        if(!afternoonChange && time >= GameManager.Instance.afterNoon)
        {
            afternoonChange = true;
            nightChange = false;
            Afternoon();
        }
        if (!nightChange && time >= GameManager.Instance.night)
        {
            morningChange = false;//스테이지가 지날때 초기화 시키는거 보다는 자체적으로 계속 돌게 하는게 좋을것 같아서 해보았다.
            nightChange = true;
            Afternoon();
        }
    }
    public void Morning() //스테이지에서 흐르는 시간대에 맞춰 배경을 바꾸는 함수 3개 게임 매니저에서 호출하게함 스테이지마다 딱 한번씩만 호출하면 됨
    {
        Bg.sprite = BackGrounds[Random.Range(0, 4)];
    }
    public void Afternoon()
    {
        Bg.sprite = BackGrounds[Random.Range(3, 6)];
    }
    public void Night()
    {
        Bg.sprite = BackGrounds[Random.Range(6, 8)];
    }
}
