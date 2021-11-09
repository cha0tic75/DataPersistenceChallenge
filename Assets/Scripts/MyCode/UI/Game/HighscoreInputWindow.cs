// ######################################################################
// HighscoreInputWindow - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace Project.Game
{
	public class HighscoreInputWindow : MonoBehaviour
	{
		#region Event/Delegate(s):
		public event Action<string> OnInputSubmittedEvent;
		#endregion

		#region Inspector Assigned Field(s):
		[SerializeField] private GameObject m_container;
		[SerializeField] private TMP_InputField m_inputField;
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void Awake() => SetVisibility(false);
		#endregion
		
		#region Public API:
		public void SetVisibility(bool _state) => m_container.SetActive(_state);
		public void OnOkButtonClicked()
		{
			OnInputSubmittedEvent?.Invoke(m_inputField.text);
		}
		public void OnCancelButtonClicked()
		{
			OnInputSubmittedEvent?.Invoke("");
		}
		#endregion
	}
}