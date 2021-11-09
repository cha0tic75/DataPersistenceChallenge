// ######################################################################
// MenuCanvasHandler - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;

namespace Project.UI
{
    public enum MenuState { MainMenu, TopTen }

    public class MenuCanvasHandler : MonoBehaviour
	{
		#region Inspector Assigned Field(s):
		[SerializeField] MenuState m_defaultMenuState = MenuState.MainMenu;
		[SerializeField] private MainMenuPanel m_mainPanel;
		[SerializeField] private TopTenPanel m_topTenPanel;
		#endregion

		#region Internal State Field(s):
		private MenuState m_currentState;
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void OnEnable()
		{
			m_mainPanel.OnRequestMenuStateChangeEvent += SetMenuStateTo;
			m_topTenPanel.OnRequestMenuStateChangeEvent += SetMenuStateTo;
		}

		private void Start() => SetMenuStateTo(m_defaultMenuState);

		private void OnDisable()
		{
			m_mainPanel.OnRequestMenuStateChangeEvent -= SetMenuStateTo;
			m_topTenPanel.OnRequestMenuStateChangeEvent -= SetMenuStateTo;
		}
		#endregion
		
		#region Public API::
		private void SetMenuStateTo(MenuState _newState)
		{
			switch(_newState)
			{
				default:
				case MenuState.MainMenu:
					m_mainPanel.SetVisibility(true);
					m_topTenPanel.SetVisibility(false);

					m_currentState = _newState;

					break;

				case MenuState.TopTen:
					m_topTenPanel.SetVisibility(true);
					m_mainPanel.SetVisibility(false);

					m_currentState = _newState;

					break;
			}
		}
		#endregion
	}
}