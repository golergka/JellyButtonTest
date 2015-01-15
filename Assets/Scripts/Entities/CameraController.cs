using UnityEngine;

[RequireComponent (typeof(Camera))]
public class CameraController : MonoBehaviour
{
	Vector3 r_Delta;
	float r_FOV;

	public float BoostDistanceMultiplier = 2f;
	public float BoostFOVMultiplier = 0.5f;

	public float TransitionTime = 1f;
	public float PositionSnap = 0.1f;
	public float FOVSnap = 5f;
	public Vector3 BoostDelta;

    float PositionSpeed
	{
		get
		{
			return Mathf.Abs(BoostDistanceMultiplier - 1f) * r_Delta.magnitude / TransitionTime;
		}
	}
	float FOVSpeed
	{
		get
		{
			return Mathf.Abs(BoostFOVMultiplier - 1f) * r_FOV / TransitionTime;
		}
	}

    public Transform Target;

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

		var desiredPos = Target.position + r_Delta + (Movement.Instance.Boost ? BoostDelta : Vector3.zero);
		var deltaPos = (desiredPos - transform.position).normalized * PositionSpeed * Time.deltaTime;
		var snapPos = (desiredPos - transform.position).magnitude < PositionSnap;
		transform.position = snapPos ? desiredPos : (transform.position + deltaPos);

		var desiredFOV = r_FOV * (Movement.Instance.Boost ? BoostFOVMultiplier : 1f);
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
}
