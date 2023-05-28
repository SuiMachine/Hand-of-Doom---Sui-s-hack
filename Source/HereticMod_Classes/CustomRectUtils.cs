using UnityEngine;

namespace SuisHack.HereticMod_Classes
{
	public static class CustomRectUtils
	{
		public static void SetStrech(this RectTransform ui)
		{
			ui.localScale = new Vector3(1, 1, 1);
			ui.pivot = new Vector2(0.5f, 0.5f);

			ui.anchorMin = new Vector2(0, 0);
			ui.anchorMax = new Vector2(1, 1);
			ui.anchoredPosition = new Vector2(0, 0);
			ui.sizeDelta = new Vector2(0, 0);
		}
	}
}
