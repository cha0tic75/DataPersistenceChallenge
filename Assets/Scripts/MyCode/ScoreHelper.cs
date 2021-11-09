// ######################################################################
// ScoreHelper - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System;
using System.IO;
using UnityEngine;

namespace Project.Persistence
{
    public class ScoreHelper
	{
		#region Internal State Field(s):
		private string m_savePath;
		#endregion

		#region Properties:
		public TopTenScores TopTen { get; private set; }
		#endregion

		#region Constructor(s):
		public ScoreHelper()
		{
			m_savePath = Application.persistentDataPath + "/savefile.json";
			
			TopTen = new TopTenScores();

			for (int i = 0; i < 10; i++)
			{
				TopTen.Data[i] = new ScoreData();
			}
		}
		#endregion

		#region Public API:
		public ScoreData GetHighSCore() => GetScoreByRank(0);

        public ScoreData GetScoreByRank(int _rank) => (_rank >= 0 && _rank < TopTen.Data.Length) ? TopTen.Data[_rank] : null;

        public void SetScore(string _inputValue, int _score)
        {
            TopTen.Data[0] = new ScoreData() {Name = _inputValue, Score = _score};
			SaveData();
        }

        public void SaveData()
		{
			string json = JsonUtility.ToJson(TopTen);

			File.WriteAllText(m_savePath, json);
		}

		public void LoadData()
		{
			if (File.Exists(m_savePath))
			{
				string json = File.ReadAllText(m_savePath);
				TopTenScores data = JsonUtility.FromJson<TopTenScores>(json);

				TopTen = data;
			}
		}
		#endregion

		#region Support Class(es):
		[System.Serializable]
		public class TopTenScores
		{
			public ScoreData[] Data = new ScoreData[10];
		}
        #endregion
    }
}