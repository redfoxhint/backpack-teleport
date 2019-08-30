using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BackpackTeleport.Characters
{
	public class BaseCharacterMovement : MonoBehaviour
	{
		[Header("Movement Configuration")]
		[SerializeField] private float turnSpeed = 50f;
		private Transform targetDestination;
		[SerializeField] private LayerMask enemyMask;

		[Header("Probe Setup")]
		[SerializeField] private float probeDetectionRange = 1.0f;
		[SerializeField] private Transform forwardProbe;
		[SerializeField] private Transform leftProbe;
		[SerializeField] private Transform rightProbe;
		[SerializeField] private Transform downProbe;

		// Private Variables
		private Transform obstaleToAvoid;
		private bool avoidObstacle = false;
		private bool hasPath = false;

		// Components
		private NavMeshAgent2D navMeshAgent2D;

		private void Awake()
		{
			navMeshAgent2D = GetComponent<NavMeshAgent2D>();
		}

		public void SetTarget(Transform newDestination)
		{
			targetDestination = newDestination;
			hasPath = true;

			if (forwardProbe == null)
				forwardProbe = transform;

			if (leftProbe == null)
				leftProbe = transform;

			if (rightProbe == null)
				rightProbe = transform;

			if (downProbe == null)
				downProbe = transform;

		}

		private void Update()
		{
			if (hasPath)
			{
				RaycastHit2D hit2D;
				Vector2 direction = (targetDestination.position - transform.position).normalized;

				bool previousCastMissed = true;

				hit2D = Physics2D.Raycast(forwardProbe.position, transform.up, probeDetectionRange, enemyMask);

				if (hit2D)
				{
					if (obstaleToAvoid != targetDestination.transform)
					{
						Debug.DrawLine(transform.position, hit2D.point, Color.green);
						previousCastMissed = false;
						avoidObstacle = true;
						navMeshAgent2D.isStopped = true;

						if (hit2D.transform != transform)
						{
							obstaleToAvoid = hit2D.transform;
							direction += hit2D.normal * turnSpeed;
						}
					}
				}

				if (obstaleToAvoid && previousCastMissed)
				{
					hit2D = Physics2D.Raycast(leftProbe.position, -transform.right, probeDetectionRange, enemyMask);

					if (hit2D)
					{
						if (obstaleToAvoid != targetDestination)
						{
							Debug.DrawLine(leftProbe.position, hit2D.point, Color.red);
							avoidObstacle = true;
							navMeshAgent2D.isStopped = true;

							if (hit2D.transform != transform)
							{
								obstaleToAvoid = hit2D.transform;
								previousCastMissed = false;
								direction += hit2D.normal * turnSpeed;
							}
						}
					}

				}

				if (obstaleToAvoid && previousCastMissed)
				{
					hit2D = Physics2D.Raycast(rightProbe.position, transform.right, probeDetectionRange, enemyMask);

					if (hit2D)
					{
						if (obstaleToAvoid != targetDestination)
						{
							Debug.DrawLine(rightProbe.position, hit2D.point, Color.green);
							avoidObstacle = true;
							navMeshAgent2D.isStopped = true;

							if (hit2D.transform != transform)
							{
								obstaleToAvoid = hit2D.transform;
								previousCastMissed = false;
								direction += hit2D.normal * turnSpeed;
							}
						}
					}
				}

				if (obstaleToAvoid != null)
				{
					Vector2 forward = transform.TransformDirection(Vector2.up);
					Vector2 toOther = obstaleToAvoid.position - transform.position;

					if(Vector2.Dot(forward, toOther) < 0)
					{
						avoidObstacle = false;
						obstaleToAvoid = null;
						navMeshAgent2D.ResetPath();
						navMeshAgent2D.SetDestination(targetDestination.position);
						navMeshAgent2D.isStopped = false;
					}
				}

				if(avoidObstacle)
				{
					//Quaternion rot = Quaternion.LookRotation(direction);
					//transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);
					//transform.position += transform.up * navMeshAgent2D.speed * Time.deltaTime;
				}
			}
		}
	}
}


