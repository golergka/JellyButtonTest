using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraController : MonoBehaviour
{
	#region Configuration

    public Transform	Target;

	public float		TransitionTime = 1f;

	public float		BoostFOVMultiplier = 0.5f;
	public Vector3		BoostDelta;

	public float		PositionSnap = 0.1f;
	public float		FOVSnap = 5f;

	#endregion

	#region Helper properties

    float PositionSpeed
	{
		get
		{
			return Mathf.Abs(BoostDelta.magnitude - 1f) * r_Delta.magnitude / TransitionTime;
		}
	}

	float FOVSpeed
	{
		get
		{
			return Mathf.Abs(BoostFOVMultiplier - 1f) * r_FOV / TransitionTime;
		}
	}
	
	#endregion

	#region Initial state

	Vector3 r_Delta;
	float	r_FOV;

	#endregion

	#region Engine methods

	void Start()
	{
		r_Delta = transform.position - Target.position;
		r_FOV = camera.fieldOfView;
	}

    private void LateUpdate()
    {
        // Early out if we don't have a Target
        if (!Target)
        {
            return;
        }

		// Where do we need to get
		var desiredPos = Target.position + r_Delta + (Controller.Boost ? BoostDelta : Vector3.zero);
		// How far is that
		var deltaPos = (desiredPos - transform.position).normalized * PositionSpeed * Time.deltaTime;
		// Can we snap to that position?
		var snapPos = (desiredPos - transform.position).magnitude < PositionSnap;
		transform.position = snapPos ? desiredPos : (transform.position + deltaPos);

		// Exactly the same computation for field of view
		var desiredFOV = r_FOV * (Controller.Boost ? BoostFOVMultiplier : 1f);
		var deltaFOV = Mathf.Sign(desiredFOV - camera.fieldOfView) * FOVSpeed * Time.deltaTime;
		var snapFOV = Mathf.Abs(desiredFOV - camera.fieldOfView) < FOVSnap;
		camera.fieldOfView = snapFOV ? desiredFOV : (camera.fieldOfView + deltaFOV);

    }

	void OnDrawGizmosSelected()
	{
		var initialPosition = Application.isPlaying ? (Target.position + r_Delta) : transform.position;
		var initialFOV = Application.isPlaying ? r_FOV : camera.fieldOfView;
		Gizmos.matrix = Matrix4x4.TRS(initialPosition + BoostDelta, transform.rotation, Vector3.one);
		Gizmos.color = Color.green;
		Gizmos.DrawFrustum(
				Vector3.zero,
			   	initialFOV * BoostFOVMultiplier,
			   	camera.farClipPlane,
			   	camera.nearClipPlane,
			   	camera.aspect
			);
	}

	#endregion
}
