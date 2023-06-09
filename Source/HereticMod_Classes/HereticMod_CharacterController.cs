using Rewired;
using System.Reflection;
using UnityEngine;

namespace SuisHack.HereticMod_Classes
{
	public static class HereticMod_CharacterController
	{
		public static bool Initialized;

		public static void Initialize()
		{
			if (Initialized)
				return;

			var sourceClass = typeof(CharacterController);
			var sourceMethod = sourceClass.GetMethod("Update", BindingFlags.Instance | BindingFlags.NonPublic);

			var targetClass = typeof(HereticMod_CharacterController);
			var targetMethod = new HarmonyLib.HarmonyMethod(targetClass.GetMethod(nameof(UpdateHooked), BindingFlags.Static | BindingFlags.NonPublic));

			Initialized = true;
			Plugin.HarmonyInst.Patch(sourceMethod, prefix: targetMethod);
		}

		static bool UpdateHooked(CharacterController __instance, ref float ___timer, ref bool ___skipTextOnScreen, ref Player ___player, ref scr_interact ___interactTarget)
		{
			if(__instance.videoPlayer.isPlaying)
			{
				___timer += Time.deltaTime;
				if(___skipTextOnScreen)
				{
					if(___player.GetButtonDown("TiltReset"))
					{
						__instance.videoPlayer.frame = (long)(__instance.videoPlayer.frameCount - 2UL);
						__instance.videoPlayer.transform.GetChild(0).gameObject.SetActive(false);
					}
				}
				else if(___player.GetAnyButtonDown())
				{
					___skipTextOnScreen = true;
					__instance.videoPlayer.transform.GetChild(0).gameObject.SetActive(true);
					___timer = 0f;
				}
				if(___timer > 2f)
				{
					___skipTextOnScreen = false;
					__instance.videoPlayer.transform.GetChild(0).gameObject.SetActive(false);
				}
			}

			Controller lastActiveController = ___player.controllers.GetLastActiveController();

			if(CharacterController.inCutscene)
			{
				CharacterController.canMove = false;
				CharacterController.canTurn = false;
				CharacterController.canTilt = false;
				GameObject gameObject = (GameObject)typeof(CharacterController).GetField("disabler", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
				if (gameObject == null || !gameObject.activeInHierarchy)
					CharacterController.EnableInteraction();
			}

			Vector3 eulerAngles = __instance.mainCam.transform.localEulerAngles;
			float lowerClampBound = -45f;
			float upperClampBound = 45;
			eulerAngles = new Vector3(ClampAngle(eulerAngles.x, lowerClampBound, upperClampBound), 0f, 0f);
			if(CharacterController.canTilt)
			{
				var tilt = Input.GetAxis("Mouse Y");
				if (tilt != 0f)
				{
					if (tilt > 0f && eulerAngles.x > lowerClampBound)
						__instance.mainCam.transform.Rotate(Vector3.right, tilt * -CharacterController.rotateSpeed * Time.deltaTime);
					else if(tilt < 0f && eulerAngles.x < upperClampBound)
						__instance.mainCam.transform.Rotate(Vector3.right, -tilt * CharacterController.rotateSpeed * Time.deltaTime);
				}
			}

			if (___player.GetButtonDown("Attack"))
				__instance.StartAttack();
			if(___player.GetButtonDown("Pause"))
			{
				if (CharacterController.paused)
					typeof(CharacterController).GetMethod("Pause", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(__instance, new object[] { });
				else
					__instance.Unpause();
			}

			if(CharacterController.canMove)
			{
				var verticalMove = ___player.GetAxis("MoveVertical");
				if (verticalMove != 0f)
				{
					var cc = __instance.GetCC();

					cc.Move(cc.transform.forward * verticalMove * Time.deltaTime * CharacterController.speed);
				}

				var strafeMove = ___player.GetAxis("Strafe");

				if (strafeMove != 0f)
				{
					var cc = __instance.GetCC();
					cc.Move(cc.transform.right * strafeMove * Time.deltaTime * CharacterController.speed);
				}
			}

			float turn;
			if(CharacterController.canTurn && (turn = Input.GetAxis("Mouse X")) != 0f)
			{
				var cc = __instance.GetCC();
				cc.transform.Rotate(0f, turn * Time.deltaTime * CharacterController.rotateSpeed, 0);
			}

			scr_interact scr_interactObject = ___interactTarget;
			if (scr_interactObject == null || !scr_interactObject.gameObject.activeInHierarchy)
				___interactTarget = null;

			return false;
		}

		private static FieldInfo GetCCFieldInfo;
		public static UnityEngine.CharacterController GetCC(this CharacterController controller)
		{
			if(GetCCFieldInfo == null)
			{
				GetCCFieldInfo = typeof(CharacterController).GetField("cc", BindingFlags.NonPublic | BindingFlags.Instance);
			}
			
			return (UnityEngine.CharacterController)GetCCFieldInfo.GetValue(controller);

		}

		private static float ClampAngle(float angle, float min, float max)
		{
			if (min < 0f && max > 0f && (angle > max || angle < min))
			{
				angle -= 360f;
				if (angle > max || angle < min)
				{
					if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max)))
					{
						return min;
					}
					return max;
				}
			}
			else if (min > 0f && (angle > max || angle < min))
			{
				angle += 360f;
				if (angle > max || angle < min)
				{
					if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max)))
					{
						return min;
					}
					return max;
				}
			}
			if (angle < min)
			{
				return min;
			}
			else if (angle > max)
			{
				return max;
			}
			return angle;
		}


	}
}
