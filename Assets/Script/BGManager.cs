using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    private static BGManager instance;
    [SerializeField] Sprite[] BackGrounds;
    [SerializeField] SpriteRenderer Bg;
    public static BGManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Moring() //스테이지에서 흐르는 시간대에 맞춰 배경을 바꾸는 함수 3개 게임 매니저에서 호출하게함 스테이지마다 딱 한번씩만 호출하면 됨
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
