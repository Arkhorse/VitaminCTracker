using Il2CppTLD.Gameplay;
using Il2CppTLD.UI.Generics;
using Il2CppTMPro;

using UnityEngine.UI;
using VitaminCTracker.Utilities;

namespace VitaminCTracker.VitaminTracking
{
	[RegisterTypeInIl2Cpp(false)]
	public class VitaminTrackerBar : MonoBehaviour
	{
		public VitaminTrackerBar() { }
		public VitaminTrackerBar(IntPtr pointer) : base(pointer) { }

		private bool PanelOpened;
		private bool Logged;
		public const string IconName = "ico_units_vitc";

		public AssetBundle Bundle;
		public GameObject VitaminCTrackerObject;
		public GameObject Canvas;
		public GameObject Panel;
		//public GameObject Root;
		//public GameObject Icon;
		public GameObject CleanBar;
		public GameObject Values;
		public GameObject Total;
		public GameObject Drain;

		public void Awake()
		{
			BuildUI();
			/* Old code incase things dont work
			VitaminCObject ??= new() { name = "VitaminCObject", layer = vp_Layer.UI };

			DontDestroyOnLoad(VitaminCObject);

			ConditionBar ??= gameObject.transform.GetChild(3).gameObject;
			Main.Logger.Log($"ConditionBar: {ConditionBar.name} | Enabled: {ConditionBar.active}", FlaggedLoggingLevel.Debug);

			VitaminCObject.transform.SetParent(gameObject.transform);
			VitaminCLabel = VitaminCObject.GetComponent<UILabel>();

			if (VitaminCLabel == null)
			{
				Main.Logger.Log("VitaminCLabel is null, creating", FlaggedLoggingLevel.Debug);

				VitaminCLabel = VitaminCObject.AddComponent<UILabel>();
			}

			VitaminCLabel.ambigiousFont				= GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
			VitaminCLabel.bitmapFont				= GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
			VitaminCLabel.font						= GameManager.GetFontManager().GetUIFontForCharacterSet(FontManager.m_CurrentCharacterSet);
			VitaminCLabel.alignment					= NGUIText.Alignment.Center;

			// -72.708f, -298.916f, 0f
			VitaminCLabel.transform.localPosition	= new Vector3(-72.708f, -298.916f, 0f);
			VitaminCLabel.fontSize					= 24;
			VitaminCLabel.enabled					= true;

			NGUITools.SetActive(VitaminCObject, true);
			*/
		}

