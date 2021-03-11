using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.FadeConst;

namespace Rowlan
{
    public abstract class Fader
    {
        private Coroutine currentFadeCoroutine = null;
		private float duration = 0;
		private Ease ease;

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

		public abstract void ApplyFade(float value);
	}
}