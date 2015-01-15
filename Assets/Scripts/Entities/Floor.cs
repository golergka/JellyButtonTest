using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{

	#region Configuration

	public float ScaleFactor = 1f;
	public AnimationCurve Cycle;

	#endregion

	#region Scroll state

	float m_Offset;

	#endregion

	#region Engine methods

	void Update ()
	{
		m_Offset += Time.deltaTime * Movement.Instance.Speed * ScaleFactor;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, m_Offset));
		renderer.material.SetTextureOffset("_SpecTex", new Vector2(0, m_Offset));
		renderer.material.SetTextureOffset("_NormalTex", new Vector2(0, m_Offset));
		renderer.material.SetTextureOffset("_EmissionTex", new Vector2(0, m_Offset));
		float emission = Cycle.Evaluate(Time.time);
		renderer.material.SetFloat("_Emission", emission);
	}

	#endregion
}
