using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	#region Singleton

	static public Tutorial Instance;

	#endregion

	#region Engine methods

	void Awake()
	{
		Instance = this;
	}

	void Start ()
	{
		StartCoroutine(Learning());
	}

	#endregion

	#region Tutorial state

	public enum Step
	{
		Steering,
		Boost,
		Complete
	}

	Step m_CurrentStep;
	Step CurrentStep
	{
		get { return m_CurrentStep; }
		set
		{
			m_CurrentStep = value;
			if (m_OnStep != null)
			{
				m_OnStep(m_CurrentStep);
			}
		}
	}

	#endregion

	#region State callback

	public void OnStep(System.Action<Step> _Callback)
	{
		_Callback(CurrentStep);
		m_OnStep += _Callback;
	}

	event System.Action<Step> m_OnStep;

	#endregion

	#region Tutorial step logic

	IEnumerator Learning()
	{
		CurrentStep = Step.Steering;
		AsteroidGenerator.Instance.enabled = false;
		Score.Instance.enabled = false;

		while(Game.Instance.CurrentState != Game.State.Playing
			|| Controller.Steering == 0f)
		{ yield return new WaitForEndOfFrame(); }

		CurrentStep = Step.Boost;

		while(Game.Instance.CurrentState != Game.State.Playing
			|| !Controller.Boost)
		{ yield return new WaitForEndOfFrame(); }

		CurrentStep = Step.Complete;
		AsteroidGenerator.Instance.enabled = true;
		Score.Instance.enabled = true;

	}

	#endregion
}
