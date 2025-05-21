using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_morte : MonoBehaviour
{
    [SerializeField] string nomeLevel;

    public void VoltarMenu()
    {
        SceneManager.LoadScene(nomeLevel);
    }
}
