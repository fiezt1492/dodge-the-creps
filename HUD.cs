using Godot;
using System;

public class HUD : CanvasLayer
{
	// Don't forget to rebuild the project so the editor knows about the new signal.

	[Signal]
	public delegate void StartGame();
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void ShowMessage(string text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();

		GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver()
	{
		ShowMessage("Game Over");

		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer, "timeout");

		var message = GetNode<Label>("Message");
		message.Text = "Dodge the\nCreeps!";
		message.Show();

		await ToSignal(GetTree().CreateTimer(1), "timeout");
		GetNode<Button>("StartButton").Show();
	}

	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}

	public void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal("StartGame");
	}

	public void OnMessageTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//
	//  }
}
