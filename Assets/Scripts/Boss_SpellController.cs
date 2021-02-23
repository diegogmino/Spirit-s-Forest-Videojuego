using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SpellController : MonoBehaviour
{

    private Rigidbody2D spellRB;

    public float spellSpeed;
    public float spellLife;

    // Se llama a este método siempre antes del Start()
    void Awake()
    {

        spellRB = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        spellRB.velocity = new Vector2(-spellSpeed, spellRB.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(gameObject, spellLife);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
