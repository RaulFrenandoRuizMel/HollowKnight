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
    }

    // Update is called once per frame
    void Update()
    {
        Velocidad.y -= 60 *Time.deltaTime; 
        Velocidad.x = playerInput.actions["Move"].ReadValue<Vector2>().x * 4;

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
            saltos_restantes = 1;

            if (Velocidad.x == 0)
            {
                animator.Play("jugador_iddle");
            }
            else
            {
                animator.Play("jugador_caminando");
            }

            Velocidad.y = -1;
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                Velocidad.y = 10;
                animator.Play("jugador_saltar");
            }
        }
        else
        {
            if (playerInput.actions["Jump"].IsPressed())
            {
                Velocidad.y += 40 * Time.deltaTime;
            }
            
            if (Velocidad.y < -1)
            {
                animator.Play("jugador_cayendo");
            }
            else if(Velocidad.y < 0)
            {
                animator.Play("empezar_caida");
            }

            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                if(saltos_restantes>0)
                {
                    animator.Play("jugador_saltar");

                    Velocidad.y = 10;
                    saltos_restantes--;
                }
            }
        }

        characterController.Move(Velocidad*Time.deltaTime);
        this.transform.rotation = Quaternion.Euler(rotacion);
    }
}
