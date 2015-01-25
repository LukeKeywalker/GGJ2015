using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour
{
	private string m_pathInResources;
	private AudioClip m_audioClip;
	private AudioSource m_audioSource;
	
	void Awake()
	{
		m_audioSource = GetComponent<AudioSource>();
	}
	
	public void Initialize(string pathInResources, bool loop, float volume)
	{
		m_pathInResources = pathInResources;

		m_audioSource.loop = loop;

		Load ();
	}
	
	public void Load()
	{
		if (m_audioClip != null)
			return;
		
		m_audioClip = Resources.Load<AudioClip>(m_pathInResources);
		
		if (m_audioClip == null)
			Debug.LogError("Couldn't load audio file " + m_pathInResources);
		
		m_audioSource.clip = m_audioClip;
	}
	
	public void Unload()
	{
		if (m_audioClip == null)
			return;
		
		m_audioSource.clip = null;
		
		Resources.UnloadAsset(m_audioClip);
		m_audioClip = null;
	}
	
	public void Play()
	{
		if (m_audioClip == null)
		{
			Debug.LogError("Audio clip " + m_pathInResources + " is not loaded");
			return;
		}
		
		m_audioSource.Play();
	}
	
	public void Stop()
	{
		if (m_audioClip == null)
		{
			Debug.LogError("Audio clip " + m_pathInResources + " is not loaded");
			return;
		}
		
		m_audioSource.Stop();
	}

}
