// ######################################################################
// GameOverPanelUI - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;

namespace Project.Game
{
	public class GameOverPanelUI : MonoBehaviour
	{
		#region Inspector Assigned Field(s):
		[SerializeField] private GameObject m_container;
		[SerializeField] private GameObject m_informNewGameContainer;
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void Awake()
		{
			m_informNewGameContainer.SetActive(false);
			m_container.SetActive(false);
		}

		private void OnEnable()
		{
			GameManager.Instance.OnGameOverEvent += GameManager_OnGameOverCallback;
			GameManager.Instance.OnInformNewGameEvent += GameManager_OnInformNewGameCallback;
		}

		private void OnDisable()
		{
			GameManager.Instance.OnGameOverEvent -= GameManager_OnGameOverCallback;
			GameManager.Instance.OnInformNewGameEvent += GameManager_OnInformNewGameCallback;
		}
		#endregion

		#region Callback(s):
		private void GameManager_OnGameOverCallback() => m_container.SetActive(true);
		private void GameManager_OnInformNewGameCallback() => m_informNewGameContainer.SetActive(true);
		#endregion
	}
}