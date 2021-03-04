using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public GameObject selectSound;
    public GameObject clickSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectSoundButton()
    {
        Instantiate(selectSound);
    }

    public void clickSoundButton()
    {
        Instantiate(clickSound);
    }

}
