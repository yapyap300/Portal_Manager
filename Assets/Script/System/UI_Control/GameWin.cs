using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWin : MonoBehaviour
{
    [SerializeField] private Text main;
    [SerializeField] private Image panel;
    [SerializeField] private Text[] left;
    [SerializeField] private Text[] right;
    [SerializeField] private Button exit;
    [SerializeField] private Image inPanel;
    private int[] scores = new int[6];
    private string[] texts = new string[6];
    // Start is called before the first frame update
    void Start()
    {
        scores[0] = GameManager.Instance.totalPay + GameManager.Instance.money;
        scores[1] = GameManager.Instance.wrongArea + GameManager.Instance.wrongCount;
        scores[2] = GameManager.Instance.vipCount;
        scores[3] = GameManager.Instance.gameoverCount;
        scores[4] = GameManager.Instance.endMoney;
        scores[5] = scores[0] - (scores[1] * 30) + (scores[2] * 1000) - (scores[3] * 10000) + (scores[4] * 10);
        texts[0] = LocalizationSettings.StringDatabase.GetLocalizedString("Main", "win00", GameManager.Instance.currentLocale);
        texts[1] = LocalizationSettings.StringDatabase.GetLocalizedString("Main", "win01", GameManager.Instance.currentLocale) + $" ( {scores[1]} )";
        texts[2] = LocalizationSettings.StringDatabase.GetLocalizedString("Main", "win02", GameManager.Instance.currentLocale) + $" ( {scores[2]} )";
        texts[3] = LocalizationSettings.StringDatabase.GetLocalizedString("Main", "win03", GameManager.Instance.currentLocale) + $" ( {scores[3]} )";
        texts[4] = LocalizationSettings.StringDatabase.GetLocalizedString("Main", "win04", GameManager.Instance.currentLocale);
        texts[5] = LocalizationSettings.StringDatabase.GetLocalizedString("Main", "win05", GameManager.Instance.currentLocale);
        int index = 0;
        DOTween.Sequence().Append(main.DOText(LocalizationSettings.StringDatabase.GetLocalizedString("Main", "win06", GameManager.Instance.currentLocale), 2f)
            .SetEase(Ease.Linear)).Append(panel.DOFade(1, 2f)).Append(ScoreActive(index, scores[index], texts[index++]))
            .Append(ScoreActive(index, scores[index] * -30, texts[index++])).Append(ScoreActive(index, scores[index] * 1000, texts[index++]))
            .Append(ScoreActive(index, scores[index] * -10000, texts[index++])).Append(ScoreActive(index, scores[index] * 10, texts[index++])).Append(ScoreActive(index, scores[index], texts[index++]))
            .Join(inPanel.DOFade(1f,0.5f)).SetUpdate(true).OnComplete(() => exit.interactable = true);
    }

    private Tween ScoreActive(int index,int target,string s)
    {
        return DOVirtual.Int(0, target, 1.5f, (x) => right[index].text = x.ToString()).SetUpdate(true).SetEase(Ease.Linear).OnPlay(() => {
            left[index].text = s;
        });
    }
    public void GameExit()
    {
        SceneManager.LoadScene(0);
    }
}
