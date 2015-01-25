using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	public Sound m_soundPrefab;
	
	public class Sounds
	{
		public static Sound Insect;
		public static Sound Plop;
		public static Sound RockSlide;
		public static Sound Splash;
		public static Sound Waterfall;
		public static Sound WilhelmScream;
		public static Sound Blood;
	}
	
	private void InitializeSounds()
	{
		m_soundsFilter = transform.FindChild("Sounds");
		
		Sounds.Insect = InitSound("sounds/insect", "Insect", 1.0f);
		Sounds.Plop = InitSound ("sounds/plop", "Plop", 1.0f);
		Sounds.RockSlide = InitSound ("sounds/rockslide", "Rock Slide", 1.0f);
		Sounds.Splash = InitSound ("sounds/splash", "Splash", 1.0f);
		Sounds.Waterfall = InitSound ("sounds/waterfall", "Waterfall", 0.15f);
		Sounds.WilhelmScream = InitSound ("sounds/WilhelmScream", "Wilhelm Scream", 1.0f);
		Sounds.Blood = InitSound ("sounds/blood", "Blood", 1.0f);
	}
	
	private Sound InitSound(string pathInResources, string name, float volume, bool loop = false)
	{
		Sound sound = (Sound)GameObject.Instantiate(m_soundPrefab);
		sound.transform.parent = m_soundsFilter;
		sound.Initialize(pathInResources, loop, volume);
		sound.gameObject.name = name;
		
		m_sounds.Add(sound);
		
		return sound;
	}
	
	public enum Music
	{
		Menu,
		Race
	}
	

	public AudioSource m_music;

	private Transform m_soundsFilter;
	
	private float m_musicVolume = 1;
	private float m_soundVolume = 1;
	
	private static List<Sound> m_sounds = new List<Sound>();
	
	private static AudioManager m_instance;
	
	public static AudioManager Instance
	{
		get { return m_instance; }
	}
	
	public float SoundVolume
	{
		get
		{
			return m_soundVolume;
		}
		
		set
		{
			m_soundVolume = value;			
		}
	}
	
	public float MusicVolume
	{
		get
		{
			return m_musicVolume;
		}
		
		set
		{
			m_musicVolume = value;
			m_music.volume = value;
		}
	}
	
	void Awake()
	{
		//DontDestroyOnLoad(this);
		
		//if (m_instance != null)
//			throw new UnityException("AudioManager.Awake() is called second time");
		
		m_instance = this;
		
		InitializeSounds();
	}
	
	public void PlayMusic(float timeOffset = 0.0f)
	{
		m_music.time = timeOffset;
		m_music.Play();
	}
	
	public Sound GetSoundByName(string soundName)
	{
		foreach (var sound in m_sounds)
			if (sound.name == soundName)
				return sound;
		
		return null;
	}
	
	public void MuteMusicForSeconds(float seconds, float volume)
	{
		StartCoroutine(MuteMusicForSecondsCoroutine(seconds, volume));
	}
	
	private IEnumerator MuteMusicForSecondsCoroutine(float seconds, float volume)
	{
		if (m_music != null)
		{
			float baseVolume = m_music.volume;
			float attackDecayTime = seconds * 0.3f;
			float timeElapsed = 0;
			
			while (timeElapsed < attackDecayTime)
			{
				timeElapsed += Time.deltaTime;
				m_music.volume = Mathf.Lerp(baseVolume, volume, timeElapsed / attackDecayTime);
				yield return null;
			}
			
			yield return new WaitForSeconds(seconds - 2.0f * attackDecayTime);
			
			timeElapsed = 0;
			while (timeElapsed < attackDecayTime)
			{
				timeElapsed += Time.deltaTime;
				m_music.volume = Mathf.Lerp(volume, baseVolume, timeElapsed / attackDecayTime);
				yield return null;
			}
			
		}
		
		yield return null;
	}
	
	public void StopAll()
	{
		foreach (var sound in m_sounds)
			sound.Stop();
	}
	
	public void FadeInMusic(Music music, float time)
	{
		StartCoroutine(FadeCoroutine(music, 1.0f / time, -1.0f));
	}
	

	public void FadeOutMusic(Music music, float time)
	{
		StartCoroutine(FadeCoroutine(music, 1.0f / time, 1.0f));
	}
	
	private IEnumerator FadeCoroutine(Music music, float fadeSpeed, float direction)
	{
		AudioSource musicSource = m_music;
		
		musicSource.volume = direction == 1.0f ? 0.0f : 1;
		
		while (true)
		{
			if (direction == 1.0f && musicSource.volume == 1)
				yield break;
			
			if (direction == -1.0f && musicSource.volume == 0.0f)
				yield break;
			
			musicSource.volume = Mathf.Clamp(musicSource.volume += direction * fadeSpeed * Time.deltaTime, 0, 1);
			
			yield return null;
		}
	}
	
}