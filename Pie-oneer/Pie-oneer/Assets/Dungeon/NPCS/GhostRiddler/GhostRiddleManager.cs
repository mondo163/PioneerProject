using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRiddleManager : MonoBehaviour
{
    public List<GameObject> ghostDialogues;
    public GameObject ghost;
    public int currentRiddle = 1;
    public bool allRiddlesCorrect = true;
    public GameObject grandPrize;
    public GameObject torchForDarkness;
    public GameObject smokeEffect;
    public AudioClip grandPrizeClip;
    public AudioClip smokeEffectClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDialogue(int riddleNum, bool wasCorrect)
    {
        currentRiddle = riddleNum + 1;

        if(allRiddlesCorrect)
            allRiddlesCorrect = wasCorrect;

        if(ghostDialogues.Count < currentRiddle)
        {
            //All Dialogues completed
            StartCoroutine(RiddlesCompleted());
        }
        else
        {
            ghostDialogues[currentRiddle - 1].SetActive(true);
        }
    }

    private IEnumerator RiddlesCompleted()
    {
        if (allRiddlesCorrect)
        {
            audioSource.PlayOneShot(grandPrizeClip);
            grandPrize.SetActive(true);
            yield return new WaitForSeconds((float)0.2);
        }

        audioSource.PlayOneShot(smokeEffectClip);
        Instantiate(smokeEffect, torchForDarkness.transform.position, Quaternion.identity);
        Instantiate(smokeEffect, ghost.transform.position, Quaternion.identity);

        yield return new WaitForSeconds((float)0.2);

        Destroy(ghost);
        torchForDarkness.SetActive(true);
    }
}
