using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class UITutorial : MonoBehaviour
{
	Text m_Text;
	Text Text
	{
		get
		{
			if (m_Text == null)
			{
				m_Text = GetComponent<Text>();
			}
			return m_Text;
		}
	}

	void Start()
	{
		Tutorial.Instance.OnStepSteering(delegate
		{
			Debug.Log("Steering");
			gameObject.SetActive(true);
			Text.text = "Press A or left to steer left, and D or right to steer right";
		});

		Tutorial.Instance.OnStepBoost(delegate
		{
			gameObject.SetActive(true);
			Text.text = "Press Space to activate the boost";
		});
		
		Tutorial.Instance.OnStepComplete(delegate
		{
			gameObject.SetActive(false);
		});
	}
}
