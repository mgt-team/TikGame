public class CommonDestroy : Destroyble {
	public override void Destroy()
	{
		gameObject.SetActive(false);
		Destroy(gameObject);
	}
}
