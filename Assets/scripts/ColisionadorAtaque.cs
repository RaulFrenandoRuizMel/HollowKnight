using UnityEngine;

public class ColisionadorAtaque : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="enemigo")
        {
            enemigo enemigo = other.gameObject.GetComponent<enemigo>();
            enemigo.recibirDano();
        }
    }
}
