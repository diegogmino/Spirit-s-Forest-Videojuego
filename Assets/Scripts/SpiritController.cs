using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritController : MonoBehaviour

    
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public float health = 5f;
    public Transform spellSpawner;
    public GameObject spell;

    private Rigidbody2D rbd2;
    private Animator anim;
    private Collider2D collider;
    private bool jump;
    private bool doubleJump;
    private bool movement = true;
    

    // Start is called before the first frame update
    void Start() {
        rbd2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() {
        anim.SetFloat("Speed", Mathf.Abs(rbd2.velocity.x));
        anim.SetBool("Grounded", grounded);

        if(Input.GetKeyDown(KeyCode.Space)) {
            if(grounded) {
                jump = true;
                doubleJump = true;
            } else if(doubleJump) {
                jump = true;
                doubleJump = false;
            }      
        }

        SpiritSpell();

    }

    void FixedUpdate() {

        // Simulamos fricción manualmente
        Vector3 fixedVelocity = rbd2.velocity;
        fixedVelocity *= 0.75f;

        if(grounded) {
            rbd2.velocity = fixedVelocity;
        }

        float h = Input.GetAxis("Horizontal");
        // Forzamos a que no haya movimiento
        if (!movement) h = 0;

        rbd2.AddForce(Vector2.right * speed * h);

        //Variable que contiene el límite de velocidad que puede alcanzar el personaje
        float limitedSpeed = Mathf.Clamp(rbd2.velocity.x, -maxSpeed, maxSpeed);

        rbd2.velocity = new Vector2(limitedSpeed, rbd2.velocity.y);

        if(h > 0.1f) { //Comprobar si se pulsa para ir a la derecha
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if(h < -0.1f) { //Comprobar si se pulsa para ir a la izquierda
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if(jump) {
            rbd2.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }

    }

    void OnBecameInvisible() {
        transform.position = new Vector3(-6,0,0);
    }

    public void SpiritSpell() {
        // Ponemos a falso el ataque en bucle para que al pulsar dos veces seguidas el botón no se quede pillada la animación
        anim.SetBool("AttackSpell", false);

        if (Input.GetButtonDown("Fire1")) {  
            anim.SetBool("AttackSpell", true);
        }
    }

    public void ThrowSpell()
    {  
        movement = false;
        Instantiate(spell, spellSpawner.position, spellSpawner.rotation);
        Invoke("EnableMovement", 0.7f);   

    }

    public void EnemyHit(float enemyPosX)
    {

        health -= 1f;

        if (health == 0)
        {
            anim.SetTrigger("Die");
            rbd2.AddForce(Vector2.up * 0);

            Invoke("Respawn", 3f);

        }
        else
        {
            float side = Mathf.Sign(enemyPosX - transform.position.x);
            rbd2.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse); // Ejecutamos una fuerza para separar al jugador del enemigo
            anim.SetTrigger("Hit");
            movement = false;
            rbd2.isKinematic = true; // Deshabilitamos el RigidBody2D
            Invoke("EnableMovement", 0.7f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag== "Boss_Spell")
        {
            health -= 1f;
            EnemyHit(254.82f);
        }
        
    }

    

    void Respawn()
    {

        anim.SetTrigger("Respawn");
        transform.position = new Vector3(-6, -1, 0);
        health = 5f;
        
    }

    void EnableMovement()
    {
        movement = true;
        rbd2.isKinematic = false;
    }
}
