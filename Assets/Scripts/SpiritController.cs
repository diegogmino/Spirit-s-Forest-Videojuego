using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritController : MonoBehaviour

    
{
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public float health = 5f;
    public Transform spellSpawner;
    public GameObject spell;
    public Image heart;
    public Sprite noHeart;
    public int numHeart;
    public RectTransform posFirstHeart;
    public Canvas canvas;
    public int offSet;
    // Sounds
    public GameObject spellSound;
    public GameObject jumpSound;
    public GameObject hurtSound;

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

        Transform posHeart = posFirstHeart;

        for (int i = 0; i < numHeart; i++) {

            Image newHeart = Instantiate(heart, posHeart.position, Quaternion.identity);
            newHeart.transform.parent = canvas.transform;
            posHeart.position = new Vector2(posHeart.position.x + offSet, posHeart.position.y);

        }

    }

    // Update is called once per frame
    void Update() {
        anim.SetFloat("Speed", Mathf.Abs(rbd2.velocity.x));
        anim.SetBool("Grounded", grounded);

        if(Input.GetKeyDown(KeyCode.Space)) {
            if(grounded) {
                jump = true;
                doubleJump = true;

                Instantiate(jumpSound);

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
        Instantiate(spell, spellSpawner.position, spellSpawner.rotation);
        Instantiate(spellSound);  

    }

    public void EnemyHit(float enemyPosX)
    {

        Instantiate(hurtSound);
        health -= 1f;
        canvas.transform.GetChild(numHeart + 1).gameObject.GetComponent<Image>().sprite = noHeart;
        
        numHeart -= 1;

        if (health == 0)
        {
            anim.SetTrigger("Die");
            rbd2.AddForce(Vector2.up * 0);

            Invoke("Respawn", 2f);

        }
        else
        {
            //float side = Mathf.Sign(enemyPosX - transform.position.x);
            //rbd2.AddForce((Vector2.left * side * jumpPower) * 5, ForceMode2D.Impulse); // Ejecutamos una fuerza para separar al jugador del enemigo
            anim.SetTrigger("Hit");
            //movement = false;
            //rbd2.isKinematic = true; // Deshabilitamos el RigidBody2D
            collider.enabled = false;
            Invoke("EnableMovement", 1.5f);
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
        //movement = true;
        //rbd2.isKinematic = false;
        collider.enabled = true;
    }

}
