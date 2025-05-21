using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Player_Controller : MonoBehaviour
{

    [SerializeField] internal float velocidade;
    [SerializeField] internal float forcaPulo;
    [SerializeField] internal bool nochao;
    [SerializeField] internal bool nopulo;
    [SerializeField] internal LayerMask chao;
    [SerializeField] private float quedaMultiplier = 2.5f; 
    int puloDuplo = 0;
    bool podeDash = true;
    bool noDash;
    [SerializeField] internal float forcaDash = 24f;
    [SerializeField] internal float tempoDash = 0.2f;
    [SerializeField] internal float recuperaDash = 1f;
    [SerializeField] private GameObject bolaDeFogoPrefab;
    [SerializeField] private Transform pontoDisparo;
    [SerializeField] private float cooldownPoder = 1f;
    [SerializeField] string nomeLevel;
    [SerializeField] TrailRenderer tr;
    private bool podeAtirar = true;
    Transform verificaChao;
    Rigidbody2D rb;
    Vida vidaPlayer;
    Poder poder;
    private Animator anim;

    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] internal int vidaAtual;
    

    public bool travaDash = false;
    public bool travaPuloDuplo = false;

    int vida;



    //Carlos ==================================================================


    
    public float Speed => velocidade;
    public float Jump => forcaPulo;
    // public float Strong => strong;
    public float Power => poder.dano;

    //==============================================================================
    public bool Dash = true;
    public bool DoubleJump = true;

    void Awake()
    {
        verificaChao = GameObject.Find("VerificaChão").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //vida  = GetComponent<Vida>().vidaAtual;

    }
    
    
    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    public float Life => vidaAtual;

    // Update is called once per frame
    void Update()
    {
        if(noDash)
        {
            return;
        }
        if(Input.GetButtonDown("Jump") && nochao)
        {
            nopulo = true;
        }
        else if(Input.GetButtonDown("Jump") && nochao == false && puloDuplo <1)
        {
            if(travaPuloDuplo == false)
            {
                nopulo = true;
                puloDuplo++;
            }   
        }
        else if(Input.GetButtonUp("Jump") && rb.linearVelocityY > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.2f);
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && podeDash)
        {
            if(travaDash == false)
            {
                StartCoroutine(DashFun());
            }
            
        }
        if(Input.GetKeyDown(KeyCode.F) && podeAtirar)
        {
            anim.SetBool("tiro1", true);
            StartCoroutine(DispararPoder());
            //anim.SetBool("tiro1", false);
            
        }
        if(nochao == false)
        {
            //anim.SetBool("pulo", false);
        }
        //Soco();
    }
    void FixedUpdate()
    {
        if(nochao)
        {
            puloDuplo = 0;
        }
        Nochao();
        Movimentação();
        Pulo();
        if (rb.linearVelocityY < 0)
        {
            rb.linearVelocityY += Physics2D.gravity.y * (quedaMultiplier - 1) * Time.fixedDeltaTime;
        }
        

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

    void Soco()
    {
        anim.SetBool("soco", true);
    }
    void Movimentação()
    {
        anim.SetBool("andando", true);
        if(noDash)
        {
            return;
        }

        float inputX = Input.GetAxis("Horizontal");

        if (inputX != 0)
        {
            rb.linearVelocityX = inputX * velocidade;
        }
        else
        {
            rb.linearVelocityX = 0f;
            anim.SetBool("andando", false);
        }
        // Flip visual baseado na direção
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Direita
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Esquerda
        }

    }

    void Pulo()
    {
        if(nopulo)
        {
            anim.SetBool("pulo",true);
            rb.linearVelocity = new Vector2(rb.linearVelocityX, forcaPulo);
            if(nopulo && puloDuplo<1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, forcaPulo);
            }
            nopulo = false;
            
        }
    }
    void Nochao()
    {
        nochao = Physics2D.Linecast(verificaChao.position, transform.position, chao);
        Debug.DrawLine(verificaChao.position, transform.position, Color.blue);
        
        //puloDuplo = 0;
    }

    private IEnumerator DashFun()
    {
        podeDash = false;
        noDash = true;
        float gravidadeOriginal = rb.gravityScale;
        rb.gravityScale = 0f;
        float inputX = Input.GetAxisRaw("Horizontal");
        float dashDirection = inputX != 0 ? inputX : transform.localScale.x;
        rb.linearVelocity = new Vector2(dashDirection * forcaDash, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(tempoDash);
        tr.emitting = false;
        rb.gravityScale = gravidadeOriginal;
        noDash = false;
        yield return new WaitForSeconds(recuperaDash);
        podeDash = true;
    }

    private IEnumerator DispararPoder()
    {
        podeAtirar = false;

        GameObject bola = Instantiate(bolaDeFogoPrefab, pontoDisparo.position, Quaternion.identity);

        // Define direção com base na escala do personagem
        Vector2 direcao = transform.localScale.x < 0 ? Vector2.left : Vector2.right;

        // Configura a bola de fogo
        bola.GetComponent<Poder>().Configurar(direcao, 10f); // 10f = velocidade (pode expor no Inspector)
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("tiro1", false);

        yield return new WaitForSeconds(cooldownPoder);
        podeAtirar = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Poder_Inimigo"))
        {
            Poder_inimigo poderColidido = collision.collider.GetComponent<Poder_inimigo>();
            LevarDano(poderColidido.dano);
        }
        if(collision.collider.CompareTag("espinho"))
        {
            LevarDano(20);
        }
    }



    public void SetLife(int value) => vidaAtual = value;
    public void SetSpeed(float value) => velocidade = value;
    public void SetJump(float value) => forcaPulo = value;
    //public void SetStrong(float value) => strong = value;
    public void SetPower(int value) => poder.dano = value;
    public void SetDash(bool value) => travaDash = value;
    public void SetDoubleJump(bool value) => travaPuloDuplo = value;


}
