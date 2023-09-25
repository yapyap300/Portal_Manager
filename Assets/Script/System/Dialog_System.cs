using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum Talk { Player = 0, leaderK }
public class Dialog_System : MonoBehaviour
{
    [SerializeField] private Dialog[] dialogs;// 현재 분기의 대사 목록
    [SerializeField] private Image[] images;// 대화창 Image UI
    [SerializeField] private Text[] textNames;// 현재 대사중인 캐릭터 이름 출력 Text UI
    [SerializeField] private Text[] textDialogues;// 현재 대사 출력 Text UI
    [SerializeField] private GameObject[] objectArrows;// 대사가 완료되었을 때 출력되는 커서 오브젝트
    [SerializeField] private float speed;// 텍스트 타이핑 효과의 재생 속도
    [SerializeField] private KeyCode keyCodeSkip = KeyCode.Space;// 타이핑 효과를 스킵하는 키    

    private int currentIndex = -1;
    private bool isTyping = false;            // 텍스트 타이핑 효과를 재생중인지
    private Talk currentTalk = Talk.Player;
    
    public void Setup()
    {
        for (int i = 0; i < 2; ++i)
        {
            // 모든 대화 관련 게임오브젝트 비활성화
            InActiveObjects(i);
        }
        
        SetNextDialog();
    }

    public bool UpdateDialog()
    {
        if (Input.GetKeyDown(keyCodeSkip) || Input.GetMouseButtonDown(0))
        {
            // 텍스트 타이핑 효과를 재생중일때 마우스 왼쪽 클릭하면 타이핑 효과 종료
            if (isTyping == true)
            {
                // 타이핑 효과를 중지하고, 현재 대사 전체를 출력한다
                textDialogues[(int)currentTalk].DOKill();                
                isTyping = false;
                textDialogues[(int)currentTalk].text = dialogs[currentIndex].dialogue;
                // 대사가 완료되었을 때 출력되는 커서 활성화
                objectArrows[(int)currentTalk].SetActive(true);

                return false;
            }

            // 다음 대사 진행
            if (currentIndex + 1 < dialogs.Length)
            {
                SetNextDialog();
            }
            // 대사가 더 이상 없을 경우 true 반환
            else
            {
                // 모든 캐릭터 이미지를 어둡게 설정
                for (int i = 0; i < 2; ++i)
                {
                    // 모든 대화 관련 게임오브젝트 비활성화
                    InActiveObjects(i);
                }

                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        // 이전 화자의 대화 관련 오브젝트 비활성화
        InActiveObjects((int)currentTalk);

        currentIndex++;

        // 현재 화자 설정
        currentTalk = dialogs[currentIndex].talking;

        // 대화창 활성화
        images[(int)currentTalk].gameObject.SetActive(true);

        // 현재 화자 이름 텍스트 활성화 및 설정
        textNames[(int)currentTalk].gameObject.SetActive(true);
        textNames[(int)currentTalk].text = dialogs[currentIndex].talking.ToString();

        // 화자의 대사 텍스트 활성화 및 설정 (Typing Effect)
        textDialogues[(int)currentTalk].gameObject.SetActive(true);
        isTyping = true;
        textDialogues[(int)currentTalk].DOText(dialogs[currentIndex].dialogue, speed).OnComplete(() => { isTyping = false; objectArrows[(int)currentTalk].SetActive(true); });        
    }

    private void InActiveObjects(int index)
    {
        images[index].gameObject.SetActive(false);
        textNames[index].gameObject.SetActive(false);
        textDialogues[index].gameObject.SetActive(false);
        objectArrows[index].SetActive(false);
    }
}
[System.Serializable]
public struct Dialog
{
    public Talk talking; // 화자
    [TextArea(3, 5)]
    public string dialogue;	// 대사
}

