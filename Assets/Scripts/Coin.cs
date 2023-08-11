using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Action OnCollected { set => onCollected.Add(value); }

    List<Action> onCollected = new List<Action>();

    [SerializeField] AudioClip onTake, onCollect;

    AudioSource src;

    public void TakeUp()
    {
        src = GetComponent<AudioSource>();
        src.PlayOneShot(onTake);
    }

    public void TakeDown()
    {
        src.PlayOneShot(onTake);
    }

    public void Collect()
    {
        foreach (var action in onCollected)
        {
            action();
        }

        StartCoroutine(PlaySound());
        gameObject.transform.localScale = Vector3.zero;
    }

    IEnumerator PlaySound()
    {
        src.PlayOneShot(onCollect);
        yield return new WaitForSeconds(onCollect.length);
        Destroy(gameObject);
    }


}
