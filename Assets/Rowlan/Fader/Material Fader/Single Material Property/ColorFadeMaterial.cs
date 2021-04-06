using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rowlan.Fader
{
	/// <summary>
	/// Material color fading handler.
	/// </summary>
	public class ColorFadeMaterial : BaseFadeMaterial
	{
		public ColorFadeMaterial(Renderer renderer, FadeSettings propertyFadeSettings) : base(renderer, propertyFadeSettings)
		{
		}

		public override MaterialBlockBase CreateMaterialBlock(int index, Material material)
		{
			ColorMaterialBlock data = new ColorMaterialBlock(index, material.GetColor(propertyFadeSettings.propertyNameID));

			return data;
		}

		public override void UpdateMaterials(float percentage)
		{
			foreach (ColorMaterialBlock materialBlock in materialBlocks)
			{
				float interpolatedValue = propertyFadeSettings.ease.Lerp(propertyFadeSettings.minimumValue, propertyFadeSettings.maximumValue, percentage);

				Color color = materialBlock.initialColor * interpolatedValue; // note about gamma: for gamma you could use baseColor * Mathf.LinearToGammaSpace( value)

				materialBlock.propertyBlock.SetColor(propertyFadeSettings.propertyNameID, color);

				renderer.SetPropertyBlock(materialBlock.propertyBlock, materialBlock.index);
			}

			// renderer.UpdateGIMaterials();
		}

		public class ColorMaterialBlock : MaterialBlockBase
		{
			/// <summary>
			/// The initial color of the material property
			/// </summary>
			public Color initialColor;

			public ColorMaterialBlock(int index, Color initialColor) : base(index)
			{
				this.initialColor = initialColor;
			}
		}
	}
}