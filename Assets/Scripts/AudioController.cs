using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource audioSource;

    public GameObject[] ambients;
    public List<AudioSource> ambientSources;

    public AudioClip buttonClip;
    public AudioClip navClip;
    public AudioClip shiftClip;

    public float volume;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < ambients.Length; i++)
        {
            ambientSources.Add(ambients[i].GetComponent<AudioSource>());
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        audioSource.volume = volume;

        foreach (AudioSource ambientSource in ambientSources)
        {
            ambientSource.volume = volume;
        }
	}

    // Funtion that plays the navigation sound clip
    public void PlayNavClip ()
    {
        audioSource.PlayOneShot(navClip);
    }

    // Funtion that waits for a given time and then plays the button sound clip
    public void PlayButtonClip ()
    {
        audioSource.PlayOneShot(buttonClip);
    }

    public void PlayShiftClip ()
    {
        audioSource.PlayOneShot(shiftClip, 0.1f);
    }
}
