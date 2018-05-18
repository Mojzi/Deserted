using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpriteBasedCollision : MonoBehaviour {

	// Jakiś kawałek kodu, który znalazłem, który w locie generuje kolizję na podstawie danego sprite'a.
	// Większość rzeczy ma inaczej zrobioną kolizję (jest niby pod kątem, więc collider jest tak jakby "w nogach"),
	// ale to jest dobre do animacji ataku bronią białą

	public string spriteSheetName;

	 public bool iStrigger ;
        //  public PhysicsMaterial2D _material ;
     
         private SpriteRenderer spriteRenderer;
         private List<Sprite> spritesList;
         private Dictionary<int, PolygonCollider2D> spriteColliders;
         private bool _processing ;
     
         private int _frame ;
         public int Frame {
                 get { return _frame; }
                 set {
                         if (value != _frame) {
                                 if (value > -1) {
                                         spriteColliders [_frame].enabled = false;
                                         _frame = value;
                                         spriteColliders [_frame].enabled = true;
                                 } else {
                                         _processing = true;

										spriteSheetName = GetComponent<ReSkinAnimation>().spriteSheetName;
										var subSprites = Resources.LoadAll<Sprite>("Graphics/" + spriteSheetName);

											string spriteName = spriteRenderer.sprite.name;
											var newSprite = System.Array.Find(subSprites, item => item.name == spriteName);
											if(newSprite) {
												spriteRenderer.sprite = newSprite;
											}

                                         StartCoroutine (AddSpriteCollider (spriteRenderer.sprite));
                                 }
                         }
                 }
         }
 
         private IEnumerator AddSpriteCollider (Sprite sprite)
         {
                 spritesList.Add (sprite);
                 int index = spritesList.IndexOf (sprite);
                 PolygonCollider2D spriteCollider = gameObject.AddComponent<PolygonCollider2D> ();
                 spriteCollider.isTrigger = iStrigger;
                //  spriteCollider.sharedMaterial = _material;
                 spriteColliders.Add (index, spriteCollider);
                 yield return new WaitForEndOfFrame ();
                 Frame = index;
                 _processing = false;
         }
     
         private void OnEnable ()
         {
                 spriteColliders [Frame].enabled = true;
         }
     
         private void OnDisable ()
         {
                 spriteColliders [Frame].enabled = false;
         }
 
         private void Awake ()
         {
                 spriteRenderer = this.GetComponent<SpriteRenderer> ();
         
                 spritesList = new List<Sprite> ();
         
                 spriteColliders = new Dictionary<int, PolygonCollider2D> ();
         
                 Frame = spritesList.IndexOf (spriteRenderer.sprite);
         }
     
         private void LateUpdate ()
         {

                 if (!_processing)
                         Frame = spritesList.IndexOf (spriteRenderer.sprite);
         }
}
