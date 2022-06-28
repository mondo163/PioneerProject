using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossTestingScript : MonoBehaviour
{
    public GameObject mb;

    private int count = 0;
    private Animator mbc;
    // Start is called before the first frame update
    void Start()
    {
        mbc = mb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (count == 0)
            {
                mbc.SetTrigger("PlayerDetected");
                count++;
            }
            else
            {
                mbc.SetTrigger("StatueDestroyed");
            }
        }
    }
}
