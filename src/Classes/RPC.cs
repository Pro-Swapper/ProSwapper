using System;
using DiscordRPC;
namespace Pro_Swapper
{
	public class RPC
	{
		public static DiscordRpcClient client;
		public static Timestamps rpctimestamp { get; set; }
		public static void InitializeRPC()
		{
			client = new DiscordRpcClient("697579712653819985");
			client.Initialize();
			SetState("Idle", false);
		}

		public static void SetState(string state, bool watching)
		{
			string discordurl = Convert.ToString(api.apidata.discordurl);
			Button[] buttons = new Button[] { new Button() { Label = "Discord", Url = discordurl } };
			if (watching) state = "Watching " + state;
			client.SetPresence(new RichPresence()
			{
				Details = "Pro Swapper | " + global.version,
				State = state,
				Timestamps = rpctimestamp,
				Buttons = buttons,

				Assets = new Assets()
				{
					LargeImageKey = "logotransparent",
					LargeImageText = "Pro Swapper",
					SmallImageKey = "proswapperman",
					SmallImageText = "Made by Kye#5000"
				}
			});
		}
	}
}
