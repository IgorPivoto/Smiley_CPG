using UnityEngine;

public class PlatafomaY : MonoBehaviour
{
    [SerializeField] Transform pontoA;
    [SerializeField] Transform pontoB;
    [SerializeField] private float velocidade;
    private Transform alvoAtual;
    void Start()
    {
        alvoAtual = pontoB;
    }

    // Update is called once per frame
    void Update()
    {
        Movimentacao();
    }

    void Movimentacao()
    {
        // Move apenas no eixo Y, mantendo o X fixo
        Vector2 novaPosicao = new Vector2(transform.position.x, 
        Mathf.MoveTowards(transform.position.y, alvoAtual.position.y, velocidade * Time.deltaTime));

        transform.position = novaPosicao;

        // Quando chegar perto o suficiente no Y, troca de alvo
        if (Mathf.Abs(transform.position.y - alvoAtual.position.y) < 0.1f)
        {
            alvoAtual = (alvoAtual == pontoA) ? pontoB : pontoA;
        }
    }
}
