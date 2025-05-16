using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator _doorAnimator;

    public bool breakLeft = false;

    private bool _broken = false;

    public AudioSource breakAudioSource;
    public AudioClip breakSound;

    // Start is called before the first frame update
    void Start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    public void TryBreak(bool hitSide)
    {
        if (_broken || breakLeft != hitSide)
        {
            return;
        }

        _broken = true;
        _doorAnimator.SetBool("Destroy", true);

        // Reproducir el sonido de ruptura
        if (breakAudioSource != null && breakSound != null)
        {
            breakAudioSource.PlayOneShot(breakSound);
        }

        StartCoroutine(BreakWall());
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Weapon"))
        {
            Debug.Log("Enter collider");
            _doorAnimator.SetBool("Destroy", true);

            StartCoroutine(BreakWall());
        }
    }*/

    IEnumerator BreakWall()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
}
