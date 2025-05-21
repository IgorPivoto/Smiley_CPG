using UnityEngine;

public class Sentinela : MonoBehaviour
{
    [SerializeField] private int dano;
    [SerializeField] Transform pontoA;
    [SerializeField] Transform pontoB;
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private int vida;
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
        // Move em direção ao alvo
        transform.position = Vector2.MoveTowards(transform.position, alvoAtual.position, velocidade * Time.deltaTime);

        // Quando chegar perto o suficiente, troca de alvo
        if (Vector2.Distance(transform.position, alvoAtual.position) < 0.1f)
        {
            alvoAtual = (alvoAtual == pontoA) ? pontoB : pontoA;

            // Flip visual (opcional, se estiver olhando para a esquerda/direita)
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    void Vida(int danoInimigo)
    {
        vida -= danoInimigo;
        if(vida == 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Vida>()?.LevarDano(dano);
        }
        
        if(collision.collider.CompareTag("Poder"))
        {
            Poder poderColidido = collision.collider.GetComponent<Poder>();
            Vida(poderColidido.dano);
        }

    }
}
