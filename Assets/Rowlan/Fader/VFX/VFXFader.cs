using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
    public class VFXFader : MonoBehaviour
    {
#if USING_VFX

		#region Public Variables

		public UnityEngine.VFX.VisualEffect visualEffectComponent;

		[Header( "Fade")]

		[Tooltip("The minimum hour offset. Note that if you want from midday to 3 hours past midnight, you should use e. g. from 12 to 27. The maximum hour offset. Note that if you want from midday to 3 hours past midnight, you should use e. g. from 12 to 27")]
		public FadeSettings propertyFadeSettings = new FadeSettings( 0f, 12f);

		[Tooltip("The fade duration in seconds")]
		public float duration = 5f;

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
			fader = new CustomFader(visualEffectComponent, propertyFadeSettings);
		}
		#endregion Initialization

		#region Fade Trigger
		void Update()
		{
			if (Input.anyKeyDown)
			{
				if (Input.GetKeyDown(toggleKeyCode))
				{
					fader.Start(this, fadeDirection, duration);

					// toggle fade direction
					fadeDirection = fadeDirection == FadeDirection.In ? FadeDirection.Out : FadeDirection.In;
				}
			}
		}
		#endregion Fade Trigger

		#region Fade Logic

		public class CustomFader : Fader
		{
			private UnityEngine.VFX.VisualEffect visualEffectComponent;
			private FadeSettings propertyFadeSettings;

			public CustomFader(UnityEngine.VFX.VisualEffect visualEffectComponent, FadeSettings propertyFadeSettings) 
			{
				this.visualEffectComponent = visualEffectComponent;
				this.propertyFadeSettings = propertyFadeSettings;
			}

			public override void ApplyFade(float percentage)
			{
			    float interpolatedValue = propertyFadeSettings.ease.Lerp(propertyFadeSettings.minimumValue, propertyFadeSettings.maximumValue, percentage);

				visualEffectComponent.SetFloat(propertyFadeSettings.propertyNameID, interpolatedValue);

			}
		}

		#endregion Fade Logic

#endif
	}
}