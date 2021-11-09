// ######################################################################
// InputManager - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System;
using UnityEngine;

namespace Project.Game
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
	{
		#region Event/Delegate(s):
		public event Action<float> OnHorizontalInputEvent;
		public event Action OnSpaceBarInputEvent;
		#endregion


		#region Internal State Field(s):
		private bool m_inputEnabled;
		#endregion

		#region MonoBehaviour Callback Method(s):
		protected override void Awake()
		{
			base.Awake();
			m_inputEnabled = true;
		}
		private void OnEnable() => GameManager.Instance.OnGameOverEvent += GameManager_OnGameOverCallback;

		private void OnDisable() => GameManager.Instance.OnGameOverEvent -= GameManager_OnGameOverCallback;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space)) { OnSpaceBarInputEvent?.Invoke(); }
			
			if (m_inputEnabled)
			{
				OnHorizontalInputEvent?.Invoke(Input.GetAxis("Horizontal"));
			}

		}
		#endregion

		#region Callback Method(s):
		private void GameManager_OnGameOverCallback() => m_inputEnabled = false;
		#endregion
	}
}