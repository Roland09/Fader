using System;
using System.Collections;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Example about how to fade Aura 2's fog density
	/// </summary>
	public class Aura2VolumeFader : MonoBehaviour
	{
#if AURA_IN_PROJECT

		#region Public Variables

		[Header("Aura")]

		public Aura2API.AuraVolume auraVolume;

		[Header( "Fade")]

		[Tooltip("The minimum and the maximum fog density")]
		public FadeSettings propertyFadeSettings = new FadeSettings( -2f, 5f);

		[Tooltip("The fade duration in seconds")]
		public float duration = 2f;

		[Header("Input")]

		[Tooltip("The input key that triggers the fading")]
		public KeyCode toggleKeyCode = KeyCode.None;

		#endregion Public Variables

		#region Internal Variables
		private FadeDirection fadeDirection = FadeDirection.In;

		private Fader fader;
		#endregion Internal Variables

		#region Initialization
		void Start()
		{
			fader = new CustomFader(auraVolume, propertyFadeSettings);
		}
		#endregion Initialization

		#region Fade Trigger
		void Update()
		{
			// check if the toggle key got pressed and if so start the fading and toggle the fade direction afterwards
			fader.StartToggleOnInput(toggleKeyCode, this, ref fadeDirection, duration);
		}
		#endregion Fade Trigger

		#region Fade Logic

		public class CustomFader : Fader
		{
			FadeSettings propertyFadeSettings;
			Aura2API.AuraVolume auraVolume;

			public CustomFader(Aura2API.AuraVolume auraVolume, FadeSettings propertyFadeSettings) {
				this.auraVolume = auraVolume;
				this.propertyFadeSettings = propertyFadeSettings;
			}

			public override void ApplyFade(float percentage)
			{
				float interpolatedValue = propertyFadeSettings.ease.Lerp(propertyFadeSettings.minimumValue, propertyFadeSettings.maximumValue, percentage);

				auraVolume.densityInjection.strength = interpolatedValue;
			}
		}

		#endregion Fade Logic

#endif
	}
}