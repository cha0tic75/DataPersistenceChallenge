// ######################################################################
// Brick - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;
using UnityEngine.Events;

namespace Project.Game
{
	public class Brick : MonoBehaviour
	{	
		#region Properties:
    	public UnityEvent<int> onDestroyed;
   		public int PointValue { get; set; }
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void Start()
		{
			var renderer = GetComponentInChildren<Renderer>();

			MaterialPropertyBlock block = new MaterialPropertyBlock();

			switch (PointValue)
			{
				case 1 : block.SetColor("_BaseColor", Color.green); break;
				case 2: block.SetColor("_BaseColor", Color.yellow); break;
				case 5: block.SetColor("_BaseColor", Color.blue); break;
				default: block.SetColor("_BaseColor", Color.red); break;
			}

			renderer.SetPropertyBlock(block);
		}

		private void OnCollisionEnter(Collision other)
		{
			onDestroyed.Invoke(PointValue);
			
			//slight delay to be sure the ball have time to bounce
			Destroy(gameObject, 0.2f);
		}
		#endregion
	}
}