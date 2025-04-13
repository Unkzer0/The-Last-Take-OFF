using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    [SerializeField] GameObject deathVFXandSFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int enemylevel = 10;
    [SerializeField] int hitpoint = 10;
    

    score scoreboard;
    GameObject parent;
    void Start()
    {
        scoreboard = FindAnyObjectByType<score>();
        parent = GameObject.FindWithTag("SpawnAtRuntime");
        addrigdbody();
    }

    private void addrigdbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        
        
        if(hitpoint < 1)
        {
            processhit();
            killenemy();
        }
        else
        {
            hits();
        }
       
    }

  
    void processhit()
    {
        scoreboard.updatescore(enemylevel);
    }
     void killenemy()
{
    AudioManager.instance.PlaySFX(AudioManager.instance.enemyExplosionSFX);
    GameObject FX = Instantiate(deathVFXandSFX, transform.position, Quaternion.identity);
    FX.transform.parent = parent.transform;
    Destroy(gameObject);
}


    void hits()
    {
        hitpoint--;
        GameObject VFX = Instantiate(hitVFX, transform.position, Quaternion.identity);
        VFX.transform.parent = parent.transform;
    }

}