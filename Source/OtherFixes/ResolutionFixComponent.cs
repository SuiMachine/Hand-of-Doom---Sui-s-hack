using UnityEngine;

namespace SuisHack.OtherFixes
{
	public class ResolutionFixComponent : MonoBehaviour
	{
		ResolutionMenu resMenuReference;

		private void OnEnable()
		{
			if (resMenuReference == null)
				resMenuReference = FindObjectOfType<ResolutionMenu>();

			if (resMenuReference.fullScreenToggle == null || resMenuReference.resolutionDropdown == null)
			{
				resMenuReference.fullScreenToggle = this.GetComponentInChildren<UnityEngine.UI.Toggle>();
				resMenuReference.resolutionDropdown = this.GetComponentInChildren<TMPro.TMP_Dropdown>();

				resMenuReference.fullScreenToggle.onValueChanged.RemoveAllListeners();
				resMenuReference.resolutionDropdown.onValueChanged.RemoveAllListeners();

				typeof(ResolutionMenu).GetMethod("Start", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(resMenuReference, new object[] { });
				Plugin.LogMessage("Null reference should now be corrected...");
			}
		}
	}
}
