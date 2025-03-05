using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EtherealLoadingDance : MonoBehaviour
{
    [Header("Панель і об'єкти")]
    [SerializeField] private GameObject mysticPanel; 


    [Header("Налаштування анімацій")]
    [SerializeField] private float logoRevealTime = 1.5f; 
    [SerializeField] private float driftSpeed = 50f; 
    [SerializeField] private float panelCloseDelay = 3f; 
    [SerializeField] private float panelCloseDuration = 1f; 



   

    private void Start()
    {
        StartEtherealLoadingDance();
    }

   
    private void StartEtherealLoadingDance()
    {
     
      
        Invoke(nameof(CommenceMysticExit), panelCloseDelay);
    }


    private void CommenceMysticExit()
    {
        Sequence panelSequence = DOTween.Sequence();

        panelSequence
            .Append(mysticPanel.GetComponent<CanvasGroup>().DOFade(0, panelCloseDuration)) 
            .Join(mysticPanel.transform.DOScale(Vector3.zero, panelCloseDuration).SetEase(Ease.InBack)) 
            .OnComplete(() => mysticPanel.SetActive(false)); 
    }
}
