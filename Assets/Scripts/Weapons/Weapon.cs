using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Weapon")]
	[SerializeField] private string _name;
	[SerializeField] private int _currentAmmo;
	[SerializeField] private int _magazineSize;
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private bool _isSingleFire;
	[SerializeField] private float _fireRate;
	[SerializeField] private Transform _bulletSpawnPoint;
	[SerializeField] private float _bulletForce;

	[SerializeField] private AudioClip _debugSound;

	private float _lastBulletTime;
	private bool _isFiring;




	
	protected virtual void Update()
	{
		if (Input.GetMouseButtonDown(0))
			BeginFire();

		if (Input.GetMouseButtonUp(0))
			StopFire();


		if (_isFiring && !_isSingleFire && CanFire())
			Fire();

	}
	
	public void BeginFire()
	{
		_isFiring = true;
		
		if(CanFire() && _isSingleFire)
		{
			Fire();
		}
	}

	public void StopFire()
	{
		_isFiring = false;
	}

	protected void Fire()
	{
		AudioSource.PlayClipAtPoint(_debugSound, transform.position);
		
		_lastBulletTime = Time.time;
		

		// Spawn Bullet
		GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _bulletForce;
	}

	private bool CanFire()
	{
		return Time.time - _lastBulletTime > 60 / _fireRate;
	}
}
