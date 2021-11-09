// ######################################################################
// MenuPanelItem - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System;
using UnityEngine;

namespace Project.UI
{
    public abstract class MenuPanelItem : MonoBehaviour
	{
		#region Event/Delegate(s):
		public event Action<MenuState> OnRequestMenuStateChangeEvent;
		#endregion

		#region Inspector Assigned Field(s):
		[SerializeField] private GameObject m_container;
		#endregion

		#region Public API:
		public virtual void SetVisibility(bool _state) => m_container.SetActive(_state);
		#endregion

		#region Interally Used Method(s):
		protected void InvokeCurrentMenuStateChangeRequest(MenuState _state) => 
				OnRequestMenuStateChangeEvent?.Invoke(_state);
		#endregion
	}
}