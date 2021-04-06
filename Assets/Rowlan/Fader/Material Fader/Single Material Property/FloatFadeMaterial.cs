using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rowlan.Fader
{
	/// <summary>
	/// Material float value fading handler.
	/// </summary>
	public class FloatFadeMaterial : BaseFadeMaterial
	{
		public FloatFadeMaterial(Renderer renderer, FadeSettings propertyFadeSettings) : base(renderer, propertyFadeSettings)
		{
		}

		public override MaterialBlockBase CreateMaterialBlock(int index, Material material)
		{
			FloatMaterialBlock data = new FloatMaterialBlock(index, material.GetFloat(propertyFadeSettings.propertyNameID));

			return data;
		}

		public override void UpdateMaterials(float percentage)
		{
			foreach (FloatMaterialBlock materialBlock in materialBlocks)
			{

				float interpolatedValue = propertyFadeSettings.ease.Lerp(propertyFadeSettings.minimumValue, propertyFadeSettings.maximumValue, percentage);

				materialBlock.propertyBlock.SetFloat(propertyFadeSettings.propertyNameID, interpolatedValue);

				renderer.SetPropertyBlock(materialBlock.propertyBlock, materialBlock.index);
			}

			//renderer.UpdateGIMaterials();
		}

		public class FloatMaterialBlock : MaterialBlockBase
		{
			/// <summary>
			/// The initial float value of the material property
			/// </summary>
			public float initialValue;

			public FloatMaterialBlock(int index, float initialValue) : base(index)
			{
				this.initialValue = initialValue;
			}

		}
	}
}