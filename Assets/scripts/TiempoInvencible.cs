using UnityEngine;

public class TiempoInvencible : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
    Color Color;
    float contadorParpadeo;
    Jugador jugador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Color = Color.white;
        contadorParpadeo = 0;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador.tiempoInvulnerable > 0)
        {
            contadorParpadeo += Time.deltaTime;
            if (contadorParpadeo > 0.1f)
            {
                contadorParpadeo = 0;
                if (Color.a == 1)
                {
                    Color.a = 0.5f;
                }
                else
                {
                    Color.a = 1;
                }
                SpriteRenderer.color = Color;
            }
        }
        else
        {
            Color.a = 1;
            SpriteRenderer.color = Color;
        }
    }
}
