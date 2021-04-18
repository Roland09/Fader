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
			// check if the toggle key got pressed and if so start the fading and toggle the fade direction afterwards
			fader.StartToggleOnInput(toggleKeyCode, this, ref fadeDirection, duration);
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