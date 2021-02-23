using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Boss : MonoBehaviour
{

    private GameObject boss;
    private Animator bossAnimator;

    // Start is called before the first frame update
    void Start()
    {

        boss = GameObject.FindGameObjectWithTag("Boss");
        bossAnimator = boss.GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            bossAnimator.SetBool("Start", true);
        }
    }
}
