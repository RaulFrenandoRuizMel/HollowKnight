using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;
    Vector3 Velocidad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        playerInput = this.GetComponent<PlayerInput>();
        Velocidad = Vector3.zero;

        Application.targetFrameRate = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Velocidad.y -= 40 *Time.deltaTime; 
        Velocidad.x = playerInput.actions["Move"].ReadValue<Vector2>().x * 4;

        if(characterController.isGrounded)
        {
            Velocidad.y = -1;
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                Velocidad.y = 10;
            }
        }

        characterController.Move(Velocidad*Time.deltaTime);
    }
}
