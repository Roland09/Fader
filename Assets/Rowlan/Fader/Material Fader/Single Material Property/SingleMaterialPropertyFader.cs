using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Fade a material property value in and out with specified easing functions.
	/// 
	/// Supported property types:
	/// + Float
	/// + Color
	/// 
	/// </summary>
	public class SingleMaterialPropertyFader : MonoBehaviour
	{
		#region Public Variables
		
		// [Header("GameObjects")]

		public List<GameObject> sceneGameObjects = new List<GameObject>(new GameObject[1]);

		// [Header("Material Settings")]

		public FadeSettings propertyFadeSettings = new FadeSettings();

		[Space]

		[Header("Interpolation")]

		[Tooltip("The fade duration in seconds")]
		public float duration = 1f;

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
			fader = new CustomFader(sceneGameObjects, propertyFadeSettings);
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
			private readonly List<BaseFadeMaterial> fadeMaterials;

			public CustomFader(List<GameObject> sceneGameObjects, FadeSettings propertyFadeSettings) {
				this.fadeMaterials = GetFadeMaterials(sceneGameObjects, propertyFadeSettings);
			}

			/// <summary>
			/// Collect all fade materials depending on the property type and name id
			/// </summary>
			/// <param name="sceneGameObject"></param>
			/// <param name="propertyType"></param>
			/// <param name="propertyNameID"></param>
			/// <returns></returns>
			private List<BaseFadeMaterial> GetFadeMaterials(List<GameObject> sceneGameObjects, FadeSettings propertyFadeSettings)
			{
				List<BaseFadeMaterial> fadeMaterials = new List<BaseFadeMaterial>();

				foreach (GameObject sceneGameObject in sceneGameObjects)
				{
					Renderer[] rendererChildren = sceneGameObject.GetComponentsInChildren<Renderer>();

					foreach (Renderer renderer in rendererChildren)
					{
						BaseFadeMaterial fadeMaterial = null;

						switch (propertyFadeSettings.propertyType)
						{
							case MaterialPropertyType.Float:
								fadeMaterial = new FloatFadeMaterial(renderer, propertyFadeSettings);
								break;
							case MaterialPropertyType.Color:
								fadeMaterial = new ColorFadeMaterial(renderer, propertyFadeSettings);
								break;
						}

						if (fadeMaterial.IsValid())
						{
							fadeMaterials.Add(fadeMaterial);
						}
					}
				}

				return fadeMaterials;
			}

			public CustomFader(List<BaseFadeMaterial> fadeMaterials) {
				this.fadeMaterials = fadeMaterials;
			}

			public override void ApplyFade(float percentage)
			{
				foreach (BaseFadeMaterial fadeMaterial in fadeMaterials)
				{
					fadeMaterial.UpdateMaterials(percentage);
				}
			}
		}

		#endregion Fade Logic
	}
}