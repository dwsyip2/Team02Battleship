using SwinGameSDK;

/// <summary>
/// The menu controller handles the drawing and user interactions
/// from the menus in the game. These include the main menu, game
/// menu and the settings m,enu.
/// </summary>

static class MenuController
{

	/// <summary>
	/// The menu structure for the game.
	/// </summary>
	/// <remarks>
	/// These are the text captions for the menu items.
	/// </remarks>
	private static readonly string [] [] _menuStructure = {
		new string[] {
			"PLAY",
			"SETUP",
			"SETTINGS",
			"SCORES",
			"HOW TO PLAY",
			"CHANGE MUSIC",
			"QUIT"
		},
		new string[] {
			"RETURN",
			"SURRENDER",
			"QUIT"
		},
		new string[] {
			"EASY",
			"MEDIUM",
			"HARD"
		},
		new string[] {
			"SOUND ON",
			"MUSIC ON"
		},
		new string[] {
			"BGM 1",
			"BGM 2",
			"BGM 3"
		}
	};
	private const int MENU_TOP = 150;
	private const int MENU_LEFT = 575;
	private const int MENU_GAP = 0;
	private const int BUTTON_WIDTH = 150;
	private const int BUTTON_HEIGHT = 50;
	private const int BUTTON_SEP = BUTTON_WIDTH + MENU_GAP;

	private const int TEXT_OFFSET = -10;
	private const int MAIN_MENU = 0;
	private const int GAME_MENU = 1;
	private const int SETUP_MENU = 2;
	private const int SETTING_MENU = 3;
	private const int BGM_MENU = 4;

	private const int MAIN_MENU_PLAY_BUTTON = 0;
	private const int MAIN_MENU_SETUP_BUTTON = 1;
	private const int MAIN_MENU_SETTING_BUTTON = 2;
	private const int MAIN_MENU_TOP_SCORES_BUTTON = 3;
	private const int MAIN_MENU_HOW_TO_PLAY_BUTTON = 4;
	private const int MAIN_MENU_MUSIC_BUTTON = 5;
	private const int MAIN_MENU_QUIT_BUTTON = 6;

	private const int SETUP_MENU_EASY_BUTTON = 0;
	private const int SETUP_MENU_MEDIUM_BUTTON = 1;
	private const int SETUP_MENU_HARD_BUTTON = 2;
	private const int SETUP_MENU_EXIT_BUTTON = 3;

	private const int SETTING_MENU_SOUND_TOGGLE = 0;
	private const int SETTING_MENU_MUSIC_TOGGLE = 1;

	private const int BGM_MENU_1 = 0;
	private const int BGM_MENU_2 = 1;
	private const int BGM_MENU_3 = 2;

	private const int GAME_MENU_RETURN_BUTTON = 0;
	private const int GAME_MENU_SURRENDER_BUTTON = 1;
	private const int GAME_MENU_QUIT_BUTTON = 2;

	private static readonly Color MENU_COLOR = SwinGame.RGBAColor(2, 167, 252, 255);

