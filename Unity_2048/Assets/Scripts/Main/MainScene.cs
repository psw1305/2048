using UnityEngine;

public class MainScene : MonoBehaviour
{
    void Start()
    {
        // #1. 매니저 초기화
        Manager.Resource.Initialize();
        Manager.Game.Initialize();

        // #2. 게임 시작
        Manager.Game.NewGame();
    }
}
