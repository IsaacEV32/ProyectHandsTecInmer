using UnityEngine;
using UnityEngine.SceneManagement;

public class Hands : MonoBehaviour
{
    int levelIndex;
    [SerializeField] ARSession arSession;
    public static Hands instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void setLevelIndex(int _levelIndex)
    {
        levelIndex = _levelIndex;
    }
    public void Loghands()
    {
        if (levelIndex != 0)
        {
            SceneManager.LoadScene(levelIndex);
        }
    }
}
