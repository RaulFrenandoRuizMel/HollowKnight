using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;
    Vector3 Velocidad;
    Vector3 rotacion;
    Animator animator;
    int saltos_restantes;
    int VIDA;
    float is_grounded;

    public float tiempoInvulnerable;

    //Estados

    float contadorDash;
    float cooldowDash;
    float contadorAtaque;
    float retrocesoHitX;

    //ataque
    [SerializeField] GameObject prefabHitboxAtaque;
    [SerializeField] Transform lugarCreacionHitboxAtaque;
    [SerializeField] JugadorDestello destello;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        playerInput = this.GetComponent<PlayerInput>();
        Velocidad = Vector3.zero;
        rotacion = Vector3.zero;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        //Application.targetFrameRate = 30;
        saltos_restantes = 1;
        is_grounded = 0;

        //Estados
        contadorAtaque = 0;
        contadorDash = 0;
        cooldowDash = 0;
        VIDA = 3;

        tiempoInvulnerable = 0;
        retrocesoHitX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Velocidad.y -= 60 *Time.deltaTime;
        if (contadorDash <= 0)
        {
            if (retrocesoHitX == 0)
            {

                Velocidad.x = playerInput.actions["Move"].ReadValue<Vector2>().x * 4;
            }
        }
        else 
        {
            Velocidad.y = 0;
        }
        //------------------------------------------Dash
        
      
        if(Velocidad.x > 0)
        {
            rotacion.y = 0;
        }

        if(Velocidad.x < 0)
        {
            rotacion.y = 180;
        }

        if(characterController.isGrounded)
        {
            is_grounded = 0.15f;
        }

        is_grounded -= Time.deltaTime;

        if (is_grounded > 0)
        {
            saltos_restantes = 1;

            if (contadorDash <= 0 && contadorAtaque <= 0)
            {
                if (Velocidad.x == 0)
                {
                    animator.Play("jugador_iddle");
                }
                else
                {
                    animator.Play("jugador_caminando");
                }
            }


            Velocidad.y = -1;
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                Debug.Log("salta");
                Velocidad.y = 10;
                is_grounded = 0;
                animator.Play("jugador_saltar");
            }
        }
        else
        {
            if (playerInput.actions["Jump"].IsPressed())
            {
                Velocidad.y += 40 * Time.deltaTime;
            }

            if (contadorAtaque <= 0)
        { 
            if (Velocidad.y < -1)
            {
                animator.Play("jugador_cayendo");
            }
            else if (Velocidad.y < 0)
            {
                animator.Play("empezar_caida");
            }
        }

            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                if(saltos_restantes>0)
                {
                    animator.Play("jugadpor_dobleSalto");

                    Velocidad.y = 10;
                    saltos_restantes--;
                }
            }
        }

        cooldowDash -= Time.deltaTime;

        if (playerInput.actions["Sprint"].WasPressedThisFrame())
        {
            animator.Play("jugador_dash");

            if (cooldowDash <= 0)
            {
                contadorDash = 0.2f;
                if (rotacion.y == 0)
                {
                    Velocidad.x = 20;
                }
                else
                {
                    Velocidad.x = -20;
                }
                cooldowDash = 0.3f;
            }
        }
        contadorDash -= Time.deltaTime;

        //------------------------------------------attack
        if (playerInput.actions["Attack"].WasPressedThisFrame())
        {
            if (contadorAtaque <= 0)
            {
                animator.Play("jugador_ataque");
                contadorAtaque = 0.2f;
                Instantiate(prefabHitboxAtaque, lugarCreacionHitboxAtaque.position, Quaternion.identity);
            }
        }
        contadorAtaque -= Time.deltaTime;

        tiempoInvulnerable -= Time.deltaTime;

        //empujon

        if(retrocesoHitX > 0)
        {
            retrocesoHitX -= Time.deltaTime * 10;

            if(retrocesoHitX <0)
            {
                retrocesoHitX = 0;
            }
        }

        if (retrocesoHitX < 0)
        {
            retrocesoHitX += Time.deltaTime * 10;

            if (retrocesoHitX > 0)
            {
                retrocesoHitX = 0;
            }
        }
        characterController.Move(Velocidad*Time.deltaTime);
        this.transform.rotation = Quaternion.Euler(rotacion);

        Debug.DrawRay(this.transform.position + Vector3.up * .5f, Vector3.up * .8f, Color.purple);

        RaycastHit hit;

        if(Physics.Raycast(this.transform.position + Vector3.up * .5f, Vector3.up, out hit, .8f))
        {
            Velocidad.y = -1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "dano")
        {
            if (tiempoInvulnerable  <= 0)
            {
                destello.ActivarDestello();
                VIDA--;
                tiempoInvulnerable = 1.5f;
                 //empujon
                 if(other.gameObject.transform.position.x > this.transform.position.x)
                {
                    retrocesoHitX = -5;
                }
                 else
                {
                    retrocesoHitX = 5;

                }
                Velocidad.x = retrocesoHitX;
                Velocidad.y = 10;
                is_grounded = 0;
                characterController.Move(Vector3.up * 0.2f);

                if (VIDA <= 0)
                {

                    Destroy(this.gameObject);
                }
            }
        }
    }
}
