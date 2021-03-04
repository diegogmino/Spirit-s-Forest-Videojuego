using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private SpiritController spirit;

    // Start is called before the first frame update
    void Start()
    {
        spirit = GetComponentInParent<SpiritController>();
    }

    void OnCollisionStay2D(Collision2D col) {

        if(col.gameObject.tag == "Death") {
            spirit.transform.position = new Vector3(-6, -1, 0);
        }

        if(col.gameObject.tag == "Ground") {
            spirit.grounded = true;
        }
        
    }

    void OnCollisionExit2D(Collision2D col) {

        if(col.gameObject.tag == "Ground") {
            spirit.grounded = false;
        }
    }
    
}
