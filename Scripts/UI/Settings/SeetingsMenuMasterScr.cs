using Godot;
using System;

public partial class SeetingsMenuMasterScr : Node2D
{
	//to simply references for control buttons
	[Export] public Button[] ControlButtons;
	public bool Active_Button_Input = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GameSetRes(Vector2I NewS){
		GetWindow().Size = NewS;
		//ProjectSettings.SetSetting("display/window/size/width",NewS.X);
		//ProjectSettings.SetSetting("display/window/size/height",NewS.Y);
		//ProjectSettings.Save();
	}

	public void Toggle_Vsync(){
		DisplayServer.WindowSetVsyncMode((DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled ? DisplayServer.VSyncMode.Disabled : DisplayServer.VSyncMode.Enabled));
	}
	public void Get_Vsync(NodePath Path){
		GetNode<Button>(Path).ButtonPressed = (DisplayServer.WindowGetVsyncMode() == DisplayServer.VSyncMode.Enabled);
	}
	public void Set_FOV(float value, NodePath Text_Path){
		GetNode<Label>(Text_Path).Text = ("FOV : " + value.ToString());
		GD.Print(GetNode("/root/OptionsManager") != null);
		GetNode("/root/OptionsManager").Call("SetFOV",value);
		//global
	}

	public void Set_Mono(){

	}

	public void Set_Button(int b){
		Active_Button_Input = true;
		
	}
}
