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
			StartCoroutine(ScoreSound(Mathf.FloorToInt(value) - Current));
			m_CurrentFloat = value;
			if (Hi < Current)
			{
				Hi = Current;
				BrokenHigh = true;
			}
		}
	}

	IEnumerator ScoreSound(int _Amount)
	{
		while(_Amount > 0)
		{
			if (_Amount >= AsteroidScore)
			{
				audio.PlayOneShot(ClipAsteroid);
				_Amount -= AsteroidScore;
			}
			else
			{
				audio.PlayOneShot(ClipSingle);
				_Amount--;
			}
			yield return new WaitForSeconds(AudioDelay);
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
			CurrentFloat += Time.deltaTime * (Game.Instance.Boost ? PerBoostSecond : PerSecond);
			TimeElapsed += Time.deltaTime;
		}
	}

	public void AsteroidFlownBy()
	{
		CurrentFloat += AsteroidScore;
		Asteroids++;
	}
}
