using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCaster : MonoBehaviour
{
    public float damage = 10;

    public AudioSource _audioSourse;

    public Fireball fireballPrefab;
    public Transform fireballSourceTransform;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var fireball = Instantiate(fireballPrefab, fireballSourceTransform.position, fireballSourceTransform.rotation);
            fireball.damage = damage;
            _audioSourse.Play();
        }
    }
   
    void Start()
    {
        
    }

    
    
}
