using UnityEngine;
using System.Collections;
using System.IO;
public class ColorSwitch : MonoBehaviour {
	public playerChar player;
	public GameObject SelectedColor;
	public Texture2D[] theTextures;
	public int SkinNumber;
	private Texture2D theTexture;
	public Color32 MainColor;
	public Color32 SecColor;
	// Use this for initialization
	void Start () {
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			//Calls for the generic textures and calls sprite recoloring program
			int i = 0;
			//player = other.gameObject.GetComponent<playerChar>();
			foreach (Texture2D texture in theTextures) {
				Texture2D newTexture = ChangeTexture (texture);
				theTextures [i] = newTexture;
				//byte[] bytes = newTexture.EncodeToPNG ();
			  //  File.WriteAllBytes (Application.dataPath + "/Saved" +i+".png", bytes);
				i++;
			}
			//Switches to prebuilt animation
			Animator animator = player.GetComponent<Animator> ();
			animator.SetInteger ("SkinColor", SkinNumber);

			//player.isTouchingElevator = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.GetType() == typeof(playerChar))
		{

			//player.touchedDoor = null;
			//player.isTouchingElevator = false;
			//player = null;
		}

	}
	//Recolors a texture with the main and secondary colors given in Inspector
	Texture2D ChangeTexture(Texture2D theTexture)
	{
		//Creates new texture to override old one
		Texture2D newTexture = new Texture2D (theTexture.width, theTexture.height);
		newTexture.filterMode = FilterMode.Point;
		newTexture.wrapMode = TextureWrapMode.Clamp;
		int x = 0;
		//Iterating through pixel by pixel
		while (x < newTexture.width) 
		{
			
			int y = 0;
			while (y < newTexture.height)
			{
				//Debug.Log(theTexture.GetPixel(x,y));
				if (theTexture.GetPixel (x, y) == new Color32 (255, 0, 255, 255)) 
				{
					//Inner color
					newTexture.SetPixel (x, y, MainColor);
				} 
				else if (theTexture.GetPixel (x, y) == new Color32 (200, 0, 200, 255)) 
				{
					//Outer Color
					newTexture.SetPixel (x, y,SecColor);
				}
				else
				{
					//Non Changed color
					newTexture.SetPixel(x, y, theTexture.GetPixel(x,y));
				}
				y++;

			}
			x++;
		}
		//Finalize Texture
		newTexture.Apply ();
		//Creates new Sprite with the texture, this is currently useless due to how Animator works
		//SelectedColor.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (newTexture, SelectedColor.GetComponent<SpriteRenderer> ().sprite.rect, new Vector2(.5f,.5f));
		//SelectedColor.GetComponent<SpriteRenderer> ().material.mainTexture = newTexture;
		return newTexture;
	}
	// Update is called once per frame
	void Update () {
	
	}

}
