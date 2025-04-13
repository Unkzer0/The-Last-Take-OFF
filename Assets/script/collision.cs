using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] AudioClip Crash;

    AudioSource Ad;

    void Start()
    {
        Ad = GetComponent<AudioSource>();    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            azorcrash();
        }
       
    }
    void azorcrash()
    {
        crashVFX.Play();
        AudioManager.instance.PlaySFX(AudioManager.instance.crashSFX);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<control>().enabled = false;
        Invoke("Reloadlevel", delay);
       
      
    }
    void Reloadlevel()
    {
        int currentlevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentlevel);
    }
    
}
