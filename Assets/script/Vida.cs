using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] internal int vidaAtual;
    [SerializeField] string nomeLevel;

    public bool EstaMorto => vidaAtual <= 0;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public void LevarDano(int dano)
    {
        vidaAtual -= dano;
        Debug.Log($"{gameObject.name} levou {dano} de dano. Vida restante: {vidaAtual}");

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    public void Curar(int quantidade)
    {
        vidaAtual = Mathf.Min(vidaAtual + quantidade, vidaMaxima);
        Debug.Log($"{gameObject.name} foi curado. Vida: {vidaAtual}");
    }

    private void Morrer()
    {   
        Debug.Log("Player morreu. Carregando cena: " + nomeLevel);
        SceneManager.LoadScene(nomeLevel);
        Debug.Log($"{gameObject.name} morreu!");
        // Aqui você pode chamar animação de morte, desativar o objeto, etc.
        Destroy(gameObject);
    }
}
