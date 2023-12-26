using UnityEngine;

public class Manager : MonoBehaviour
{
    #region Singleton

    private static Manager instance;
    private static bool initialized;
    public static Manager Instance
    {
        get
        {
            if (!initialized)
            {
                initialized = true;

                GameObject obj = GameObject.Find("@Manager");
                if (obj == null)
                {
                    obj = new() { name = @"Manager" };
                    obj.AddComponent<Manager>();
                    DontDestroyOnLoad(obj);
                    instance = obj.GetComponent<Manager>();
                }
            }
            return instance;
        }
    }

    #endregion

    #region Manage

    private readonly ResourceManager _resource = new();
    private readonly GameManager _game = new();

    public static ResourceManager Resource => Instance != null ? Instance._resource : null;
    public static GameManager Game => Instance != null ? Instance._game : null;

    #endregion
}
