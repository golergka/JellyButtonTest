using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Text))]
public class UITutorial : MonoBehaviour
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
			switch(_Current)
			{
				case(Tutorial.Step.Steering):
					gameObject.SetActive(true);
					Text.text = "Press A or left to steer left, and D or right to steer right";
					break;
				case(Tutorial.Step.Boost):
					gameObject.SetActive(true);
					Text.text = "Press Space to activate the boost";
					break;
				case(Tutorial.Step.Complete):
					gameObject.SetActive(false);
					break;
			}
		});
	}

	#endregion
}
