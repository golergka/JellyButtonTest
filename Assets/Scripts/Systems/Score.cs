using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Score : MonoBehaviour
{
	public static Score Instance;
	void Awake()
	{
		Instance = this;
	}

	public int PerSecond = 1;
	public int PerBoostSecond = 2;

	public int Asteroids { get; private set; }
	public float TimeElapsed { get; private set; }

	public int Current
	{
		get { return Mathf.FloorToInt(CurrentFloat); }
	}
	public bool BrokenHigh { get; private set; }

	public AudioClip ClipSingle;
	public AudioClip ClipAsteroid;

	public int AsteroidScore = 5;

	public float AudioDelay = 0.200f;

	float m_CurrentFloat;
	float CurrentFloat
	{
		get { return m_CurrentFloat; }
		set
		{
			if (value > Current)
			{
				m_ScoreToPlay += Mathf.FloorToInt(value) - Current;
			}
			m_CurrentFloat = value;
			if (Hi < Current)
			{
				Hi = Current;
				BrokenHigh = true;
			}
		}
	}

	int m_ScoreToPlay;

	IEnumerator ScoreSound()
	{
		while(true)
		{
			if (m_ScoreToPlay > 0)
			{
				if (m_ScoreToPlay >= AsteroidScore)
				{
					audio.PlayOneShot(ClipAsteroid);
					m_ScoreToPlay -= AsteroidScore;
				}
				else
				{
					audio.PlayOneShot(ClipSingle);
					m_ScoreToPlay--;
				}
				yield return new WaitForSeconds(AudioDelay);
			}
			else
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}

	const string HIGHSCORE_KEY = "Highscore";
	public int Hi
	{
		get
		{
			return PlayerPrefs.GetInt(HIGHSCORE_KEY);
		}
		private set
		{
			PlayerPrefs.SetInt(HIGHSCORE_KEY, value);
		}
	}

	void Start ()
	{
		StartCoroutine(ScoreSound());
		Game.Instance.OnLaunch(delegate
		{	
			CurrentFloat = 0;
			Asteroids	= 0;
			TimeElapsed = 0;
			BrokenHigh = false;
		});
		Game.Instance.OnOver(() => PlayerPrefs.Save());
	}

	void Update()
	{
		if (Game.Instance.CurrentState == Game.State.Playing)
		{
			CurrentFloat += Time.deltaTime * (Controller.Boost ? PerBoostSecond : PerSecond);
			TimeElapsed += Time.deltaTime;
		}
	}

	public void AsteroidFlownBy()
	{
		CurrentFloat += AsteroidScore;
		Asteroids++;
	}
}
