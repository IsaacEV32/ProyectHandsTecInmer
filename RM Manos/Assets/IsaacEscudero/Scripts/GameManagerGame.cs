using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
public class GameManagerGame : MonoBehaviour
{
    [SerializeField] List<Cube> topos = new List<Cube>();

    [SerializeField] float timer;
    float MaxTime = 120;
    bool juegoActivo = true;
    [SerializeField] TMP_Text textForTimer;
    [SerializeField] TMP_Text textForScore;
    float score;
    [SerializeField] float maxScore = 100;
    public static GameManagerGame instance;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < topos.Count; i++)
        {
            topos[i].ChangeColor(Color.white);
        }
        textForScore.text = "Score: " + score + " / " + maxScore;
        StartCoroutine(CicloJuego());
    }

    // Update is called once per frame
    void Update()
    {
        if (juegoActivo)
        {
            if (timer < MaxTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                juegoActivo = false;
            }
            if (score >= maxScore || timer >= MaxTime)
            {
                ARSession aRSession = FindFirstObjectByType<ARSession>();
                XROrigin player = FindFirstObjectByType<XROrigin>();
                DontDestroyOnLoad(aRSession);
                DontDestroyOnLoad(player);
                XRInteractionManager xRInteractionManager = FindFirstObjectByType<XRInteractionManager>();
                DontDestroyOnLoad(xRInteractionManager);
                SceneManager.LoadScene(0);
            }
        }
    }
    internal void AddToTheListOfTopos(Cube c)
    {
        topos.Add(c);
    }
    internal void SumScore(int pointsToSum)
    {
        score += pointsToSum;
        textForScore.text = "Score: " + score + " / " + maxScore;
    }
    IEnumerator CicloJuego()
    {
        while (juegoActivo)
        {
            int indexRandom = Random.Range(0, topos.Count);
            topos[indexRandom].ChangeColor(Color.red);
            yield return new WaitForSeconds(3f);
            if (!topos[indexRandom].GetColor().Equals(Color.green))
            {
                topos[indexRandom].ChangeColor(Color.black);
            }
            yield return new WaitForSeconds(1f);
            topos[indexRandom].ChangeColor(Color.white);
        }
    }
}
