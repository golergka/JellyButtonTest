using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
	static public Tutorial Instance;

	void Awake()
	{
		Instance = this;
	}

	void Start ()
	{
		StartCoroutine(Learning());
	}

	public enum Step
	{
		Steering,
		Boost,
		Complete
	}

	Step m_CurrentStep;
	public Step CurrentStep
	{
		get { return m_CurrentStep; }
		set
		{
			m_CurrentStep = value;
			switch(value)
			{
				case (Step.Steering):
					if (m_OnStepSteering != null)
					{
						m_OnStepSteering();
					}
					break;

				case (Step.Boost):
					if (m_OnStepBoost != null)
					{
						m_OnStepBoost();
					}
					break;

				case (Step.Complete):
					if (m_OnStepComplete != null)
					{
						m_OnStepComplete();
					}
					break;
			}
		}
	}

	public void OnStepSteering(System.Action _Callback)
	{
		if (CurrentStep == Step.Steering)
		{
			_Callback();
		}
		m_OnStepSteering += _Callback;
	}

	public void OnStepBoost(System.Action _Callback)
	{
		if (CurrentStep == Step.Boost)
		{
			_Callback();
		}
		m_OnStepBoost += _Callback;
	}

	public void OnStepComplete(System.Action _Callback)
	{
		if (CurrentStep == Step.Complete)
		{
			_Callback();
		}
		m_OnStepComplete += _Callback;
	}

	event System.Action m_OnStepSteering;
	event System.Action m_OnStepBoost;
	event System.Action m_OnStepComplete;

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
		AsteroidGenerator.Instance.enabled = false;
		Score.Instance.enabled = false;

	}
}
