using UnityEngine;

public class teletransporte : MonoBehaviour
{
    [SerializeField] Transform pontoinicial;
    [SerializeField] Transform jogador;

    ShopScript shop;

    public GameObject canvas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            jogador.transform.position = pontoinicial.transform.position;
            canvas.SetActive(true);
            shop.SortearCartas();

        }
    }
}
