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
		public ColorFadeMaterial(Renderer renderer, string propertyNameID) : base(renderer, propertyNameID)
		{
		}

		public override MaterialBlockBase CreateMaterialBlock(int index, Material material)
		{
			ColorMaterialBlock data = new ColorMaterialBlock(index, material.GetColor(propertyNameID));

			return data;
		}

		public override void UpdateMaterials(float value)
		{
			foreach (ColorMaterialBlock materialBlock in materialBlocks)
			{
				Color color = materialBlock.color * value; // note about gamma: for gamma you could use baseColor * Mathf.LinearToGammaSpace( value)

				materialBlock.propertyBlock.SetColor(propertyNameID, color);

				renderer.SetPropertyBlock(materialBlock.propertyBlock, materialBlock.index);
			}

			// renderer.UpdateGIMaterials();
		}

		public class ColorMaterialBlock : MaterialBlockBase
		{
			/// <summary>
			/// The initial color of the material property
			/// </summary>
			public Color color;

			public ColorMaterialBlock(int index, Color color) : base(index)
			{
				this.color = color;
			}
		}
	}
}