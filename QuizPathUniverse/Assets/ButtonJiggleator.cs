using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonJiggleator : MonoBehaviour
{
    [SerializeField] private Button magicTapper;
    [SerializeField] private float squishFactor = 0.8f;
    [SerializeField] private float bouncyDuration = 0.3f;

    private Vector3 originalScale;
    private SoundManager soundManager; 

    private void Awake()
    {
        if (magicTapper == null)
        {
            magicTapper = GetComponent<Button>();
        }

        originalScale = transform.localScale;

        soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
        {
            Debug.LogWarning("SoundManager не знайдено в сцені!");
        }

        AssignButtonMagic();
    }

    private void AssignButtonMagic()
    {
        magicTapper.onClick.AddListener(() => SummonJiggleForce());
    }

    public void SummonJiggleForce()
    {
        
        soundManager?.PlayClickSound();

        Sequence jiggleSequence = DOTween.Sequence();

        jiggleSequence
            .Append(transform.DOScale(originalScale * squishFactor, bouncyDuration / 2).SetEase(Ease.OutQuad))
            .Append(transform.DOScale(originalScale * 1.1f, bouncyDuration / 2).SetEase(Ease.InOutBounce))
            .Append(transform.DOScale(originalScale, bouncyDuration / 2).SetEase(Ease.OutElastic));
    }

    public void ResetJiggleCharm()
    {
        magicTapper.onClick.RemoveListener(() => SummonJiggleForce());
        transform.localScale = originalScale;
    }
}
