using UnityEngine;

public class JugadorDestello : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer JugadorSpriteRenderer;
    [SerializeField] GameObject prefabDestello;
    Color color;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        color= Color.white;
        color.a = 0;
        spriteRenderer.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        color.a -= Time.deltaTime * 2;
        spriteRenderer.color = color;
        spriteRenderer.sprite = JugadorSpriteRenderer.sprite;
    }

    public void ActivarDestello()
    {
        Instantiate(prefabDestello, this.transform.position, Quaternion.identity);
        color.a = 1;
    }
}
