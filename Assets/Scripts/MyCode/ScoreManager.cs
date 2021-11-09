// ######################################################################
// ScoreManager - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using Project.Persistence;
using UnityEngine;

namespace Project
{
    [DefaultExecutionOrder(-1)]
    public class ScoreManager : MonoBehaviour
	{
        #region Properties:
		public static ScoreManager Instance { get; private set; }
		public ScoreHelper ScoreHelper { get; private set; }
        #endregion

        #region MonoBehaviour Callback Method(s):
		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;

            DontDestroyOnLoad(gameObject);

            LoadScores();
		}
        #endregion

        #region Internally Used Method(s):
        private void LoadScores()
        {
			ScoreHelper = new ScoreHelper();
            ScoreHelper.LoadData();
        }
        #endregion
	}
}