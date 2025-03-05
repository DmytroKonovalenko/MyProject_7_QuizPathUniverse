using UnityEngine;
using DG.Tweening;

public class PhantomWindowAnimator : MonoBehaviour
{
    [Header("Налаштування анімації")]
    [SerializeField] private float spectralOpenTime = 0.7f; 
    [SerializeField] private float shadowyCloseTime = 0.5f; 
    [SerializeField] private Vector3 apparitionScale = new Vector3(1.1f, 1.1f, 1f); 
    [SerializeField] private float ghostlyFade = 0.7f; 

    private CanvasGroup etherealCanvasGroup;

    private void Awake()
    {
        etherealCanvasGroup = GetComponent<CanvasGroup>();
        if (etherealCanvasGroup == null)
        {
            etherealCanvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void OnEnable()
    {
        PerformEtherealEntrance();
    }

    
    private void PerformEtherealEntrance()
    {
     
        transform.localScale = apparitionScale;
        etherealCanvasGroup.alpha = ghostlyFade;

      
        transform.DOScale(Vector3.one, spectralOpenTime).SetEase(Ease.OutBack);
        etherealCanvasGroup.DOFade(1f, spectralOpenTime).SetEase(Ease.InOutQuad);
    }

   
    public void ExecutePhantomExit()
    {
        
        Sequence spectralExitSequence = DOTween.Sequence();

        spectralExitSequence
            .Append(transform.DOScale(apparitionScale, shadowyCloseTime).SetEase(Ease.InBack)) 
            .Join(etherealCanvasGroup.DOFade(0f, shadowyCloseTime).SetEase(Ease.InOutQuad)) 
            .OnComplete(() => gameObject.SetActive(false)); 
    }
}
