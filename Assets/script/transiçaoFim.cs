using UnityEngine;
using UnityEngine.SceneManagement;

public class transiçaoFim : MonoBehaviour
{

    [SerializeField] private GameObject cena1;
    [SerializeField] private GameObject cena2;
    public void Cena1()
    {
        cena1.SetActive(false);
        cena2.SetActive(true);
    }
    public void Cena2()
    {
        cena2.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
