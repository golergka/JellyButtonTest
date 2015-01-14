using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScore : MonoBehaviour
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

	void Update ()
	{
		Text.text = "Score: " + Score.Instance.Current.ToString() + " Highest: " + Score.Instance.Hi.ToString();
	}
}
