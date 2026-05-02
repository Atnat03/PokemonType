using UnityEngine;

public class PokemonFollow : MonoBehaviour
{
	#region Properties

	#endregion


	#region Variables

	[SerializeField] private float followSpeed = 3;
	[SerializeField] private Animator animator;
	
	private Transform target;
	private Vector3 _previousPosition;
	
	#endregion


	#region Fonctions

	public void SetTarget(Transform newTarget)
	{
		target = newTarget;
	}

	void Update()
	{
		if(target != null)
		{
			transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
			
			transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, followSpeed * Time.deltaTime);
			
			float velocity = (transform.position - _previousPosition).magnitude / Time.deltaTime;
			_previousPosition = transform.position;

			animator.SetFloat("Moving", velocity / followSpeed);
		}
	}

	#endregion
}
