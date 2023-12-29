using UnityEngine;

public class GameManager
{
    #region Fields

    private MainScene mainScene;
    private TileBoard gameBoard;

    #endregion

    #region Properties

    public int Score { get; private set; }
    

    #endregion

    #region Init

    public void Initialize()
    {
        mainScene = GameObject.FindObjectOfType<MainScene>();
        gameBoard = GameObject.FindObjectOfType<TileBoard>();
        gameBoard.Initialize();
    }

    #endregion

    #region Game Progress

    public void StartGame()
    {
        NewGame();
        mainScene.StartGameAnimate();
    }

    public void NewGame()
    {
        SetScore(0);
        mainScene.SetBestScoreText(LoadBestScore());
        mainScene.SetGameOverCanvas();

        gameBoard.ClearBoard();
        gameBoard.CreateTile();
        gameBoard.CreateTile();
    }

    public void GameOver()
    {
        mainScene.FadeGameOverCanvas();
    }

    public void GoTitle()
    {
        mainScene.GoTitleAnimate();
    }

    #endregion

    #region Score

    public void AddScore(int points)
    {
        SetScore(Score + points);
    }

    private void SetScore(int score)
    {
        Score = score;
        mainScene.SetScoreText(score);
        SaveBestScore();
    }

    private void SaveBestScore()
    {
        int bestScore = LoadBestScore();

        if (Score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", Score);
        }
    }

    private int LoadBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0);
    }

    #endregion
}
