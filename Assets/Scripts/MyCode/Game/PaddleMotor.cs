// ######################################################################
// PaddleMotor - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;

namespace Project.Game
{
	public class PaddleMotor : MonoBehaviour
	{
		#region Inspector Assigned Field(s):
		[SerializeField] private float m_speed = 2.0f;
		[SerializeField] private float m_maxMovement = 2.0f;
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void OnEnable()
		{
			InputManager.Instance.OnHorizontalInputEvent += InputManager_HandleHorizontalInput;
		}

		private void OnDisable()
		{
			InputManager.Instance.OnHorizontalInputEvent += InputManager_HandleHorizontalInput;
		}
		#endregion

		#region Callback Method(s):
		private void InputManager_HandleHorizontalInput(float _input)
		{
			// float input = Input.GetAxis("Horizontal");

			Vector3 pos = transform.position;
			pos.x += _input * m_speed * Time.deltaTime;

			if (pos.x > m_maxMovement)
			{
				pos.x = m_maxMovement;
			}
			else if (pos.x < -m_maxMovement)
			{
				pos.x = -m_maxMovement;
			}

			transform.position = pos;
		}
		#endregion
	}
}