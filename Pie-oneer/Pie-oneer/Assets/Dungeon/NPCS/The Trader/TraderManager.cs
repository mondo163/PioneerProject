using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderManager : Action
{
    public List<GameObject> traderDialogue; // 0th is starting dial, 1th is need to pay dial, 2th is trading dial
    public GameObject trader;
    public int currentDialogue = 1;
    public GameObject smokeEffect;
    public AudioClip smokeEffectClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
       audioSource = GetComponent<AudioSource>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateNextDiaglogue(int indexNum)
    {
        if(indexNum < traderDialogue.Count)
        {
            traderDialogue[indexNum].SetActive(true);
        }
    }

    public IEnumerator TraderDisappears()
    {
        audioSource.PlayOneShot(smokeEffectClip);
        Instantiate(smokeEffect, trader.transform.position, Quaternion.identity);

        yield return new WaitForSeconds((float)0.2);

        Destroy(trader);

        yield return new WaitForSeconds((float)3);

        Destroy(gameObject);
    }

    public override void DoSomething() // when the trader is insulted then they disappear
    {
        StartCoroutine(TraderDisappears());
    }
}
