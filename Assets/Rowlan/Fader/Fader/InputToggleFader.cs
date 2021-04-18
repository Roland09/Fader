using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Control the <see cref="Fader"/> via Input and toggle the fade direction upon keypress.
	/// </summary>
	public abstract class InputToggleFader : Fader
	{
		/// <summary>
		/// Starts the fading using <see cref="Start"/> and toggled the fade direction afterwards
		/// </summary>
		/// <param name="monoBehaviour"></param>
		/// <param name="fadeDirection"></param>
		/// <param name="duration"></param>
		public void StartToggle(MonoBehaviour monoBehaviour, ref FadeDirection fadeDirection, float duration)
		{
			Start(monoBehaviour, fadeDirection, duration);

			// toggle fade direction
			fadeDirection = fadeDirection == FadeDirection.In ? FadeDirection.Out : FadeDirection.In;
		}

		/// <summary>
		/// Checks if the toggle key got pressed and if so starts the fading and toggles the fade direction afterwards
		/// </summary>
		/// <param name="toggleKeyCode"></param>
		/// <param name="monoBehaviour"></param>
		/// <param name="fadeDirection"></param>
		/// <param name="duration"></param>
		public void StartToggleOnInput(KeyCode toggleKeyCode, MonoBehaviour monoBehaviour, ref FadeDirection fadeDirection, float duration)
		{
			if (Input.anyKeyDown)
			{
				if (Input.GetKeyDown(toggleKeyCode))
				{
					StartToggle(monoBehaviour, ref fadeDirection, duration);
				}
			}
		}
	}
}