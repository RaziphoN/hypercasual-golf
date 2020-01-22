using UnityEngine;
using UnityEngine.UI;

using Scripts.Framework.Utils;

namespace Scripts.UI
{
	public class LayeredLabel : MonoBehaviour
	{
		public bool automaticResize = true;

		public string text;

		public TMPro.TMP_FontAsset font;
		public float fontSize;
		public bool richText;
		public bool bestFit;
		public Vector2 bestFitSizeConstraint;
		public Vector2 offset;

		public Color layer1Color;
		public Color layer2Color;
		public Color layer3Color;
		
		[HideInInspector] public TMPro.TextMeshProUGUI layer1;
		[HideInInspector] public TMPro.TextMeshProUGUI layer2;
		[HideInInspector] public TMPro.TextMeshProUGUI layer3;

		public void Init()
		{
			if (layer1 == null)
			{
				layer1 = Hierarchy.FindComponentInChildDeep<TMPro.TextMeshProUGUI>(gameObject, "layer_1");
			}

			if (layer2 == null)
			{
				layer2 = Hierarchy.FindComponentInChildDeep<TMPro.TextMeshProUGUI>(gameObject, "layer_2");
			}

			if (layer3 == null)
			{
				layer3 = Hierarchy.FindComponentInChildDeep<TMPro.TextMeshProUGUI>(gameObject, "layer_3");
			}

			font = layer1.font;
			fontSize = layer1.fontSize;
			text = layer1.text;
			richText = layer1.richText;
			bestFit = layer1.enableAutoSizing;

			bestFitSizeConstraint.x = layer1.fontSizeMin;
			bestFitSizeConstraint.y = layer1.fontSizeMax;

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
			SetBestFit(bestFit);
			SetBestFitConstraints(bestFitSizeConstraint);

			if (!bestFit)
			{
				SetFontSize(fontSize);
			}

			SetRichText(richText);
			SetText(text);

			if (automaticResize)
			{
				ResizeToPreffered();
			}
		}

		public void SetBestFit(bool enabled)
		{
			layer1.enableAutoSizing = enabled;
			layer2.enableAutoSizing = enabled;
			layer3.enableAutoSizing = enabled;
		}

		public void SetBestFitConstraints(Vector2 constraints)
		{
			layer1.fontSizeMin = constraints.x;
			layer1.fontSizeMax = constraints.y;

			layer2.fontSizeMin = constraints.x;
			layer2.fontSizeMax = constraints.y;

			layer3.fontSizeMin = constraints.x;
			layer3.fontSizeMax = constraints.y;
		}

		public void SetOffset(float horizontal, float vertical)
		{
			SetLayerOffset(layer1, horizontal, vertical);
			SetLayerOffset(layer2, horizontal, vertical);
		}

		public void SetFont(TMPro.TMP_FontAsset font)
		{
			layer1.font = font;
			layer2.font = font;
			layer3.font = font;
		}

		public void SetFontSize(float size)
		{
			layer1.fontSize = size;
			layer2.fontSize = size;
			layer3.fontSize = size;
		}

		public void SetRichText(bool enabled)
		{
			layer1.richText = enabled;
			layer2.richText = enabled;
			layer3.richText = enabled;
		}

		public void SetText(string text)
		{
			layer1.text = text;
			layer2.text = text;
			layer3.text = text;
		}

		public string GetText()
		{
			return layer1.text;
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

		public Vector2 GetPrefferedSize()
		{
			Vector2 size = new Vector2();

			size.x = layer1.preferredWidth + (-offset.x) * 2;
			size.y = layer1.preferredHeight + offset.y * 2;

			return size;
		}

		public int GetLayerCount()
		{
			return 3;
		}

		private void SetLayerOffset(TMPro.TextMeshProUGUI layer, float horizontal, float vertical)
		{
			var horOffset = layer.rectTransform.offsetMax;
			horOffset.x = horizontal;

			layer.rectTransform.offsetMax = horOffset;

			var verOffset = layer2.rectTransform.offsetMin;
			verOffset.y = vertical;

			layer.rectTransform.offsetMin = verOffset;
		}

		private void ResizeToPreffered()
		{
			var size = GetPrefferedSize();
			var rectTransform = GetComponent<RectTransform>();

			rectTransform.sizeDelta = size;
		}
	}
}
