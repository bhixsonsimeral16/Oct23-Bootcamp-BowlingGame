using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource ballSound, uiSound, pinSound;
    [SerializeField] private AudioClip ballThrowClip, ballRollingClip, pinCollisionClip, spareClip, strikeClip, thudClip;

    public void PlaySound(string soundName)
    {
        switch(soundName)
        {
            case "ballThrow":
                ballSound.PlayOneShot(ballThrowClip);
                break;

            case "ballRolling":
                ballSound.loop = true;
                ballSound.clip = ballRollingClip;
                ballSound.Play();
                break;

            case "pinCollision":
                ballSound.loop = false;
                ballSound.Stop();
                pinSound.PlayOneShot(pinCollisionClip);
                break;

            case "spare":
                uiSound.PlayOneShot(spareClip);
                break;

            case "strike":
                uiSound.PlayOneShot(strikeClip);
                break;
            
            case "thud":
                ballSound.loop = false;
                ballSound.Stop();
                ballSound.PlayOneShot(thudClip);
                break;

            default:
                Debug.Log("Sound not found");
                break;
        }
    }
}
