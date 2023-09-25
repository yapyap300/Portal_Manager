using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum Talk { Player = 0, leaderK }
public class Dialog_System : MonoBehaviour
{
    [SerializeField] private Dialog[] dialogs;// ���� �б��� ��� ���
    [SerializeField] private Image[] images;// ��ȭâ Image UI
    [SerializeField] private Text[] textNames;// ���� ������� ĳ���� �̸� ��� Text UI
    [SerializeField] private Text[] textDialogues;// ���� ��� ��� Text UI
    [SerializeField] private GameObject[] objectArrows;// ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� ������Ʈ
    [SerializeField] private float speed;// �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
    [SerializeField] private KeyCode keyCodeSkip = KeyCode.Space;// Ÿ���� ȿ���� ��ŵ�ϴ� Ű    

    private int currentIndex = -1;
    private bool isTyping = false;            // �ؽ�Ʈ Ÿ���� ȿ���� ���������
    private Talk currentTalk = Talk.Player;
    
    public void Setup()
    {
        for (int i = 0; i < 2; ++i)
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
                isTyping = false;
                textDialogues[(int)currentTalk].text = dialogs[currentIndex].dialogue;
                // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
                objectArrows[(int)currentTalk].SetActive(true);

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
                // ��� ĳ���� �̹����� ��Ӱ� ����
                for (int i = 0; i < 2; ++i)
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

        // ���� ȭ�� �̸� �ؽ�Ʈ Ȱ��ȭ �� ����
        textNames[(int)currentTalk].gameObject.SetActive(true);
        textNames[(int)currentTalk].text = dialogs[currentIndex].talking.ToString();

        // ȭ���� ��� �ؽ�Ʈ Ȱ��ȭ �� ���� (Typing Effect)
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
    public Talk talking; // ȭ��
    [TextArea(3, 5)]
    public string dialogue;	// ���
}

