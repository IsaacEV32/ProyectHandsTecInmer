using UnityEngine;

public class IndexLevel : MonoBehaviour
{
    [SerializeField] int levelIndex;
    [SerializeField] int playerLayerInt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayerInt)
        {
            Hands.instance.setLevelIndex(levelIndex);
        }
    }

}
