using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardUI : MonoBehaviour, IPointerClickHandler
{
    public CardData dadosDaCarta;
    public Text nomeTexto;
    public Text valorTexto;

    public GameObject canvas;

    public void SetCard(CardData novaCarta)
    {
        dadosDaCarta = novaCarta;
        AtualizarVisual();
    }

    public void AtualizarVisual()
    {
        if (dadosDaCarta != null)
        {
            nomeTexto.text = dadosDaCarta.nome;
            valorTexto.text = dadosDaCarta.valorAtual.ToString("0.0");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AplicarCarta();
        canvas.SetActive(false);
    }


    public void AplicarCarta()
    {
        if (!dadosDaCarta.ativo) return;

        if (dadosDaCarta.valorAtual > dadosDaCarta.valorMinimo)
        {
            dadosDaCarta.valorAtual -= dadosDaCarta.passosDeNerf;

            Debug.Log("O valor da carta é" + dadosDaCarta.valorAtual);

            if (dadosDaCarta.valorAtual <= dadosDaCarta.valorMinimo)
            {
                dadosDaCarta.valorAtual = dadosDaCarta.valorMinimo;
                dadosDaCarta.ativo = false;
            }

            AtualizarVisual();
            ShopScript.Instance.AtualizarPlayerComCarta(dadosDaCarta);
        }
    }
}