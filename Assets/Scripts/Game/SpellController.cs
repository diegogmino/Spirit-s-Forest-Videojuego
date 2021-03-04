using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    
    private Rigidbody2D spellRB;
    private GameObject spirit;
    private Transform spiritTransform;
    
    public float spellSpeed;
    public float spellLife;

    // Se llama a este método siempre antes del Start()
    void Awake()
    {

        spellRB = GetComponent<Rigidbody2D>();
        spirit = GameObject.FindGameObjectWithTag("Player");
        spiritTransform = spirit.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Si el jugador está mirando hacia la derecha, el spell irá tendrá una velocidad positiva
        if (spiritTransform.localScale.x > 0)
        {
            spellRB.velocity = new Vector2(spellSpeed, spellRB.velocity.y);
        }
        // Si el jugador está mirando hacia la izquierda, el spell tendrá una velocidad negativa
        else
        {
            spellRB.velocity = new Vector2(-spellSpeed, spellRB.velocity.y);
        }
        

    }

    // Update is called once per frame
    void Update()
    {

        Destroy(gameObject, spellLife);

    }


    private void OnTriggerEnter2D(Collider2D col) {
        if((col.gameObject.tag == "Enemy") || (col.gameObject.tag == "Boss")) {
            Destroy(gameObject);
        }
    }
}
    
