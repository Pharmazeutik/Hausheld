var texture : Texture2D;
var change : boolean = true;

 
function OnTriggerEnter (info : Collider) {
              GetComponent.<Renderer>().material.mainTexture = texture;
                     
}