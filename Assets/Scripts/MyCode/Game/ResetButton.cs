// ######################################################################
// ResetButton - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;

namespace Project.Game
{
	public class ResetButton : MonoBehaviour
	{	
		#region Public API:
		public void OnResetButtonClicked()
		{
			ScoreManager.Instance.ScoreHelper.SetScore("", 0);
			GameManager.Instance.ResetBestScoreText();
		}
		#endregion
	}
}