using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Interpolate between 0 and 1 for a specified duration 
	/// and apply it to the implemented <see cref="ApplyFade"/> method.
	/// </summary>
	public abstract class Fader
	{
		private Coroutine currentFadeCoroutine = null;

		/// <summary>
		/// Stop currently running fading and start a new one depending on the specified parameters
		/// </summary>
		/// <param name="monoBehaviour">Required for the Coroutine invocations</param>
		public void Start(MonoBehaviour monoBehaviour, FadeDirection fadeDirection, float duration)
		{

			if (currentFadeCoroutine != null)
			{
				monoBehaviour.StopCoroutine(currentFadeCoroutine);
			}

			currentFadeCoroutine = monoBehaviour.StartCoroutine(Fade(duration, fadeDirection));
		}

		private IEnumerator Fade(float globalDuration, FadeDirection fadeDirection)
		{

			float timeElapsed = 0;

			while (timeElapsed < globalDuration)
			{

				float percentage = timeElapsed / globalDuration;

				if (fadeDirection == FadeDirection.Out)
				{
					percentage = 1.0f - percentage;
				}

				percentage = Mathf.Clamp(percentage, 0f, 1f);

				timeElapsed += Time.deltaTime;

				ApplyFade(percentage);

				yield return null;
			}

			// set end value explicitly, otherwise it might not be the specified one because of the time increments
			if (fadeDirection == FadeDirection.Out)
			{
				ApplyFade(0f);
			}
			else
			{
				ApplyFade(1f);
			}
		}

		/// <summary>
		/// This method is invoked for the time of the fade duration and gets the interpolated percentage applied
		/// </summary>
		/// <param name="percentage">An interpolated value of [0,1] range which spans the duration</param>
		public abstract void ApplyFade(float percentage);
	}
}