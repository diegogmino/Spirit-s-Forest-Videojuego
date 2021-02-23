using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    private Rigidbody2D rbd2;
    private Animator anim;
    private Collider2D collider;

    public Transform spellSpawner;
    public GameObject spell;

    public float health = 20f;

    // Start is called before the first frame update
    void Start()
    {

        rbd2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Spell")
        {

            anim.SetTrigger("Hurt");
            health--;
            Invoke("ReAttack", 2f);
            if (health == 0)
            {
                anim.SetTrigger("Die");
                Destroy(rbd2, 0f);
                Destroy(collider, 0f);
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
    }

}
