using UnityEngine;

public class enemigo1 : MonoBehaviour
{
    float angulo;
    int vidas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angulo = 90;
        vidas = 2;
    }

    // Update is called once per frame
    void Update()
    {
        angulo += 300 * Time.deltaTime;
        this.transform.Translate(Vector3.up * Mathf.Sin(angulo * Mathf.Deg2Rad) * Time.deltaTime);
    }
    public void recibirDano()
    {
        vidas--;
        if(vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
