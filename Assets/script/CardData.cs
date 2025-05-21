using UnityEngine;

[System.Serializable]
public class CardData
{
    public string id;
    public string nome;
    public int valorAtual;
    public int valorMinimo;
    public int passosDeNerf = 1;
    public bool ativo = true;
}