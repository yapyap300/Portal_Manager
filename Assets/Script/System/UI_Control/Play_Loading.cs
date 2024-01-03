using DG.Tweening;
using UnityEngine;

public class Play_Loading : MonoBehaviour
{    
    [SerializeField] private Vector3[] size;
    private Transform image;
    private Vector3 defaultSize = new(5, 5, 5);
    void Awake()
    {
        image = transform.GetChild(0);
    }
    void OnEnable()
    {
        image.localScale = defaultSize;
        SoundManager.Instance.PlaySfx("Loading", 4.121f);
        image.DOScale(size[GameManager.Instance.stageIndex], 5).OnComplete(() => gameObject.SetActive(false)).SetUpdate(true);
    }
}
