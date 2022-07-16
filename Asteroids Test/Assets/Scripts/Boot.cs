using DefaultNamespace.GameSession;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    [SerializeField] private SceneContext _sceneContext;

    private void Awake()
    {
        DontDestroyOnLoad(_sceneContext.gameObject);

        SceneManager.LoadScene("GameScene");
    }
}
