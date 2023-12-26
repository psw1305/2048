using UnityEngine;

public class GameManager
{
    #region Fields

    private TileBoard gameBoard;

    #endregion

    #region Init

    public void Initialize()
    {
        gameBoard = GameObject.FindObjectOfType<TileBoard>();
        gameBoard.Initialize();
    }

    #endregion

    #region Game Progress

    public void NewGame()
    {
        gameBoard.ClearBoard();
        gameBoard.CreateTile();
        gameBoard.CreateTile();
    }

    public void GameOver()
    {
        NewGame();
    }

    #endregion
}
