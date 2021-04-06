using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rowlan.Fader
{
	public class FadeMaterial
	{
		protected List<FadeSettings> propertyFadeSettings;

		protected Renderer renderer;
		protected List<MaterialBlockBase> materialBlocks = new List<MaterialBlockBase>();

		public FadeMaterial(Renderer renderer, List<FadeSettings> propertyFadeSettings)
		{
			this.renderer = renderer;
			this.propertyFadeSettings = propertyFadeSettings;

			RegisterMaterials();
		}

		/// <summary>
		/// Collect all supported materials of the render. The supported ones are the ones which have the specified property name id.
		/// </summary>
		private void RegisterMaterials()
		{

			// create a list of all property name IDs in the settings
			List<string> propertyNameIDs = propertyFadeSettings.ConvertAll<string>(x => x.propertyNameID);

			Material[] materials = renderer.materials;

			for (int i = 0; i < materials.Length; i++)
			{
				Material material = materials[i];

				// ensure all property name ids exist, otherwise skip the material; useful if you have multiple different materials
				bool hasPropertyNameID = true;

				foreach (string propertyNameID in propertyNameIDs)
				{
					if (!material.HasProperty(propertyNameID))
					{
						hasPropertyNameID = false;
						break;
					}
				}

				if (!hasPropertyNameID)
					continue;

				// Create a material data container for fading
				MaterialBlockBase materialBlock = new MaterialBlockBase(i, material, propertyFadeSettings);

				materialBlocks.Add(materialBlock);
			}
		}

		/// <summary>
		/// Whether or not this material is supported for fading. It is valid if at least 1 material block has been registered.
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{
			return materialBlocks.Count > 0;
		}

		/// <summary>
		/// Process the material data containers and set the fade percentage.
		/// </summary>
		/// <param name="percentage">An interpolated value of [0,1] range which spans the duration</param>
		public void UpdateMaterials(float percentage) {

			foreach(MaterialBlockBase materialBlock in materialBlocks) {

				List<FadeSettings> propertyFadeSettings = materialBlock.propertyFadeSettings;

				// store the initial values of all material property ids of the given material
				foreach (FadeSettings fadeSettings in propertyFadeSettings)
				{

					float interpolatedValue;

					switch (fadeSettings.propertyType)
					{
						case FadeConst.MaterialPropertyType.Float:

							interpolatedValue = fadeSettings.ease.Lerp(fadeSettings.minimumValue, fadeSettings.maximumValue, percentage);

							materialBlock.propertyBlock.SetFloat(fadeSettings.propertyNameID, interpolatedValue);

							break;

						case FadeConst.MaterialPropertyType.Color:

							interpolatedValue = fadeSettings.ease.Lerp(fadeSettings.minimumValue, fadeSettings.maximumValue, percentage);

							Color color = materialBlock.GetInitialColor( fadeSettings.propertyNameID) * interpolatedValue; // note about gamma: for gamma you could use baseColor * Mathf.LinearToGammaSpace( value)

							materialBlock.propertyBlock.SetColor(fadeSettings.propertyNameID, color);

							break;

						default:
							throw new ArgumentOutOfRangeException(string.Format("Unsupported property type {0}", fadeSettings.propertyType));
					}

				}

				renderer.SetPropertyBlock(materialBlock.propertyBlock, materialBlock.index);

			}

		}

		/// <summary>
		/// Material data container class per supported material. Supported materials are the ones which contain the given property name id
		/// </summary>
		public class MaterialBlockBase
		{
			/// <summary>
			/// The index of the material in the renderer's material list
			/// </summary>
			public int index;
			public MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

			public Dictionary<string, float> initialFloatMap = new Dictionary<string, float>();
			public Dictionary<string, Color> initialColorMap = new Dictionary<string, Color>();

			public List<FadeSettings> propertyFadeSettings;

			public MaterialBlockBase(int index, Material material, List<FadeSettings> propertyFadeSettings)
			{
				this.index = index;
				// this.material = material;
				this.propertyFadeSettings = propertyFadeSettings;

				// save the initial values of all material property ids of the given material
				SaveInitialValues( material);
			}

			private void SaveInitialValues(Material material) {

				foreach (FadeSettings fadeSettings in propertyFadeSettings)
				{

					switch (fadeSettings.propertyType)
					{
						case FadeConst.MaterialPropertyType.Float:
							initialFloatMap.Add(fadeSettings.propertyNameID, material.GetFloat(fadeSettings.propertyNameID));
							break;

						case FadeConst.MaterialPropertyType.Color:
							initialColorMap.Add(fadeSettings.propertyNameID, material.GetColor(fadeSettings.propertyNameID));
							break;

						default:
							throw new ArgumentOutOfRangeException(string.Format("Unsupported property type {0}", fadeSettings.propertyType));
					}

				}

			}

			public float GetInitialFloat(string propertyNameID)
			{
				return initialFloatMap[propertyNameID];
			}

			public Color GetInitialColor( string propertyNameID) {
				return initialColorMap[propertyNameID];
			}
		}
	}
}