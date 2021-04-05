using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Interpolate a value within a specified duration and apply it to the implemented <see cref="ApplyFade"/> method.
	/// </summary>
	public abstract class Fader
    {
        private Coroutine currentFadeCoroutine = null;
		private float duration = 0;
		private Ease ease;

		/// <summary>
		/// Stop currently running fading and start a new one depending on the specified parameters
		/// </summary>
		/// <param name="monoBehaviour">Required for the Coroutine invocations</param>
		/// <param name="fadeDirection"></param>
		/// <param name="duration"></param>
		/// <param name="ease"></param>
		/// <param name="minimumValue"></param>
		/// <param name="maximumValue"></param>
		public void Start(MonoBehaviour monoBehaviour, FadeDirection fadeDirection, float duration, Ease ease, float minimumValue, float maximumValue) {

			this.duration = duration;
			this.ease = ease;

			if (currentFadeCoroutine != null)
			{
				monoBehaviour.StopCoroutine(currentFadeCoroutine);
			}

			// start fading
			if (fadeDirection == FadeDirection.In)
			{
				currentFadeCoroutine = monoBehaviour.StartCoroutine(Fade(minimumValue, maximumValue));
			}
			else if (fadeDirection == FadeDirection.Out)
			{
				currentFadeCoroutine = monoBehaviour.StartCoroutine(Fade(maximumValue, minimumValue));
			}
		}

		private IEnumerator Fade(float beginValue, float endValue)
		{
			float timeElapsed = 0;

			while (timeElapsed < duration)
			{
				float value = ease.Lerp(beginValue, endValue, timeElapsed / duration);

				timeElapsed += Time.deltaTime;

				ApplyFade(value);

				yield return null;
			}

			// set end value explicitly, otherwise it might not be the specified one because of the time increments
			ApplyFade(endValue);
		}

		/// <summary>
		/// This method is invoked for the time of the fade duration and gets the interpolated value applied
		/// </summary>
		/// <param name="value"></param>
		public abstract void ApplyFade(float value);
	}
}