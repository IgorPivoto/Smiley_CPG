using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_pausa : MonoBehaviour
{
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] string nomeLevel;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AbrirOpcoes();
        }
    }
    public void AbrirOpcoes()
    {
        painelOpcoes.SetActive(true);
        Time.timeScale = 0f;
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        Time.timeScale = 1f; 
    }
    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeLevel);
    }

}
