using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public static Player instance;
    static bool isFirstTimePlayed = true;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (isFirstTimePlayed)
        {
            ARSession arSession = FindFirstObjectByType<ARSession>();
            XROrigin player = FindFirstObjectByType<XROrigin>();
            DontDestroyOnLoad(arSession);
            DontDestroyOnLoad(player);
            AudioManager manager = FindFirstObjectByType<AudioManager>();
            DontDestroyOnLoad(manager);
            XRInteractionManager xRInteractionManager = FindFirstObjectByType<XRInteractionManager>();
            DontDestroyOnLoad(xRInteractionManager);
            isFirstTimePlayed = false;
        }
    }
}
