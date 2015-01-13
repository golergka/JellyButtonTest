using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour
{
	float m_Offset;

	public float ScaleFactor = 1f;

	// Update is called once per frame
	void Update ()
	{
		m_Offset += Time.deltaTime * Game.Instance.Speed * ScaleFactor;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, m_Offset));
		renderer.material.SetTextureOffset("_SpecTex", new Vector2(0, m_Offset));
		renderer.material.SetTextureOffset("_NormalTex", new Vector2(0, m_Offset));
		renderer.material.SetTextureOffset("_EmissionTex", new Vector2(0, m_Offset));
	}
}
