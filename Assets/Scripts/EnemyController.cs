using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float maxSpeed = 1f;
    public float speed = 1f;
    public Transform target;
    public float life = 10f;
    public float damage = 0f;

    private Vector3 start, end;
    private Rigidbody2D rbd2;
    private Animator anim;
    private Collider2D collider;
    private float fixedSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if(target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }

        rbd2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider = GetComponent<Collider2D>(); 
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        if(target != null)
        {
            fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);

            if (transform.position == target.position)
            {

                if (target.position == start)
                {
                    //Cambiamos la posición del target
                    target.position = end;
                    // Cambiamos la orientación de la animación del enemigo
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    //Cambiamos la posición del target
                    target.position = start;
                    // Cambiamos la orientación de la animación del enemigo
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }

            }
        }

        

    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Spell") {
            anim.SetTrigger("Hurt");
            life--;
            if(life == 0) {
                anim.SetTrigger("Die");
                fixedSpeed = 0f;
                target = null;
                Destroy(rbd2, 0f);
                Destroy(collider, 0f);
            }

        }

        if(col.gameObject.tag == "Player")
        {
            col.SendMessage("EnemyHit", transform.position.x);
        }

    }


}
