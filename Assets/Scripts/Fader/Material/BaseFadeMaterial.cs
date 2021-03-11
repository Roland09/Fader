using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rowlan
{
	public abstract class BaseFadeMaterial
	{
		protected string propertyNameID;

		protected Renderer renderer;
		protected List<MaterialBlockBase> materialBlocks = new List<MaterialBlockBase>();

		public BaseFadeMaterial(Renderer renderer, string propertyNameID)
		{
			this.renderer = renderer;
			this.propertyNameID = propertyNameID;

			RegisterMaterials();
		}

		private void RegisterMaterials()
		{
			Material[] materials = renderer.materials;

			for (int i = 0; i < materials.Length; i++)
			{
				Material material = materials[i];

				if (!material.HasProperty(propertyNameID))
					continue;

				MaterialBlockBase materialBlock = CreateMaterialBlock(i, material);

				materialBlocks.Add(materialBlock);
			}
		}

		public bool IsValid()
		{
			return materialBlocks.Count > 0;
		}

		public abstract MaterialBlockBase CreateMaterialBlock(int index, Material material);

		public abstract void UpdateMaterials(float value);

		public class MaterialBlockBase
		{
			/// <summary>
			/// The index of the material in the renderer's material list
			/// </summary>
			public int index;
			public MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

			public MaterialBlockBase(int index)
			{
				this.index = index;
			}
		}
	}
}