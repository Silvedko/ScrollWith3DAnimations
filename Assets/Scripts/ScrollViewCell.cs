using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollViewCell : MonoBehaviour 
{
	public delegate void ScrollViewCellDelegate (ScrollViewCell cell);
	public event ScrollViewCellDelegate OnCellSelected;

	//3d-model for current button
	//You must manually set 3d-model object in scene for each cell in Inspector.
	public ModelObject model; 

	/// <summary>
	/// Gets a value indicating whether this cell is selected.
	/// </summary>
	/// <value><c>true</c> if this instance is cell selected; otherwise, <c>false</c>.</value>
	public bool IsSelected 
	{
		get { return isSelected; }
		private set 
		{ 
			if (value)
			{
				SetCellColor (selectionCellColor);

				if (OnCellSelected != null)
					OnCellSelected (this);
			}
			else
				SetCellColor (deselectionCellColor);
			
			isSelected = value; 
		}
	}
	private bool isSelected = false;

	/// <summary>
	/// The cell title text. 
	/// </summary>
	[SerializeField]
	private Text cellTitleText;

	/// <summary>
	/// The color of the selection cell.
	/// </summary>
	[SerializeField]
	private Color selectionCellColor = Color.blue;

	/// <summary>
	/// The color of the deselection cell.
	/// </summary>
	[SerializeField]
	private Color deselectionCellColor = Color.white;

	/// <summary>
	/// The button for handling clicks on cell.
	/// </summary>
	[SerializeField]
	private Button btn;

	/// <summary>
	/// The preview icon of the model.
	/// </summary>
	[SerializeField]
	private Image modelPreviewImage;

	/// <summary>
	/// The cells image. Background of the cell.
	/// </summary>
	private Image cellImage;

	/// <summary>
	/// Deselects the cell. Set default color and set IsCellActive to false
	/// </summary>
	public void DeselectCell ()
	{
		IsSelected = false;
		model.AnimateModel (false);
		model.SetModelActive (false);
	}

	void Start () 
	{
		btn.onClick.AddListener (OnCellClickedHandler);
		modelPreviewImage.sprite = model.modelPrewiewSprite;
		cellImage = btn.image;
		cellTitleText.text = model.modelName;
	}

	void OnDestroy ()
	{
		btn.onClick.RemoveListener (OnCellClickedHandler);
	}

	/// <summary>
	/// Raises the cell clicked handler event.
	/// </summary>
	void OnCellClickedHandler () 
	{
		IsSelected = true;
		model.SetModelActive (true);
	}

	/// <summary>
	/// Sets the color of the cell.
	/// </summary>
	/// <param name="color">Color.</param>
	void SetCellColor (Color color)
	{
		cellImage.color = color;
	}
}