		public void Update()
		{
			if (!Utilities.SceneUtilities.IsScenePlayable(GameManager.m_ActiveScene))
			{
				Main.Logger.Log($"{GameManager.m_ActiveScene} is not playable", FlaggedLoggingLevel.Verbose);
				return;
			}
			// If the panel isnt enabled, dont update to save performance
			if (InterfaceManager.GetPanel<Panel_FirstAid>() == null || !InterfaceManager.GetPanel<Panel_FirstAid>().enabled)
			{
				Main.Logger.Log($"Panel_FirstAid is either null or not enabled. Enabled: {InterfaceManager.GetPanel<Panel_FirstAid>().enabled}", FlaggedLoggingLevel.Verbose);
				return;
			}
			#region Object Null Checks
			if (VitaminCTrackerObject == null)
			{
				Main.Logger.Log($"VitaminCTrackerObject is null", FlaggedLoggingLevel.Verbose);
				return;
			}
			//if (Root == null)
			//{
			//	Main.Logger.Log($"Root is null", FlaggedLoggingLevel.Verbose);
			//	return;
			//}
			if (CleanBar == null)
			{
				Main.Logger.Log($"CleanBar is null", FlaggedLoggingLevel.Verbose);
				return;
			}
			if (Values == null)
			{
				Main.Logger.Log($"Values is null", FlaggedLoggingLevel.Verbose);
				return;
			}
			if (Total == null)
			{
				Main.Logger.Log($"Total is null", FlaggedLoggingLevel.Verbose);
				return;
			}
			if (Drain == null)
			{
				Main.Logger.Log($"Drain is null", FlaggedLoggingLevel.Verbose);
				return;
			}
			#endregion
			if (Il2CppTLD.Player.Nutrition.Instance == null)
			{
				Main.Logger.Log($"Il2CppTLD.Player.Nutrition.Instance is null", FlaggedLoggingLevel.Verbose);
				return;
			}
			if (Il2CppTLD.Player.Nutrition.Instance.m_Amounts == null || Il2CppTLD.Player.Nutrition.Instance.m_Amounts.Count == 0)
			{
				Main.Logger.Log($"Il2CppTLD.Player.Nutrition.Instance.m_Amounts is either null or empty", FlaggedLoggingLevel.Verbose);
				return;
			}

			CleanBarController();
			//IconController();
			ValuesController();
			
			//PanelOpened = InterfaceManager.GetPanel<Panel_FirstAid>().enabled;
			//if (PanelOpened && !Logged) OnPanelOpen();
			//if (!PanelOpened && Logged) Logged = false;
			//VitCAmount = Il2CppTLD.Player.Nutrition.Instance.m_Amounts[0].ToString("n2");
			//VitCRateLossDaily = Il2CppTLD.Player.Nutrition.Instance.m_LossPerDay[0].ToString();
			//NGUITools.SetActive(VitaminCLabel.gameObject, false);
			//VitaminCLabel.text = $"Current: {VitCAmount} / 500 | Rate of Loss: {VitCRateLossDaily}/day";
			//NGUITools.SetActive(VitaminCLabel.gameObject, true);
		}

		//public void LateUpdate()
		//{
		//	if (CleanBar == null) return;
		//	CleanBar.transform.position = new Vector3(660f, 90f, 0);
		//	// local incase normal doesnt work
		//	//CleanBar.transform.localPosition = new Vector3(-300f, -450f, 0);
		//}

		//public void OnPanelOpen()
		//{
		//	if (Logged) return;
		//	Main.Logger.Log($"VitaminTrackerBar.Update()", FlaggedLoggingLevel.Verbose);
		//	Logged = true;

		//	var nutrition = Il2CppTLD.Player.Nutrition.Instance;

		//	if (nutrition == null)
		//	{
		//		Main.Logger.Log("[Il2CppTLD.Player.Nutrition.Instance] is null", FlaggedLoggingLevel.Verbose);
		//		return;
		//	}
		//	if (Il2CppTLD.Player.Nutrition.s_Instance == null)
		//	{
		//		Main.Logger.Log("[Il2CppTLD.Player.Nutrition.s_Instance] is null", FlaggedLoggingLevel.Verbose);
		//	}
		//	else
		//	{
		//		Main.Logger.Log("[Il2CppTLD.Player.Nutrition.s_Instance] is not null", FlaggedLoggingLevel.Verbose);
		//	}

		//	float VitCNormalized = GameManager.GetScurvyComponent().GetVitaminCNormalized();
		//	float Amounts = Il2CppTLD.Player.Nutrition.Instance.m_Amounts.Count;
		//	float total = nutrition.m_Amounts[0];
		//	float drain = nutrition.m_LossPerDay[0];

		//	Main.Logger.Log($"Current: {total} / 500 | Rate of Loss: {drain}/day | Normalized Current: {VitCNormalized}", FlaggedLoggingLevel.Always);
		//	Main.Logger.Log($"Total number of float's in m_Amounts: {Amounts}", FlaggedLoggingLevel.Always);

		//	if (Amounts > 0)
		//	{
		//		for (int i = 0; i < Il2CppTLD.Player.Nutrition.Instance.m_Amounts.Count; i++)
		//		{
		//			Main.Logger.Log($"\tEntry number {i} in m_Amounts: {Il2CppTLD.Player.Nutrition.Instance.m_Amounts[i]}", FlaggedLoggingLevel.Verbose);
		//		}
		//	}
		//}

