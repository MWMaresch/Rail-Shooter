using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosionSpawner : MonoBehaviour {

    public GameObject SmallExplosion;
    
    // Use this for initialization
    void Start()
    {
        Instantiate(SmallExplosion, transform.position, transform.rotation);
        Instantiate(SmallExplosion, transform.position, transform.rotation);
        Instantiate(SmallExplosion, transform.position, transform.rotation);
        Instantiate(SmallExplosion, transform.position, transform.rotation);
        Instantiate(SmallExplosion, transform.position, transform.rotation);
        Instantiate(SmallExplosion, transform.position, transform.rotation);
        Instantiate(SmallExplosion, transform.position, transform.rotation);
    }    
}
