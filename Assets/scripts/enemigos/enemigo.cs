using UnityEngine;
using UnityEngine.Events;

public class enemigo : MonoBehaviour
{
    [SerializeField] UnityEvent eventoRecibirDano;
    public void recibirDano()
    {
        eventoRecibirDano.Invoke();
    }
}
