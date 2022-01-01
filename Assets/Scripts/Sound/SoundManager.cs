using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public SoundParam[] sounds;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        Play("Ambiant_sound", this.gameObject);
        Play("The_wake_of_man", this.gameObject);
    }


    public void Play(string name, GameObject sourceObject)
    {
        try
        {
            SoundParam sp = Array.Find(sounds, sound => sound.name.Equals(name));


            //Si vide, on l'initialise (il va avoir la position de l'objet child)
            if (sp.audioSource == null)
            {

                sp.audioSource = sourceObject.AddComponent<AudioSource>();

                sp.audioSource.clip = sp.audioClip;
                sp.audioSource.volume = sp.volume;

                sp.audioSource.pitch = sp.pitch;
                sp.audioSource.loop = sp.loop;

                sp.audioSource.spatialBlend = sp.spatialBlend;

            }

            if (name.Equals("carriage"))
            {
                float rand = UnityEngine.Random.Range(0.95f, 1.0f);
                sp.audioSource.pitch = rand;
            }
            if (sp.loop == true)
            {
                sp.audioSource.Play();
            }
            {
                sp.audioSource.PlayOneShot(sp.audioClip);
            }
        }
        catch (NullReferenceException)
        {
            Debug.Log("Audio clip" + name + " non existant");
        }
    }
}