		public bool BuildUI()
		{
			Bundle = Main.LoadAssetBundle("VitaminCTracker.Resources.vitaminctracker");
			//var bundledobjects = Bundle.LoadAllAssets();
			VitaminCTrackerObject ??= Instantiate(Bundle.LoadAsset<GameObject>("assets/vitaminctracker.prefab"), gameObject.transform);
			if (VitaminCTrackerObject != null)
			{
				int children = VitaminCTrackerObject.transform.GetChildCount();
				Main.Logger.Log($"Successfully loaded root gameobject, number of children: {children}", FlaggedLoggingLevel.Debug);

				VitaminCTrackerObject.DontUnload();

				try
				{
					// do it this way so I dont get confused about the damn child hierachy
					Canvas = VitaminCTrackerObject.transform.GetChild(0).gameObject;
					Panel = Canvas.transform.GetChild(0).gameObject;
					//Root = Panel.transform.GetChild(0).gameObject;

					CleanBar = Panel.transform.GetChild(0).gameObject;

					Values = Panel.transform.GetChild(1).gameObject;
					Total = Values.transform.GetChild(0).gameObject;
					Drain = Values.transform.GetChild(1).gameObject;

					//Icon = Root.transform.GetChild(2).gameObject;
					//Icon.SetActive(false);
				}
				catch (Exception e)
				{
					Main.Logger.Log($"Attempting to assign all gameobjects failed", FlaggedLoggingLevel.Exception, e);
					return false;
				}

				return true;
			}
			return false;
		}

		public void SwapDirection(bool left)
		{
			var layout = Panel.GetComponent<HorizontalLayoutGroup>();
			layout.m_ReverseArrangement = !left;
		}
		//public void IconController()
		//{
		//	UISprite sprite = new();
		//	SetupUISprite(sprite, IconName);
		//	var icon = sprite.mainTexture;
		//	var s = Sprite.Create(icon as Texture2D, new Rect(0f, 0f, 24f, 24f), new(0.5f, 0.5f), 200f);
		//	Icon.GetComponent<Image>().sprite = s;
		//}
		public void CleanBarController()
		{
			var bar = CleanBar.GetComponent<Slider>();
			if (bar == null)
			{
				Main.Logger.Log($"CleanBar is null", FlaggedLoggingLevel.Verbose);
				return;
			}
			bar.value = Il2CppTLD.Player.Nutrition.Instance.m_Amounts[0];
		}

		public void ValuesController()
		{
			TextMeshProUGUI total = new();
			TextMeshProUGUI drain = new();

			try
			{
				total = Total.GetComponent<TextMeshProUGUI>();
				drain = Drain.GetComponent<TextMeshProUGUI>();
			}
			catch (Exception e)
			{
				Main.Logger.Log($"Attempting to get text components failed ", FlaggedLoggingLevel.Exception, e);
				return;
			}

			total.SetText(Il2CppTLD.Player.Nutrition.Instance.m_Amounts[0].ToString("N2"));
			drain.SetText($"{Il2CppTLD.Player.Nutrition.Instance.m_LossPerDay[0].ToString()}/Day");
		}

		public static void SetupUISprite(UISprite sprite, string spriteName)
		{
			UIAtlas baseAtlas = InterfaceManager.GetInstance().m_ScalableAtlases[0];
			UISpriteData spriteData = baseAtlas.GetSprite(spriteName);

			sprite.atlas = baseAtlas;
			sprite.spriteName = spriteName;
			sprite.mSprite = spriteData;
			sprite.mSpriteSet = true;
			//sprite.depth				= 30;
			//sprite.alpha                = 1f;
			//sprite.color                = Color.white;
			//sprite.MakePixelPerfect();
			//sprite.enabled              = true;

			//sprite.MakePixelPerfect();
		}
	}
}
