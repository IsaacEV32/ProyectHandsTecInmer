using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

public class Hands : MonoBehaviour
{
    int levelIndex;
    [SerializeField] ARSession arSession;
    [SerializeField] GameObject player;
    public static Hands instance;
    AudioManager manager;
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
        DontDestroyOnLoad(arSession);
        DontDestroyOnLoad(player);
        AudioManager manager = FindFirstObjectByType<AudioManager>();
        DontDestroyOnLoad(manager);
        XRInteractionManager xRInteractionManager = FindFirstObjectByType<XRInteractionManager>();
        DontDestroyOnLoad(xRInteractionManager);
        SceneManager.LoadScene(levelIndex);
    }
}
