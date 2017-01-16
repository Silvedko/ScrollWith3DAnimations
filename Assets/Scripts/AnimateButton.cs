using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class AnimateButton : MonoBehaviour 
{
	[SerializeField]
	private Button animateButton;

	[SerializeField]
	private Text buttonText;

	private string defaultButtonText = "Animate object";
	private string pressedButtonText = "Stop animation";

	private bool isButtonPressed = false;

	/// <summary>
	/// Raises the animate button clicked event.
	/// </summary>
	public void OnAnimateButtonClicked ()
	{
		isButtonPressed = !isButtonPressed;
		buttonText.text = isButtonPressed ? pressedButtonText : defaultButtonText;
		SceneController.AnimateCurrentActiveModel (isButtonPressed);
	}

	/// <summary>
	/// Raises when the new ScrollViewCell selected. Stops animation and change button text.
	/// </summary>
	private void OnAnyCellSelectedHandler ()
	{
		animateButton.interactable = true;
		isButtonPressed = false;
		buttonText.text = defaultButtonText;
	}
		
	void Start () 
	{
		SceneController.OnAnyCellSelected += OnAnyCellSelectedHandler;

		if (animateButton == null)
			animateButton = GetComponent <Button> ();

		buttonText.text = defaultButtonText;

		animateButton.onClick.AddListener (OnAnimateButtonClicked);
		animateButton.interactable = false;
	}

	void OnDestroy ()
	{
		animateButton.onClick.RemoveListener (OnAnimateButtonClicked);
		SceneController.OnAnyCellSelected -= OnAnyCellSelectedHandler;
	}
}
