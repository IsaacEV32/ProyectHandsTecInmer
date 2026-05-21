using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
public class GameManagerGame : MonoBehaviour
{
    [SerializeField] List<Cube> topos = new List<Cube>();

    [SerializeField] float timer;
    float MaxTime = 120;
    bool juegoActivo = true;
    [SerializeField] TMP_Text textForTimer;
    [SerializeField] TMP_Text textForScore;
    float score;
    [SerializeField]float maxScore = 100;
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
            // 1. Elegimos un topo aleatorio (Nota: Random.Range para enteros es exclusivo en el máximo, de esta forma incluye el último)
            int indexRandom = Random.Range(0, topos.Count);

            // 2. Lo pintamos de rojo
            topos[indexRandom].ChangeColor(Color.red);

            // 3. Esperamos 3 segundos con el topo activo
            yield return new WaitForSeconds(3f);

            // 4. Lo devolvemos a su color original (Blanco)
            topos[indexRandom].ChangeColor(Color.white);

            // 5. Opcional: Esperamos un pequeño tiempo muerto (ej. 1 segundo) antes de que salga el siguiente topo
            yield return new WaitForSeconds(1f);
        }
    }
}
