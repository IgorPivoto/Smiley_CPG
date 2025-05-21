using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class Guarda : MonoBehaviour
{
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private int vida;
    [SerializeField] private int dano;
    [SerializeField] GameObject gatilho;
    [SerializeField] Transform jogador;
    [SerializeField] private GameObject bolaDeFogoPrefab;
    [SerializeField] private Transform pontoDisparo;
    [SerializeField] private float cooldownPoder = 1f;
    bool podeDano = false;
    bool jogadorNaÁrea = false;
    private bool podeAtirar = true;
    internal bool ativar = false;
    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").transform;
        //cl.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Seguir();

        if (ativar && jogadorNaÁrea && podeAtirar)
        {
            StartCoroutine(DispararPoder());
        }
    }

    void Seguir()
    {
        //cl.enabled = true;
        if (ativar && jogador != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, jogador.position, velocidade * Time.deltaTime);

            // Flip para o lado do jogador
            Vector3 escala = transform.localScale;
            if (transform.position.x < jogador.position.x)
                escala.x = Mathf.Abs(escala.x);
            else
                escala.x = -Mathf.Abs(escala.x);
            
            transform.localScale = escala;
        }
    }

    private IEnumerator DispararPoder()
    {
        podeAtirar = false;

        GameObject bola = Instantiate(bolaDeFogoPrefab, pontoDisparo.position, Quaternion.identity);

        // Define direção com base na escala do personagem
        Vector2 direcao = transform.localScale.x < 0 ? Vector2.left : Vector2.right;

        // Configura a bola de fogo
        bola.GetComponent<Poder_inimigo>().Configurar(direcao, 10f); // 10f = velocidade (pode expor no Inspector)

        yield return new WaitForSeconds(cooldownPoder);
        podeAtirar = true;
    }
    void Vida(int danoInimigo)
    {
        if(podeDano)
        {
            vida -= danoInimigo;
            if(vida <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Poder"))
        {
            Poder poderColidido = collision.collider.GetComponent<Poder>();
            Vida(poderColidido.dano);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            podeDano = true;
            jogadorNaÁrea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jogadorNaÁrea = false;
        }
        
    }
}
