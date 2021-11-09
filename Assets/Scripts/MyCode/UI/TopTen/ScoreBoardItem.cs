// ######################################################################
// ScoreBoardItem - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project
{
	public class ScoreBoardItem : MonoBehaviour
	{
		#region Inspector Assigned Field(s):
		[SerializeField] private TextMeshProUGUI m_rankTextTMP;
		[SerializeField] private TextMeshProUGUI m_nameTextTMP;
		[SerializeField] private TextMeshProUGUI m_scoreTextTMP;
		[SerializeField] private Image m_image;
		#endregion

		#region Internal State Field(s):
		private static Color m_evenColor = new Color(0.5660378f, 0.5660378f, 0.5660378f, 1f);
		private static Color m_oddColor = new Color (0.5188679f, 0.5188679f, 0.5188679f, 1f);
		#endregion
		
		#region Public API:
		public void Initialize(int _rank, ScoreData _scoreData)
		{
			m_image.color = (_rank % 2 == 0) ? m_evenColor : m_oddColor;

			m_rankTextTMP.SetText((_rank < 10) ? $" {_rank}" : $"{_rank}");

			bool hasData = _scoreData != null;

			if (hasData)
			{
				m_nameTextTMP.SetText((hasData) ? _scoreData.Name : "");
				m_scoreTextTMP.SetText((hasData) ? $"{_scoreData.Score}" : "0");
			}
		}
		#endregion
	}
}