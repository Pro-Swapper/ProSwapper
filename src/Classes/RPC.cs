using System;
using DiscordRPC;
namespace Pro_Swapper
{
	public class RPC
	{
		public static DiscordRpcClient client;
		public static Timestamps rpctimestamp { get; set; }
		private static RichPresence presence;
		public static void InitializeRPC()
		{
			client = new DiscordRpcClient("697579712653819985");
			client.Initialize();
			Button[] buttons = { new Button() { Label = "Discord", Url = API.api.apidata.discordurl }, new Button() { Label = "YouTube", Url = "https://youtube.com/proswapperofficial"} };

			presence = new RichPresence()
			{
				Details = "Pro Swapper | " + global.version,
				State = "Idle",
				Timestamps = rpctimestamp,
				Buttons = buttons,

				Assets = new Assets()
				{
					LargeImageKey = "logotransparent",
					LargeImageText = "Pro Swapper",
					SmallImageKey = "proswapperman",
					SmallImageText = "Made by Kye#5000"
				}
			};
			SetState("Idle");
		}
		public static void SetState(string state, bool watching = false)
		{
			Program.logger.Log($"Set Discord RPC to {state}");
			if (watching) 
				state = "Watching " + state;

			presence.State = state;
			client.SetPresence(presence);
		}
	}
}
