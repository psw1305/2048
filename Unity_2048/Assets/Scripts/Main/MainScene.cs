using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainScene : MonoBehaviour
{
    #region UI Fields

    [Header("Title")]
    [SerializeField] private CanvasGroup titleCanvas;
    [SerializeField] private Button startBtn;

    [Header("Main")]
    [SerializeField] private CanvasGroup mainCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button newGameBtn;

    [Header("GameOver")]
    [SerializeField] private CanvasGroup gameoverCanvas;
    [SerializeField] private Button titleBtn;
    [SerializeField] private Button retryBtn;

    #endregion

    #region Init

    private void Initialize()
    {
        startBtn.onClick.AddListener(Manager.Game.StartGame);
        newGameBtn.onClick.AddListener(Manager.Game.NewGame);
        titleBtn.onClick.AddListener(Manager.Game.GoTitle);
        retryBtn.onClick.AddListener(Manager.Game.NewGame);
    }

    void Start()
    {
        // #1. 매니저 초기화
        Manager.Resource.Initialize();
        Manager.Game.Initialize();

        // #2. 메인 씬 초기화
        Initialize();
    }

    #endregion

    #region UI Methods

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SetBestScoreText(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();
    }

    public void SetGameOverCanvas()
    {
        gameoverCanvas.blocksRaycasts = false;
        gameoverCanvas.alpha = 0;
    }

    public void StartGameAnimate()
    {
        DOTween.Sequence()
            .OnStart(() => 
            {
                titleCanvas.blocksRaycasts = false;
                mainCanvas.blocksRaycasts = true;
            })
            .Append(titleCanvas.DOFade(0f, 1f))
            .Append(Camera.main.transform.DOMoveY(10, 1f).SetEase(Ease.OutSine))
            .Append(mainCanvas.DOFade(1f, 1f));
    }

    public void GoTitleAnimate()
    {
        DOTween.Sequence()
            .OnStart(() =>
            {
                titleCanvas.blocksRaycasts = true;
                mainCanvas.blocksRaycasts = false;
                gameoverCanvas.blocksRaycasts = false;
            })
            .Append(mainCanvas.DOFade(0f, 1f))
            .Join(gameoverCanvas.DOFade(0f, 1f))
            .Append(Camera.main.transform.DOMoveY(25, 1f).SetEase(Ease.InSine))
            .Append(titleCanvas.DOFade(1f, 1f));
    }

    public void FadeGameOverCanvas()
    {
        gameoverCanvas.blocksRaycasts = true;
        gameoverCanvas.DOFade(1f, 1f);
    }

    #endregion
}
