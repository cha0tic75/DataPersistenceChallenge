// ######################################################################
// MenuCanvasHandler - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    public class TopTenPanel : MenuPanelItem
	{
		#region Inspector Assigned Field(s):
		[SerializeField] private Transform m_scoreContainer;
		[SerializeField] private ScoreBoardItem m_scoreboardItemPrefab;
		#endregion

		#region Internal State Field(s):
		private List<ScoreBoardItem> m_scoreboardIems = new List<ScoreBoardItem>();
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void Start()
		{
			for (int i = 0; i < ScoreManager.Instance.ScoreHelper.TopTen.Data.Length; i++)
			{
				ScoreBoardItem scoreboardItem = Instantiate(m_scoreboardItemPrefab, m_scoreContainer);
				scoreboardItem.Initialize(i + 1, null);
				m_scoreboardIems.Add(scoreboardItem);
			}

			SetVisibility(false);
		}
		#endregion

		#region Public API:
		public override void SetVisibility(bool _state)
		{
			base.SetVisibility(_state);

			if (_state)
			{
				for (int i = 0; i < m_scoreboardIems.Count; i++)
				{
					m_scoreboardIems[i].Initialize(i+1, ScoreManager.Instance.ScoreHelper.TopTen.Data[i]);
				}
			}
		}

		public void OnBackButtonClickedEvent() => InvokeCurrentMenuStateChangeRequest(MenuState.MainMenu);
		#endregion
	}
}