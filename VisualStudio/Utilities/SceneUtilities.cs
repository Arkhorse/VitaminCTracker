// ---------------------------------------------
// ScenenUtilities - by The Illusion
// ---------------------------------------------
// Reusage Rights ------------------------------
// You are free to use this script or portions of it in your own mods, provided you give me credit in your description and maintain this section of comments in any released source code
//
// Warning !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
// Ensure you change the namespace to whatever namespace your mod uses, so it doesnt conflict with other mods
// ---------------------------------------------

namespace VitaminCTracker.Utilities
{
	/// <summary>
	/// 
	/// </summary>
	public class SceneUtilities
	{
		/// <summary>
		/// Get if the current scene is indoor
		/// </summary>
		/// <param name="scene">true if you want the scene, false if you want the environment</param>
		/// <returns></returns>
		public static bool IsSceneIndoor(bool scene)
		{
			return (GameManager.GetWeatherComponent().IsIndoorScene() && scene) || GameManager.GetWeatherComponent().IsIndoorEnvironment();
		}

		/// <summary>
		/// Used to check if the current scene is EMPTY
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneEmpty(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && sceneName.Contains("Empty", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Used to check if the current scene is BOOT
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneBoot(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && sceneName.Contains("Boot", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Used to check if the current scene is a main menu scene
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneMenu(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && sceneName.StartsWith("MainMenu", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Used to check if the current scene is a playable scene. Use this for most needs
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsScenePlayable(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && !IsSceneEmpty(sceneName) && !IsSceneBoot(sceneName) && !IsSceneMenu(sceneName);
		}

		/// <summary>
		/// Used to check if the current scene is a base scene (Zone or Region)
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneBase(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && (sceneName.Contains("Region", StringComparison.InvariantCultureIgnoreCase) || sceneName.Contains("Zone", StringComparison.InvariantCultureIgnoreCase));
		}

		/// <summary>
		/// Used to check if the current scene is a sandbox scene
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneSandbox(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && sceneName.EndsWith("SANDBOX", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Used to check if the current scene is a DLC01 scene
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneDLC01(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && sceneName.EndsWith("DLC01", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Used to check if the current scene is a DARKWALKER scene
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneDarkWalker(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && sceneName.Contains("DARKWALKER", StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Used to check if the current scene is an additive scene, like sandbox or DLC scenes added to the base scene
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <returns></returns>
		public static bool IsSceneAdditive(string? sceneName = null)
		{
			sceneName ??= GameManager.m_ActiveScene;

			return sceneName != null && (IsSceneSandbox(sceneName) || IsSceneDLC01(sceneName) || IsSceneDarkWalker(sceneName));
		}

		/// <summary>
		/// Used to check if the current scene is valid for weather
		/// </summary>
		/// <param name="sceneName">The name of the scene to check, if null will use <c>GameManager.m_ActiveScene</c></param>
		/// <param name="IndoorOverride"></param>
		/// <returns></returns>
		public static bool IsValidSceneForWeather(string sceneName, bool IndoorOverride)
		{
			sceneName ??= GameManager.m_ActiveScene;

			// this is done this way to make it easier to see the logic
			return sceneName != null
				&& (
					( IsSceneBase(sceneName) && !(IsSceneAdditive(sceneName)) ) && !GameManager.GetWeatherComponent().IsIndoorScene()
					)
				|| (GameManager.GetWeatherComponent().IsIndoorScene() && IndoorOverride);
		}

		#region TryGet
		/// <summary>
		/// Attempt to get the human readable name of a region
		/// </summary>
		/// <param name="name">The internal name of a region</param>
		/// <param name="value">The output of the quary</param>
		/// <returns><see langword="true"/> if the <see cref="Dictionary{TKey, TValue}"/> contains an element with the specific key, otherwise <see langword="false"/></returns>
		/// <remarks>
		/// The <see cref="KeyNotFoundException"/> is properly handled, printing the error to the log rather than throwing an exception. All other possible <see cref="Dictionary{TKey, TValue}.TryGetValue(TKey, out TValue)"/> exceptions are not handled
		/// </remarks>
		public static bool TryGetRegionName(string name, out string? value)
		{
			try
			{
				return RegionNames.TryGetValue(name, out value);
			}
			catch (KeyNotFoundException knf)
			{
				Main.Logger.Log($"TryGetRegionName({name})::Value requested was not found in RegionNames", FlaggedLoggingLevel.Exception, knf);
				try
				{
					return RegionNamesDLC01.TryGetValue(name, out value);
				}
				catch (KeyNotFoundException knfe)
				{
					Main.Logger.Log($"TryGetRegionName({name})::Value requested was not found in RegionNamesDLC01", FlaggedLoggingLevel.Exception, knfe);
				}
			}
			value = null;
			return false;
		}
		#endregion

		#region Converters
		/// <summary>
		/// Contains definitions for the scenes in the game
		/// </summary>
		public static Dictionary<string, string> RegionNames = new()
		{
			{ "AshCanyonRegion", "Ash Canyon" },
			{ "BlackrockPrisonSurvivalZone", "Blackrock Prison Buildings" },
			{ "BlackrockRegion", "Blackrock" },
			{ "BlackrockTransitionZone", "Keeper’s Pass North" },
			{ "Cannery", "Bleak Inlet" },
			{ "CanyonRoadTransitionZone", "Keeper’s Pass South" },
			{ "CoastalRegion", "Coastal Highway" },
			{ "CrashMountainRegion", "Timberwolf Mountain" },
			{ "DamCaveTransitionZone", "Cave System" },
			{ "DamRiverTransitionZone", "Winding River" },
			{ "DamTransitionZone", "Lower Dam" },
			{ "HighwayMineTransitionZone", "UNKOWN" },
			{ "HighwayTransitionZone", "Crumbling Highway" },
			{ "LakeRegion", "Mystery Lake" },
			{ "MarshRegion", "Forlorn Muskeg" },
			{ "MineTransitionZone", "No. 3 Coal Mine" },
			{ "MountainTownRegion", "Mountain Town" },
			{ "RavineTransitionZone", "Revine" },
			{ "RiverValleyRegion", "Hushed River Valley" },
			{ "RuralRegion", "Pleasant Valley" },
			{ "TracksRegion", "Broken Railroad" },
			{ "WhalingStationRegion", "Desolation Point" }
		};

		/// <summary>
		/// Contains definitions for the scenes in the game for DLC01 or Tales From The Far Territory (TFTFT)
		/// </summary>
		public static Dictionary<string, string> RegionNamesDLC01 = new()
		{
			{ "AirfieldRegion", "Forsaken Airfield" },
			{ "HubRegion", "Transfer Pass" },
			{ "LongRailTransitionZone", "Far Range Branch Line" },
			{ "MiningRegion", "Zone of Contamination" }
		};

		/// <summary>
		/// 
		/// </summary>
		public static Dictionary<string, string> Interiors = new()
		{
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" },
			{ "", "" }
		};
		#endregion
	}
}
