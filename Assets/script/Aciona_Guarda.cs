using UnityEngine;

public class Aciona_Guarda : MonoBehaviour
{
    [SerializeField] GameObject guarda1;
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
            Guarda guarda = guarda1.GetComponent<Guarda>();
            guarda.ativar = true;
            Destroy(gameObject);
        }
        
        
    }
}
