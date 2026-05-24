using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

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
