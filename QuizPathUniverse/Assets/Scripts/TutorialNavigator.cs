using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InstructionPanel : MonoBehaviour
{
    public Sprite[] instructionImages;
    public string[] instructionTitles;
    public string[] instructionDescriptions;

    public Image displayImage;
    public TextMeshProUGUI displayTitle;
    public TextMeshProUGUI displayDescription;

    public Button nextButton;
    public TextMeshProUGUI buttonLabel;

    private int slideIndex = 0;
    private float transitionDuration = 0.3f;
    private Vector3 minimizedScale = new Vector3(0.7f, 0.7f, 0.7f);
    public PhantomWindowAnimator phantomWindowAnimator;
    private void Start()
    {
        if (PlayerPrefs.GetInt("InstructionPanelCompleted", 0) == 1)
            gameObject.SetActive(false);

        RefreshContent(false);
    }

    public void ChangeSlide()
    {
        slideIndex++;
        if (slideIndex >= instructionImages.Length)
        {
            slideIndex = 0;
            PlayerPrefs.SetInt("InstructionPanelCompleted", 1);
            PlayerPrefs.Save();
            phantomWindowAnimator.ExecutePhantomExit();
        }
        TriggerPageTransition();
    }

    private void TriggerPageTransition()
    {
        var fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Append(displayImage.DOFade(0, transitionDuration))
            .Join(displayImage.transform.DOScale(minimizedScale, transitionDuration))
            .Join(displayTitle.DOFade(0, transitionDuration))
            .Join(displayDescription.DOFade(0, transitionDuration));

        fadeOutSequence.AppendCallback(() => RefreshContent(true));

        fadeOutSequence.Append(displayImage.DOFade(1, transitionDuration))
            .Join(displayImage.transform.DOScale(Vector3.one, transitionDuration))
            .Join(displayTitle.DOFade(1, transitionDuration))
            .Join(displayDescription.DOFade(1, transitionDuration));
    }

    private void RefreshContent(bool animate)
    {
        displayImage.sprite = instructionImages[slideIndex];
        displayTitle.text = instructionTitles[slideIndex];
        displayDescription.text = instructionDescriptions[slideIndex];

        buttonLabel.text = slideIndex == 0 ? "Start" : slideIndex == instructionImages.Length - 1 ? "Let's go!" : "Continue";

        if (!animate)
        {
            displayImage.transform.localScale = Vector3.one;

            displayImage.color = Color.white;
            displayTitle.color = Color.white;
            displayDescription.color = Color.white;
        }
        else
        {
            displayImage.transform.localScale = minimizedScale;

            displayImage.color = new Color(1, 1, 1, 0);
            displayTitle.color = new Color(1, 1, 1, 0);
            displayDescription.color = new Color(1, 1, 1, 0);

            var fadeInSequence = DOTween.Sequence();

            fadeInSequence.Append(displayImage.DOFade(1, transitionDuration))
                .Join(displayImage.transform.DOScale(Vector3.one, transitionDuration))
                .Join(displayTitle.DOFade(1, transitionDuration))
                .Join(displayDescription.DOFade(1, transitionDuration));
        }
    }
}
