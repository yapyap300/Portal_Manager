using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum Talk { 관리자C = 0, 관리자P = 1, 아이돌L = 2, 작가K = 3, 임원D = 4}
public class Dialog_System : MonoBehaviour
{
    [SerializeField] private Dialog[] dialogs;// 현재 분기의 대사 목록
    [SerializeField] private Image[] images;// 대화창 Image UI
    [SerializeField] private Text[] textDialogues;// 현재 대사 출력 Text UI
    [SerializeField] private GameObject[] objectArrows;// 대사가 완료되었을 때 출력되는 커서 오브젝트    
    [SerializeField] private float speed;// 텍스트 타이핑 효과의 재생 속도
    [SerializeField] private KeyCode keyCodeSkip = KeyCode.Space;// 타이핑 효과를 스킵하는 키    
    

    private int currentIndex = -1;
    private bool isTyping = false;            // 텍스트 타이핑 효과를 재생중인지
    private Talk currentTalk = Talk.관리자C;
    
    public void Setup()
    {
        for (int i = 0; i < images.Length; ++i)
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
                textDialogues[(int)currentTalk].text = dialogs[currentIndex].dialogue;
                // 대사가 완료되었을 때 출력되는 커서 활성화
                objectArrows[(int)currentTalk].SetActive(true);
                isTyping = false;

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
                for (int i = 0; i < images.Length; ++i)
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

        // 화자의 대사 텍스트 활성화 및 설정 (Typing Effect)        
        textDialogues[(int)currentTalk].gameObject.SetActive(true);        
        isTyping = true;
        textDialogues[(int)currentTalk].DOText(dialogs[currentIndex].dialogue, speed * dialogs[currentIndex].dialogue.Length).SetEase(Ease.Linear).SetUpdate(true).OnComplete(() => { isTyping = false; objectArrows[(int)currentTalk].SetActive(true); });        
    }

    private void InActiveObjects(int index)
    {
        if (images[index] == null) return;
        images[index].gameObject.SetActive(false);
        textDialogues[index].text = "";
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

