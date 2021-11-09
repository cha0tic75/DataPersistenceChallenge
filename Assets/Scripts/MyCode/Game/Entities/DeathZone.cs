// ######################################################################
// DeathZone - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System;
using UnityEngine;

namespace Project.Game
{
	public class DeathZone : MonoBehaviour
	{
		#region Event/Delegate(s):
		public event Action OnGameOverEvent;
		#endregion

		#region Inspector Assigned Field(s):		
    	public MainManager Manager;
		#endregion

		#region MonoBehaviour Callback Method(s):
    	private void OnCollisionEnter(Collision other)
    	{
        	Destroy(other.gameObject);
       		OnGameOverEvent?.Invoke();
    	}
		#endregion
	}
}