using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class Spaceship : MonoBehaviour
{
	#region Configuration

	public float StrafeSpeed = 3f;
	public float RotateAngle = 40f;
	public float LimitX = 20f;

	public float HoverPeriod = 1f;
	public float HoverAmplitude = 1f;

	public GameObject ExplosionPrefab;

	#endregion

	#region Engine methods

	void Awake()
	{
		m_InitialPosition = rigidbody.position;
	}

	void OnTriggerEnter(Collider _Other)
	{
		if (_Other.GetComponent<Asteroid>() != null)
		{
			Game.Instance.Over();
		}
	}

	void Start()
	{
		var initialPosition = transform.position;
		var initialRotation = transform.rotation;
		Game.Instance.OnState(delegate(Game.State _Current)
		{
			switch(_Current)
			{
				case (Game.State.Launch):
					transform.position = initialPosition;
					transform.rotation = initialRotation;
					gameObject.SetActive(true);
					break;

				case (Game.State.Over):
					gameObject.SetActive(false);
					Instantiate(ExplosionPrefab, transform.position, transform.rotation);
					break;
			}
		});
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		var leftLimit = InitialPosition + new Vector3(-LimitX,0,0);
		var rightLimit = InitialPosition + new Vector3(LimitX,0,0);
		Gizmos.DrawLine(InitialPosition, leftLimit);
		Gizmos.DrawLine(InitialPosition, rightLimit);
	}

	void FixedUpdate()
	{
		// Inverting because of our inverted scene
		var horizontalAxis = -Controller.Steering;
		// Moving ship around
		{
			var turn = horizontalAxis * StrafeSpeed * Time.fixedDeltaTime;
			var movement = new Vector3(turn, 0, 0);
			var targetPosition = rigidbody.position + movement;
			targetPosition.x = Mathf.Clamp(
					targetPosition.x,
					InitialPosition.x -LimitX ,
					InitialPosition.x + LimitX
				);
			targetPosition.y = InitialPosition.y + HoverAmplitude * Mathf.Sin(4 * Time.fixedTime / HoverPeriod);
			rigidbody.MovePosition(targetPosition);
		}
		// Rotating ship
		{
			var targetAngle = horizontalAxis * RotateAngle;
			rigidbody.MoveRotation(Quaternion.Euler(
					rigidbody.rotation.eulerAngles.x,
					rigidbody.rotation.eulerAngles.y,
					targetAngle
				));
		}
	}

	#endregion

	#region Initial configuration

	Vector3 InitialPosition
	{
		get
		{
			return m_InitialPosition == null ?
				rigidbody.position :
				m_InitialPosition.Value;
		}
	}

	Vector3? m_InitialPosition;

	#endregion


}
