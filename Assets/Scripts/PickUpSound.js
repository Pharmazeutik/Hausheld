var PickUpSound: AudioClip;

function OnTriggerEnter (info : Collider)
{
		AudioSource.PlayClipAtPoint(PickUpSound, transform.position);

}