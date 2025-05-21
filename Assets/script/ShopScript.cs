using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class ShopScript : MonoBehaviour
{
    public static ShopScript Instance;

    public Player_Controller player;

    public List<CardData> todasAsCartas = new List<CardData>();
    public GameObject[] slotsVisiveis;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetValoresIniciais();
        SortearCartas();
    }

    void SetValoresIniciais()
    {
        foreach (var carta in todasAsCartas)
        {
            if (carta.ativo)
                AtualizarPlayerComCarta(carta);
        }
    }


    public void AtualizarPlayerComCarta(CardData carta)
    {
        switch (carta.id)
        {
            case "speed":
                player.SetSpeed(carta.valorAtual);
                break;
            case "life":
                player.SetLife(carta.valorAtual);
                break;
            case "jump":
                player.SetJump(carta.valorAtual);
                break;
            case "doubleJump":
                player.SetDoubleJump(carta.valorAtual > 0);
                break;
            case "dash":
                player.SetDash(carta.valorAtual > 0);
                break;
        }
    }

    public void AplicarNerf(CardData carta)
    {
        if (!carta.ativo) return;

        if (carta.id == "doubleJump" || carta.id == "dash")
        {
            carta.valorAtual = (carta.valorAtual > 0) ? 0 : 1;
        }
        else if (carta.valorAtual > carta.valorMinimo)
        {
            carta.valorAtual -= carta.passosDeNerf;
            if (carta.valorAtual < carta.valorMinimo)
                carta.valorAtual = carta.valorMinimo;
        }
        else
        {
            Debug.Log($"{carta.nome} já atingiu o nível mínimo!");
            return;
        }

        AtualizarPlayerComCarta(carta);

        if (carta.valorAtual <= carta.valorMinimo)
        {
            carta.ativo = false;
        }
    }

void EmbaralharSlots()
{
    for (int i = 0; i < slotsVisiveis.Length; i++)
    {
        int randomIndex = Random.Range(i, slotsVisiveis.Length);
        
        var temp = slotsVisiveis[i];
        slotsVisiveis[i] = slotsVisiveis[randomIndex];
        slotsVisiveis[randomIndex] = temp;
    }

    Debug.Log("Slots embaralhados.");
}


public void SortearCartas()
{
    Debug.Log("Sorteando cartas...");

    List<CardData> cartasDisponiveis = todasAsCartas.FindAll(c => c.ativo);
    Debug.Log("Cartas disponíveis: " + cartasDisponiveis.Count);

    List<CardData> cartasSorteadas = cartasDisponiveis.OrderBy(c => Random.value).Take(3).ToList();

    EmbaralharSlots();

    for (int i = 0; i < slotsVisiveis.Length; i++)
    {
        if (i < cartasSorteadas.Count)
        {
            Debug.Log("Ativando slot " + i + " com carta " + cartasSorteadas[i].nome);
            slotsVisiveis[i].SetActive(true);
        }
        else
        {
            Debug.Log("Desativando slot " + i);
            slotsVisiveis[i].SetActive(false);
        }
    }
}



}