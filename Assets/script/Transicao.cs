using UnityEngine;
using UnityEngine.SceneManagement;

public class Transicao : MonoBehaviour
{
    [SerializeField] private GameObject cena1;
    [SerializeField] private GameObject cena2;
    [SerializeField] private GameObject cena3;
    [SerializeField] private GameObject cena4;
    [SerializeField] string nomeLevel;

    public void Cena1()
    {
        cena1.SetActive(false);
        cena2.SetActive(true);
    }
    public void Cena2()
    {
        cena2.SetActive(false);
        cena3.SetActive(true);
    }
    public void Cena4()
    {
        cena3.SetActive(false);
        SceneManager.LoadScene(nomeLevel);
    }
}
