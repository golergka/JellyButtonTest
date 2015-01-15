using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class UIScore : MonoBehaviour
{
	#region Component properties

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

	#endregion

	#region Engine methods

	void Start()
	{
		Tutorial.Instance.OnStep(delegate(Tutorial.Step _Current)
		{
			gameObject.SetActive(_Current == Tutorial.Step.Complete);
		});
	}

	void Update ()
	{
		var t = System.TimeSpan.FromSeconds(Score.Instance.TimeElapsed);
		Text.text = "Score: " + Score.Instance.Current.ToString()
			+ " Highest: " + Score.Instance.Hi.ToString()
			+ " Asteroids: " + Score.Instance.Asteroids
			+ " Time played: " + string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
	}

	#endregion
}
