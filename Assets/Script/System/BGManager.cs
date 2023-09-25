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
    public void Moring() //������������ �帣�� �ð��뿡 ���� ����� �ٲٴ� �Լ� 3�� ���� �Ŵ������� ȣ���ϰ��� ������������ �� �ѹ����� ȣ���ϸ� ��
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
