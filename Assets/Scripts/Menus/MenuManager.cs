using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public Menu CurrentMenu;

	public void Start ()
	{
		ShowMenu (CurrentMenu);
	}

	public void openLevelSelect ()
	{
		Application.LoadLevel ("LevelSelectScreen");
	}

	public void openGhostDemo (int level)
	{
		Application.LoadLevel ("GhostDemo" + level);
	}

	public void ShowMenu (Menu menu)
	{
		if (CurrentMenu != null) {
			CurrentMenu.IsOpen = false;
		}
		if (menu != null) {
			CurrentMenu = menu;
			CurrentMenu.IsOpen = true;
		}
	}
}
