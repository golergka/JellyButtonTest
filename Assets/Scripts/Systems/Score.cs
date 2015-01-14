using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public static Score Instance;
	void Awake()
	{
		Instance = this;
	}

	public int PerSecond = 1;
	public int PerBoostSecond = 2;

	public int Current
	{
		get { return Mathf.FloorToInt(CurrentFloat); }
	}

	float m_CurrentFloat;
	float CurrentFloat
	{
		get { return m_CurrentFloat; }
		set
		{
			m_CurrentFloat = value;
			if (Hi < Current)
			{
				Hi = Current;
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
		Game.Instance.OnLaunch(() => CurrentFloat = 0);
		Game.Instance.OnOver(() => PlayerPrefs.Save());
	}

	void Update()
	{
		if (Game.Instance.CurrentState == Game.State.Playing)
		{
			CurrentFloat += Time.deltaTime * (Game.Instance.Boost ? PerBoostSecond : PerSecond);
		}
	}

	public void AsteroidCought()
	{
		CurrentFloat += 5f;
	}
}
