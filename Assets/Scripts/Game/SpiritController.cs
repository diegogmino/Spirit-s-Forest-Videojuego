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
    public Canvas youDiedCanvas;
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
    private Vector3 respawnPos;
    private GameObject boss;
    private Animator bossAnimator;
    private AudioManagerController amc;

    // Start is called before the first frame update
    void Start() {

        rbd2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        bossAnimator = boss.GetComponent<Animator>();
        amc = FindObjectOfType<AudioManagerController>();

        respawnPos = new Vector3(-6, -1, 0);
        youDiedCanvas.enabled = false;

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

    public void SpiritSpell() {
        // Ponemos a falso el ataque en bucle para que al pulsar dos veces seguidas el botón no se quede pillada la animación
        anim.SetBool("AttackSpell", false);

        if (Input.GetKeyDown(KeyCode.K)) {  
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
            Destroy(collider, 0f);
            movement = false;

            canvas.enabled = false;
            boss.GetComponent<BossController>().DisableHeart();
            youDiedCanvas.enabled = true;
            bossAnimator.SetBool("Start", false);
            amc.ChangeMusicDead();

        }
        else
        {    
            anim.SetTrigger("Hit");
            collider.enabled = false;
            Invoke("EnableCollider", 1.5f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag== "Boss_Spell")
        {
            EnemyHit(254.82f);
        }

        if (collision.gameObject.tag== "Checkpoint") {

            respawnPos = collision.transform.position;
            collision.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        }

        if(collision.gameObject.tag== "Death") {

            EnemyHit(collision.transform.position.y);
            gameObject.transform.position = respawnPos;

        }
        
    }

    void Respawn()
    {

        anim.SetTrigger("Respawn");
        transform.position = new Vector3(-6, -1, 0);
        health = 5f;
        
    }

    public void DisableMovement()
    {
        movement = false;
    }

    void EnableCollider()
    {
        collider.enabled = true;
    }

}
