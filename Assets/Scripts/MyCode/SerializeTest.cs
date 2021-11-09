// ######################################################################
// SerializeTest - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Project
{
	public class SerializeTest : MonoBehaviour
	{
		#region Internal State Field(s):
		private string m_savePath;
		[SerializeField] private SaveData m_saveData;
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void Start()
        {
            m_savePath = Application.persistentDataPath + "/savefile.json";
            // SaveToJson();
			LoadFromJson();
        }
        #endregion

        #region Internally Used Method(s):
        private string RandomName() => "Name_" + UnityEngine.Random.Range(1, 1001);

		private int RandomScore() => UnityEngine.Random.Range(1, 100001);
        private void SaveToJson()
        {
			m_saveData = new SaveData();

            for (int i = 0; i < 10; i++)
            {
                m_saveData.TopTen[i] = new Data() { Name = RandomName(), Score = RandomScore() };
            }

            // string jsonSaveData = "";
            // for (int i = 0; i < 10; i++)
            // {
            //     jsonSaveData += JsonUtility.ToJson(m_data[i]);
            // }

			string data = JsonUtility.ToJson(m_saveData);

            File.WriteAllText(m_savePath, data);
            Debug.Log(m_savePath);
        }

		private void LoadFromJson()
		{
			if (File.Exists(m_savePath))
			{
				string json = File.ReadAllText(m_savePath);
				SaveData data = JsonUtility.FromJson<SaveData>(json);

				m_saveData = data;
			}
		}
		#endregion

		#region
		[System.Serializable]
		public class SaveData
		{
			public Data[] TopTen = new Data[10];

		}
		[System.Serializable]
		public class Data
		{
			public string Name;
			public int Score;
		}
		#endregion
	}
}