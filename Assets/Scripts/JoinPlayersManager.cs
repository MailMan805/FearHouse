using UnityEngine;

public class JoinPlayersManager : MonoBehaviour
{
    public static JoinPlayersManager Instance;

    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

