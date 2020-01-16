using UnityEngine;
using UnityEngine.UI;

using Scripts.Framework.Utils;

namespace Scripts.UI
{
	public class LayeredLabel : MonoBehaviour
	{
		public string text;

		public Font font;
		public int fontSize;
		public bool richText;

		public Vector2 offset;

		public Color layer1Color;
		public Color layer2Color;
		public Color layer3Color;
		
		[HideInInspector] public Text layer1;
		[HideInInspector] public Text layer2;
		[HideInInspector] public Text layer3;

		public void Init()
		{
			if (layer1 == null)
			{
				layer1 = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "layer_1");
			}

			if (layer2 == null)
			{
				layer2 = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "layer_2");
			}

			if (layer3 == null)
			{
				layer3 = Hierarchy.FindComponentInChildDeep<Text>(gameObject, "layer_3");
			}

			font = layer1.font;
			fontSize = layer1.fontSize;
			text = layer1.text;
			richText = layer1.supportRichText;

			offset.x = layer1.rectTransform.offsetMax.x;
			offset.y = layer1.rectTransform.offsetMin.y;


			layer1Color = layer1.color;
			layer2Color = layer2.color;
			layer3Color = layer3.color;
		}

		public void Refresh()
		{
			SetOffset(offset.x, offset.y);

			SetLayerColor(1, layer1Color);
			SetLayerColor(2, layer2Color);
			SetLayerColor(3, layer3Color);

			SetFont(font);
			SetFontSize(fontSize);
			SetRichText(richText);
			SetText(text);
		}

		public void SetOffset(float horizontal, float vertical)
		{
			SetLayerOffset(layer1, horizontal, vertical);
			SetLayerOffset(layer2, horizontal, vertical);
		}

		public void SetFont(Font font)
		{
			layer1.font = font;
			layer2.font = font;
			layer3.font = font;
		}

		public void SetFontSize(int size)
		{
			layer1.fontSize = size;
			layer2.fontSize = size;
			layer3.fontSize = size;
		}

		public void SetRichText(bool enabled)
		{
			layer1.supportRichText = enabled;
			layer2.supportRichText = enabled;
			layer3.supportRichText = enabled;
		}

		public void SetText(string text)
		{
			layer1.text = text;
			layer2.text = text;
			layer3.text = text;
		}

		public void SetLayerColor(int layer, Color color)
		{
			switch (layer)
			{
				case 1:
				{
					layer1.color = color;
					break;
				}

				case 2:
				{
					layer2.color = color;
					break;
				}

				case 3:
				{
					layer3.color = color;
					break;
				}

				default:
				{
					Debug.LogError("[LayeredLabel] layer is invalid");
					break;
				}
			}
		}

		public int GetLayerCount()
		{
			return 3;
		}

		private void SetLayerOffset(Text layer, float horizontal, float vertical)
		{
			var horOffset = layer.rectTransform.offsetMax;
			horOffset.x = horizontal;

			layer.rectTransform.offsetMax = horOffset;

			var verOffset = layer2.rectTransform.offsetMin;
			verOffset.y = vertical;

			layer.rectTransform.offsetMin = verOffset;
		}
	}
}
