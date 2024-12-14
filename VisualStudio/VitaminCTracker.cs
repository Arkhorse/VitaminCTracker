#region System Directives
global using System;
global using System.Text.RegularExpressions;
#endregion
#region Il2Cpp Directives
#endregion
#region Unity Directives
#endregion
#region Mod Directives
global using VitaminCTracker.Utilities.Exceptions;
global using ComplexLogger;
#endregion

namespace VitaminCTracker
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	public class Main : MelonMod
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	{
		/// <summary>
		/// 
		/// </summary>
		public static ComplexLogger<Main> Logger { get; } = new();

        /// <inheritdoc/>
        public override void OnInitializeMelon()
        {
			Settings.OnLoad();
		}
		public static AssetBundle LoadAssetBundle(string name)
		{
			using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
			{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
				MemoryStream? memory = new((int)stream.Length);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
				stream!.CopyTo(memory);
				return AssetBundle.LoadFromMemory(memory.ToArray());
			};
		}
	}
}
