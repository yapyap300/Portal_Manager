using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum Talk { ������C = 0, ������P = 1, ���̵�L = 2, �۰�K = 3, �ӿ�D = 4}
public class Dialog_System : MonoBehaviour
{
    [SerializeField] private Dialog[] dialogs;// ���� �б��� ��� ���
    [SerializeField] private Image[] images;// ��ȭâ Image UI
    [SerializeField] private Text[] textDialogues;// ���� ��� ��� Text UI
    [SerializeField] private GameObject[] objectArrows;// ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ    
    [SerializeField] private float speed;// �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
    [SerializeField] private KeyCode keyCodeSkip = KeyCode.Space;// Ÿ���� ȿ���� ��ŵ�ϴ� Ű    
    

    private int currentIndex = -1;
    private bool isTyping = false;            // �ؽ�Ʈ Ÿ���� ȿ���� ���������
    private Talk currentTalk = Talk.������C;
    
    public void Setup()
    {
        for (int i = 0; i < images.Length; ++i)
        {
            // ��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
            InActiveObjects(i);
        }
        
        SetNextDialog();
    }

    public bool UpdateDialog()
    {
        if (Input.GetKeyDown(keyCodeSkip) || Input.GetMouseButtonDown(0))
        {
            // �ؽ�Ʈ Ÿ���� ȿ���� ������϶� ���콺 ���� Ŭ���ϸ� Ÿ���� ȿ�� ����
            if (isTyping == true)
            {
                // Ÿ���� ȿ���� �����ϰ�, ���� ��� ��ü�� ����Ѵ�
                textDialogues[(int)currentTalk].DOKill();
                textDialogues[(int)currentTalk].text = dialogs[currentIndex].dialogue;
                // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
                objectArrows[(int)currentTalk].SetActive(true);
                isTyping = false;

                return false;
            }

            // ���� ��� ����
            if (currentIndex + 1 < dialogs.Length)
            {
                SetNextDialog();
            }
            // ��簡 �� �̻� ���� ��� true ��ȯ
            else
            {                
                for (int i = 0; i < images.Length; ++i)
                {
                    // ��� ��ȭ ���� ���ӿ�����Ʈ ��Ȱ��ȭ
                    InActiveObjects(i);
                }

                return true;
            }
        }

        return false;
    }

    private void SetNextDialog()
    {
        // ���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
        InActiveObjects((int)currentTalk);

        currentIndex++;

        // ���� ȭ�� ����
        currentTalk = dialogs[currentIndex].talking;

        // ��ȭâ Ȱ��ȭ
        images[(int)currentTalk].gameObject.SetActive(true);

        // ȭ���� ��� �ؽ�Ʈ Ȱ��ȭ �� ���� (Typing Effect)        
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
    public Talk talking; // ȭ��
    [TextArea(3, 5)]
    public string dialogue;	// ���
}

