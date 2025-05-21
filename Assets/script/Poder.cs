using UnityEngine;

public class Poder : MonoBehaviour  
{
    
    [SerializeField] private float velocidade;
    [SerializeField] internal int dano;
    private Vector2 direcao;

    public void Configurar(Vector2 direcao, float velocidade)
    {
        this.direcao = direcao.normalized;
        this.velocidade = velocidade;
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    void Start()
    {
        Destroy(gameObject, 3f); // Destrói após 3 segundos
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
