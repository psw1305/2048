using UnityEngine;

public class MainScene : MonoBehaviour
{
    void Start()
    {
        // #1. �Ŵ��� �ʱ�ȭ
        Manager.Resource.Initialize();
        Manager.Game.Initialize();

        // #2. ���� ����
        Manager.Game.NewGame();
    }
}