	private static readonly Color HIGHLIGHT_COLOR = SwinGame.RGBAColor(1, 57, 86, 255);


	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleMainMenuInput()
	{
		HandleMenuInput(MAIN_MENU, 0, 0);
	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleSetupMenuInput()
	{
		bool handled = false;
		handled = HandleMenuInput(SETUP_MENU, 1, 1);

		if (!handled) {
			HandleMenuInput(MAIN_MENU, 0, 0);
		}
	}

	public static void HandleBGMMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (BGM_MENU, 1, 3);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}


	}

	/// <summary>
	/// Handles the processing of user input when the main menu is showing
	/// </summary>
	public static void HandleSettingMenuInput ()
	{
		bool handled = false;
		handled = HandleMenuInput (SETTING_MENU, 1, 2);

		if (!handled) {
			HandleMenuInput (MAIN_MENU, 0, 0);
		}
	}

	/// <summary>
	/// Handle input in the game menu.
	/// </summary>
	/// <remarks>
	/// Player can return to the game, surrender, or quit entirely
	/// </remarks>
	public static void HandleGameMenuInput()
	{
		HandleMenuInput(GAME_MENU, 0, 0);
	}

	/// <summary>
	/// Handles input for the specified menu.
	/// </summary>
	/// <param name="menu">the identifier of the menu being processed</param>
	/// <param name="level">the vertical level of the menu</param>
	/// <param name="xOffset">the xoffset of the menu</param>
	/// <returns>false if a clicked missed the buttons. This can be used to check prior menus.</returns>
	private static bool HandleMenuInput(int menu, int level, int xOffset)
	{
		if (SwinGame.KeyTyped(KeyCode.vk_ESCAPE)) {
			GameController.EndCurrentState();
			return true;
		}

		if (SwinGame.MouseClicked(MouseButton.LeftButton)) {
			int i = 0;
			for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
				//IsMouseOver the i'th button of the menu
				if (IsMouseOverMenu(i, level, xOffset)) {
					PerformMenuAction(menu, i);
					return true;
				}
			}

			if (level > 0) {
				//none clicked - so end this sub menu
				GameController.EndCurrentState();
			}
		}

		return false;
	}


	/// <summary>
	/// Draws the main menu to the screen.
	/// </summary>
	public static void DrawMainMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Main Menu", Color.White, GameResources.GameFont("ArialLarge"), 50, 50)
		GameResources.GameFont ("New").FontStyle = FontStyle.BoldFont;
		SwinGame.DrawText ("Press 'F' to toggle Fullscreen", Color.AliceBlue, GameResources.GameFont ("New"), 15, 556);
		DrawButtons(MAIN_MENU);
	}

	/// <summary>
	/// Draws the Game menu to the screen
	/// </summary>
	public static void DrawGameMenu()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Paused", Color.White, GameResources.GameFont("ArialLarge"), 50, 50)

		DrawButtons(GAME_MENU);
	}

	public static void DrawSetting ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (SETTING_MENU, 1, 2);
	}

	/// <summary>
	/// Draws the settings menu to the screen.
	/// </summary>
	/// <remarks>
	/// Also shows the main menu
	/// </remarks>
	public static void DrawSetup()
	{
		//Clears the Screen to Black
		//SwinGame.DrawText("Settings", Color.White, GameResources.GameFont("ArialLarge"), 50, 50)

		DrawButtons(MAIN_MENU);
		DrawButtons(SETUP_MENU, 1, 1);
	}

	public static void DrawBGMSettings ()
	{
		DrawButtons (MAIN_MENU);
		DrawButtons (BGM_MENU, 1, 3);
	}

	/// <summary>
	/// Draw the buttons associated with a top level menu.
	/// </summary>
	/// <param name="menu">the index of the menu to draw</param>
	private static void DrawButtons(int menu)
	{
		DrawButtons(menu, 0, 0);
	}

	/// <summary>
	/// Draws the menu at the indicated level.
	/// </summary>
	/// <param name="menu">the menu to draw</param>
	/// <param name="level">the level (height) of the menu</param>
	/// <param name="xOffset">the offset of the menu</param>
	/// <remarks>
	/// The menu text comes from the _menuStructure field. The level indicates the height
	/// of the menu, to enable sub menus. The xOffset repositions the menu horizontally
	/// to allow the submenus to be positioned correctly.
	/// </remarks>
	private static void DrawButtons(int menu, int level, int xOffset)
	{
		int btnTop = 0;
		int btnLeft = 0;
		btnLeft = MENU_LEFT - BUTTON_SEP * (level);
		int i = 0;
		for (i = 0; i <= _menuStructure[menu].Length - 1; i++) {
			btnTop = MENU_TOP + (MENU_GAP + BUTTON_HEIGHT) * (i + xOffset);
			SwinGame.FillRectangle (Color.Black, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			SwinGame.DrawRectangle (Color.White, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			SwinGame.DrawTextLines(_menuStructure [menu] [i], MENU_COLOR, Color.Transparent, GameResources.GameFont ("Menu"), FontAlignment.AlignRight, btnLeft + TEXT_OFFSET, btnTop+ BUTTON_HEIGHT/2+TEXT_OFFSET/2, BUTTON_WIDTH, BUTTON_HEIGHT);
			if (SwinGame.MouseDown (MouseButton.LeftButton) & IsMouseOverMenu (i, level, xOffset)) {
				SwinGame.DrawRectangle (Color.Red, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			} else if (IsMouseOverMenu (i, level, xOffset)) {
				SwinGame.DrawRectangle (HIGHLIGHT_COLOR, btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
			}
		}
	}

	/// <summary>
	/// Determined if the mouse is over one of the button in the main menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <returns>true if the mouse is over that button</returns>
	private static bool IsMouseOverButton(int button)
	{
		return IsMouseOverMenu(button, 0, 0);
	}

	/// <summary>
	/// Checks if the mouse is over one of the buttons in a menu.
	/// </summary>
	/// <param name="button">the index of the button to check</param>
	/// <param name="level">the level of the menu</param>
	/// <param name="xOffset">the xOffset of the menu</param>
	/// <returns>true if the mouse is over the button</returns>
	private static bool IsMouseOverMenu(int button, int level, int xOffset)
	{
		int btnTop = MENU_TOP + (MENU_GAP + BUTTON_HEIGHT) *(button + xOffset);
		int btnLeft = MENU_LEFT - BUTTON_SEP * (level);

		return UtilityFunctions.IsMouseInRectangle(btnLeft, btnTop, BUTTON_WIDTH, BUTTON_HEIGHT);
	}

	/// <summary>
	/// A button has been clicked, perform the associated action.
	/// </summary>
	/// <param name="menu">the menu that has been clicked</param>
	/// <param name="button">the index of the button that was clicked</param>
	private static void PerformMenuAction(int menu, int button)
	{
		switch (menu) {
			case MAIN_MENU:
				PerformMainMenuAction(button);
				break;
			case SETUP_MENU:
				PerformSetupMenuAction(button);
				break;
			case SETTING_MENU:
				PerformSettingMenuAction (button);
				break;
			case GAME_MENU:
				PerformGameMenuAction(button);
				break;
		    case BGM_MENU:
			    PerformBGMMenuAction (button);
			    break;

		
		}
	}

	/// <summary>
	/// The main menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformMainMenuAction(int button)
	{
		switch (button) {
			case MAIN_MENU_PLAY_BUTTON:
				GameController.StartGame();
				break;
			case MAIN_MENU_SETUP_BUTTON:
				GameController.AddNewState(GameState.AlteringSetup);
				break;
			case MAIN_MENU_SETTING_BUTTON:
				GameController.AddNewState (GameState.AlteringSetting);
				break;
			case MAIN_MENU_TOP_SCORES_BUTTON:
				GameController.AddNewState(GameState.ViewingHighScores);
				break;
		    case MAIN_MENU_HOW_TO_PLAY_BUTTON:
		     	GameController.AddNewState (GameState.HowToPlay);
		    	break;
	    	case MAIN_MENU_MUSIC_BUTTON:
			    GameController.AddNewState (GameState.BGMSettings);
			    break;
			case MAIN_MENU_QUIT_BUTTON:
				GameController.AddNewState (GameState.Quitting);
				break;
		}
	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSetupMenuAction(int button)
	{
		switch (button) {
			case SETUP_MENU_EASY_BUTTON:
				GameController.SetDifficulty(AIOption.Easy);
				break;
			case SETUP_MENU_MEDIUM_BUTTON:
				GameController.SetDifficulty(AIOption.Medium);
				break;
			case SETUP_MENU_HARD_BUTTON:
				GameController.SetDifficulty(AIOption.Hard);
				break;
		}
		//Always end state - handles exit button as well
		GameController.EndCurrentState();
	}

	private static void PerformBGMMenuAction (int button)
	{
		switch (button) {
		case BGM_MENU_1:
			Audio.StopMusic ();
			SwinGame.PlayMusic (GameResources.GameMusic ("Background"));
			break;
		case BGM_MENU_2:
			Audio.StopMusic ();
			SwinGame.PlayMusic (GameResources.GameMusic ("BGM1"));
			break;
		case BGM_MENU_3:
			Audio.StopMusic ();
			SwinGame.PlayMusic (GameResources.GameMusic ("BGM2"));
			break;
		}
		GameController.EndCurrentState ();
	}

	/// <summary>
	/// The setup menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformSettingMenuAction (int button)
	{
		switch (button) {
			case SETTING_MENU_SOUND_TOGGLE:
				GameController.ToggleSound ();
				if (GameController.getSoundState)
					_menuStructure [SETTING_MENU] [0] = "SOUND ON";
				else
					_menuStructure [SETTING_MENU] [0] = "SOUND OFF";
			break;
			case SETTING_MENU_MUSIC_TOGGLE:
				GameController.ToggleMusic ();
				if (GameController.getMusicState)
					_menuStructure [SETTING_MENU] [1] = "MUSIC ON";
				else
					_menuStructure [SETTING_MENU] [1] = "MUSIC OFF";
			break;
		}
		//Always end state - handles exit button as well
		GameController.EndCurrentState ();
	}
	/// <summary>
	/// The game menu was clicked, perform the button's action.
	/// </summary>
	/// <param name="button">the button pressed</param>
	private static void PerformGameMenuAction(int button)
	{
		switch (button) {
			case GAME_MENU_RETURN_BUTTON:
				GameController.EndCurrentState();
				break;
			case GAME_MENU_SURRENDER_BUTTON:
				GameController.EndCurrentState();
				//end game menu
				GameController.EndCurrentState();
				//end game
				break;
			case GAME_MENU_QUIT_BUTTON:
				GameController.AddNewState(GameState.Quitting);
				break;
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
