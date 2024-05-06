using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject particlePrefab;

    public void CreateParticles()
    {
        Instantiate(particlePrefab, transform.position, transform.rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pick Up"))
        {
            Destroy(particlePrefab);
        }
    }
}
