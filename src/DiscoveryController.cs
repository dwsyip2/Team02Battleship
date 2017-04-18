using System;
using SwinGameSDK;

/// <summary>
/// The battle phase is handled by the DiscoveryController.
/// </summary>
static class DiscoveryController
{

	/// <summary>
	/// Handles input during the discovery phase of the game.
	/// </summary>
	/// <remarks>
	/// Escape opens the game menu. Clicking the mouse will
	/// attack a location.
	/// </remarks>
	public static void HandleDiscoveryInput()
	{
		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.AddNewState(GameState.ViewingGameMenu);
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			DoAttack();
		}
	}

	/// <summary>
	/// Attack the location that the mouse if over.
	/// </summary>
	private static void DoAttack()
	{
		Point2D mouse = default(Point2D);

		mouse = SwinGame.MousePosition();

		//Calculate the row/col clicked
		int row = 0;
		int col = 0;
		row = Convert.ToInt32(Math.Floor((mouse.Y - UtilityFunctions.FIELD_TOP) / (UtilityFunctions.CELL_HEIGHT + UtilityFunctions.CELL_GAP)));
		col = Convert.ToInt32(Math.Floor((mouse.X - UtilityFunctions.FIELD_LEFT) / (UtilityFunctions.CELL_WIDTH + UtilityFunctions.CELL_GAP)));

		if (row >= 0 & row < GameController.HumanPlayer.EnemyGrid.Height) {
			if (col >= 0 & col < GameController.HumanPlayer.EnemyGrid.Width) {
				GameController.Attack(row, col);
			}
		}
	}

	/// <summary>
	/// Draws the game during the attack phase.
	/// </summary>s
	public static void DrawDiscovery()
	{
		const int SCORES_LEFT = 172;
		const int SHOTS_TOP = 157;
		const int HITS_TOP = 206;
		const int SPLASH_TOP = 256;

		if ((SwinGame.KeyDown(KeyCode.vk_LSHIFT) | SwinGame.KeyDown(KeyCode.vk_RSHIFT)) & SwinGame.KeyDown(KeyCode.vk_c)) {
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, true);
		} else {
			UtilityFunctions.DrawField(GameController.HumanPlayer.EnemyGrid, GameController.ComputerPlayer, false);
		}

		UtilityFunctions.DrawSmallField(GameController.HumanPlayer.PlayerGrid, GameController.HumanPlayer);
		UtilityFunctions.DrawMessage();

		SwinGame.DrawText(GameController.HumanPlayer.Shots.ToString(), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SHOTS_TOP);
		SwinGame.DrawText(GameController.HumanPlayer.Hits.ToString(), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, HITS_TOP);
		SwinGame.DrawText(GameController.HumanPlayer.Missed.ToString(), Color.White, GameResources.GameFont ("Menu"), SCORES_LEFT, SPLASH_TOP);

		SwinGame.FillRectangle (SwinGame.RGBAColor (0, 100, 200, 80), 480, 50, 295, 70);
		GameResources.GameFont ("Courier").FontStyle = FontStyle.BoldFont;
		SwinGame.DrawText ("Enemy's Ships", Color.Red, GameResources.GameFont ("Courier"), 500, 50);
		SwinGame.DrawText ("Tug", Color.White, GameResources.GameFont ("Courier"), 500, 60);
		SwinGame.DrawText ("Submarine", Color.White, GameResources.GameFont ("Courier"), 500, 70);
		SwinGame.DrawText ("Destroyer", Color.White, GameResources.GameFont ("Courier"), 500, 80);
		SwinGame.DrawText ("Battleship", Color.White, GameResources.GameFont ("Courier"), 500, 90);
		SwinGame.DrawText ("Aircraft carrier", Color.White, GameResources.GameFont ("Courier"), 500, 100);

		if (GameController.ComputerPlayer [ShipName.Tug].IsDestroyed) {
			SwinGame.DrawText ("Terminated", Color.Red, GameResources.GameFont ("Courier"), 670, 60);
		} else {
			SwinGame.DrawText ("Deployed", Color.LimeGreen, GameResources.GameFont ("Courier"), 670, 60);
		}
		if (GameController.ComputerPlayer [ShipName.Submarine].IsDestroyed) {
			SwinGame.DrawText ("Terminated", Color.Red, GameResources.GameFont ("Courier"), 670, 70);
		} else {
			SwinGame.DrawText ("Deployed", Color.LimeGreen, GameResources.GameFont ("Courier"), 670, 70);
		}
		if (GameController.ComputerPlayer [ShipName.Destroyer].IsDestroyed) {
			SwinGame.DrawText ("Terminated", Color.Red, GameResources.GameFont ("Courier"), 670, 80);
		} else {
			SwinGame.DrawText ("Deployed", Color.LimeGreen, GameResources.GameFont ("Courier"), 670, 80);
		}
		if (GameController.ComputerPlayer [ShipName.Battleship].IsDestroyed) {
			SwinGame.DrawText ("Terminated", Color.Red, GameResources.GameFont ("Courier"), 670, 90);
		} else {
			SwinGame.DrawText ("Deployed", Color.LimeGreen, GameResources.GameFont ("Courier"), 670, 90);
		}
		if (GameController.ComputerPlayer [ShipName.AircraftCarrier].IsDestroyed) {
			SwinGame.DrawText ("Terminated", Color.Red, GameResources.GameFont ("Courier"), 670, 100);
		} else {
			SwinGame.DrawText ("Deployed", Color.LimeGreen, GameResources.GameFont ("Courier"), 670, 100);
		}
		GameResources.GameFont ("Courier").FontStyle = FontStyle.NormalFont;
	}

}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
