using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (Renderer))]
[RequireComponent (typeof (Animation))]
public class ModelObject : MonoBehaviour 
{
	/// <summary>
	/// The model prewiew sprite. You must manually set it in Inspector.
	/// </summary>
	public Sprite modelPrewiewSprite; 

	/// <summary>
	/// The name of 3d-model.
	/// </summary>
	public string modelName;

	//Default 3d-model animation
	private Animation anim;
	private Renderer modelRenderer;

	public void AnimateModel (bool animate)
	{
		if (animate)
			anim.Play ();
		else
		{
			anim.Stop ();
			anim.clip.SampleAnimation (anim.gameObject, 0f);
		}
	}

	public void SetModelActive (bool isActive)
	{
		modelRenderer.enabled = isActive;

		if (GetComponent <Collider> ())
			GetComponent <Collider> ().enabled = isActive;	
	}

	void Start () 
	{
		if (anim == null)
			anim = GetComponent <Animation> ();
		
		anim.playAutomatically = false;
		modelRenderer = GetComponent <Renderer> ();
		SetModelActive (false);
	}
}
