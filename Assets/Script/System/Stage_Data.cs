using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage_Data : MonoBehaviour
{
    /*스테이지마다 활성화할 차원문이나 구역의 뒤틀림으로 담당구역 바꾸는일
    그리고 각 스테이지마다 특정한 대화나 이벤트 연출이 필요해서 그걸 가지고 있는 스테이지 정보를 이런식으로 하면 
    어떨까 생각 해보았다.
    */
    public GameObject stageEvent;
    public int stageTime;
    public int portalCount;
    public bool[] portalActive;
    public bool isMix;
    public int vipIndex;
}
