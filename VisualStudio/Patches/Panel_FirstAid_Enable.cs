using VitaminCTracker.VitaminTracking;

namespace VitaminCTracker.Patches
{
	[HarmonyPatch(typeof(Panel_FirstAid), nameof(Panel_FirstAid.Enable), new Type[] { typeof(bool) })]
	public class Panel_FirstAid_Enable
	{
		private static bool WasInit { get; set; } = false;

		public static void Prefix(Panel_FirstAid __instance)
		{
			if (!__instance.enabled) return;

			Main.Logger.Log("Panel_FirstAid.Enable", FlaggedLoggingLevel.Debug);

			if (!WasInit)
			{
				Main.Logger.Log("!WasInit", FlaggedLoggingLevel.Debug);
				__instance.gameObject.AddComponent<VitaminTrackerBar>();
				WasInit = true;
			}
		}
	}
}
