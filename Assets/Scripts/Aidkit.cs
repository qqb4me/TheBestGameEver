using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aidkit : MonoBehaviour
{
    public float healAmount = 50;

    public AudioSource _audioSourse;

    private void OnTriggerEnter(Collider other)
    {
        var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            _audioSourse.Play();
            playerHealth.AddHealth(healAmount);
            Destroy(gameObject);
        }
    }

    
}
