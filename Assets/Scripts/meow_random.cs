using UnityEngine;
using System.Collections;

public class RandomMeow : MonoBehaviour
{
    public AudioClip meowClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(MeowRoutine());
    }

    IEnumerator MeowRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(15f, 40f);

            yield return new WaitForSeconds(waitTime);

            audioSource.PlayOneShot(meowClip);
        }
    }
}