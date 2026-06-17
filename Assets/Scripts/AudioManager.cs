using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;

	public Sound[] sounds;

	void Start ()
	{


		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}

	public void Play (string sound)
	{
		if(GameManager.mute)
			return;

		Sound s = Array.Find(sounds, item => item.name == sound);
		s.source.Play();
	}
    public void Pause(string sound)
    {
        if (GameManager.mute)
            return;

        Sound s = Array.Find(sounds, item => item.name == sound);
        s.source.Pause();
    }

}
