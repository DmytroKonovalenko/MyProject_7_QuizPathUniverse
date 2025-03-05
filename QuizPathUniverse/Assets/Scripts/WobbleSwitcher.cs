using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class WobbleSwitcher : MonoBehaviour
{
    [Header("Fluffy Objects")]
    public GameObject blop1;
    public GameObject blop2;

    public CanvasGroup blop1CanvasGroup;
    public CanvasGroup blop2CanvasGroup;

    [Header("Bouncy Buttons")]
    public Button floof1;
    public Button floof2;

    [Header("Wiggly Settings")]
    public Image floof1Image;
    public Image floof2Image;

    public TextMeshProUGUI floof1Text;
    public TextMeshProUGUI floof2Text;

    [Header("Squishy Colors")]
    public Color splatColor = new Color(0.796f, 0.968f, 0.027f);
    public Color blurpColor = new Color(0.671f, 0.682f, 0.702f);
    public float wobbleTime = 0.5f;
    public float fadeDuration = 0.5f;

    private void Start()
    {
        MakeBlop1Active();
        floof1.onClick.AddListener(MakeBlop1Active);
        floof2.onClick.AddListener(MakeBlop2Active);
    }

    private void MakeBlop1Active()
    {
        blop1CanvasGroup.DOFade(1f, fadeDuration).OnStart(() => blop1.SetActive(true));
        blop2CanvasGroup.DOFade(0f, fadeDuration).OnComplete(() => blop2.SetActive(false));

        floof1Image.DOFade(1f, wobbleTime);
        floof2Image.DOFade(0f, wobbleTime);
        floof1Text.color = splatColor;
        floof2Text.color = blurpColor;
    }

    private void MakeBlop2Active()
    {
        blop1CanvasGroup.DOFade(0f, fadeDuration).OnComplete(() => blop1.SetActive(false));
        blop2CanvasGroup.DOFade(1f, fadeDuration).OnStart(() => blop2.SetActive(true));

        floof1Image.DOFade(0f, wobbleTime);
        floof2Image.DOFade(1f, wobbleTime);
        floof1Text.color = blurpColor;
        floof2Text.color = splatColor;
    }
}
