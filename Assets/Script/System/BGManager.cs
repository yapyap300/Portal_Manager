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
            morningChange = false;//���������� ������ �ʱ�ȭ ��Ű�°� ���ٴ� ��ü������ ��� ���� �ϴ°� ������ ���Ƽ� �غ��Ҵ�.
            nightChange = true;
            Afternoon();
        }
    }
    public void Morning() //������������ �帣�� �ð��뿡 ���� ����� �ٲٴ� �Լ� 3�� ���� �Ŵ������� ȣ���ϰ��� ������������ �� �ѹ����� ȣ���ϸ� ��
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
