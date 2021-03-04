using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{

    private Rigidbody2D rbd2;
    private Animator anim;
    private Collider2D collider;
    private AudioManagerController amc;

    public Transform spellSpawner;
    public GameObject spell;

    public Image heart;
    public Sprite noHeart;
    public int numHeart;
    public RectTransform posFirstHeart;
    public Canvas canvas;
    public Canvas winCanvas;
    public Canvas spiritCanvas;
    public int offSet;
    public GameObject spirit;

    // Sounds
    public GameObject hurtSound;
    public GameObject spellSound;

    public float health = 20f;

    // Start is called before the first frame update
    void Start()
    {

        rbd2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        amc = FindObjectOfType<AudioManagerController>();

        heart.enabled = false;
        winCanvas.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHeart() {

        if(heart.enabled == false)
        {
            heart.enabled = true;

            Transform posHeart = posFirstHeart;

            for (int i = 0; i < numHeart; i++) {

                Image newHeart = Instantiate(heart, posHeart.position, Quaternion.identity);
                newHeart.transform.parent = canvas.transform;
                posHeart.position = new Vector2(posHeart.position.x + offSet, posHeart.position.y);

            }
        }
        
    }

    public void DisableHeart()
    {
        canvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Spell")
        {

            Instantiate(hurtSound);
            anim.SetTrigger("Hurt");
            health--;
            canvas.transform.GetChild(numHeart + 1).gameObject.GetComponent<Image>().sprite = noHeart;
            numHeart -= 1;
            Invoke("ReAttack", 0.5f);
            if (health == 0)
            {
                DisableHeart();
                anim.SetTrigger("Die");
                Destroy(rbd2, 0f);
                Destroy(collider, 0f);
                spiritCanvas.enabled = false;
                winCanvas.enabled = true;
                amc.ChangeMusicWin();
                spirit.GetComponent<SpiritController>().DisableMovement();
                
            }

        }

        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("EnemyHit", transform.position.x);
        }
    }

    public void ReAttack()
    {
        anim.SetTrigger("ReAttack");
    }

    public void Attack()
    {
        Invoke("Slash", 1f);
        
    }

    private void Slash()
    {
        anim.SetTrigger("Attack"); 
    }

    public void SpellThrow()
    {
        Instantiate(spell, spellSpawner.position, spellSpawner.rotation);
        Instantiate(spellSound);
    }

}
