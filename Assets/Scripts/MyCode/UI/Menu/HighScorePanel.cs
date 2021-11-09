// ######################################################################
// HighScorePanel - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;
using TMPro;

namespace Project.UI
{
    public class HighScorePanel : MonoBehaviour
	{
        #region Inspector Assigned Field(s):
		[SerializeField] private TextMeshProUGUI m_dataTMP;
        #endregion

        #region Public API:
        public void SetData(ScoreData _data) =>  m_dataTMP.SetText($"{_data.Name} : {_data.Score}");
        #endregion
	}
}