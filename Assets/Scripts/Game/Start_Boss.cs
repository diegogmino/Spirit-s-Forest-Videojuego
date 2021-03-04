using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Start_Boss : MonoBehaviour
{

    private GameObject boss;
    private Animator bossAnimator;
    private AudioManagerController amc;


    // Start is called before the first frame update
    void Start()
    {

        boss = GameObject.FindGameObjectWithTag("Boss");
        bossAnimator = boss.GetComponent<Animator>();
        amc = FindObjectOfType<AudioManagerController>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            BossStart();                           
        }
    }

    private void BossStart() {

        bossAnimator.SetBool("Start", true);
        amc.ChangeMusicBoss();
        boss.GetComponent<BossController>().ShowHeart(); // Mostramos la vida del boss

    }

    public void BossStop()
    {
        bossAnimator.SetBool("Start", false);
    }
}
