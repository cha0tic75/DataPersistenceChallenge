// ######################################################################
// BallBehaviour - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;

namespace Project.Game
{
	public class BallBehaviour : MonoBehaviour
	{
		#region Internal State Field(s):
		private Rigidbody m_rigidbody;
		#endregion

		#region MonoBehaviour Callback Method(s):
		void Start() => m_rigidbody = GetComponent<Rigidbody>();
		
		private void OnCollisionExit(Collision other)
		{
			var velocity = m_rigidbody.velocity;
			
			//after a collision we accelerate a bit
			velocity += velocity.normalized * 0.01f;
			
			//check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
			if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
			{
				velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
			}

			//max velocity
			if (velocity.magnitude > 3.0f)
			{
				velocity = velocity.normalized * 3.0f;
			}

			m_rigidbody.velocity = velocity;
		}
		#endregion
	}
}