using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stage_Info //스테이지의 차별점을 둘 이벤트들의 정보 모음
{
    public List<int[]> stagePortalArea = new List<int[]>();//스테이지마다 각 차원문의 구역id가 뒤섞이는 이벤트를 위한 배열리스트
}
