using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeCaster : MonoBehaviour
{
    public float damage = 50;

    public Rigidbody grenadePrefab;
    public Transform grenadeSourceTranshorm;

    public float force = 10;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var grenade = Instantiate(grenadePrefab);
            grenade.transform.position = grenadeSourceTranshorm.position;
            grenade.GetComponent<Rigidbody>().AddForce(grenadeSourceTranshorm.forward * force);
            grenade.GetComponent<Grenade>().damage = damage;
        }
    }
}
