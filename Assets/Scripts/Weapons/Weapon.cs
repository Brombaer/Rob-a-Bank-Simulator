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




	[SerializeField] private bool weaponSway;
	[SerializeField] private float swayAmount = 0.02f;
	[SerializeField] private float maxSwayAmount = 0.06f;
	[SerializeField] private float swaySmoothValue = 4.0f;

	
	private Vector3 initialSwayPosition;
	private float _lastBulletTime;
	private bool _isFiring;




	private void Start()
	{
		initialSwayPosition = transform.localPosition;
	}
	
	protected virtual void Update()
	{
		if (_isFiring && !_isSingleFire && CanFire())
			Fire();
	}
	
	private void LateUpdate()
	{
		WeaponSway();
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

	private void WeaponSway()
	{
		if (weaponSway) 
		{
			float movementX = Mathf.Clamp (-Input.GetAxis ("Mouse X") * swayAmount, -maxSwayAmount, maxSwayAmount);
			float movementY = Mathf.Clamp (-Input.GetAxis ("Mouse Y") * swayAmount, -maxSwayAmount, maxSwayAmount);
			
			Vector3 finalSwayPosition = new Vector3(movementX, movementY, 0);
			
			transform.localPosition = Vector3.Lerp (transform.localPosition, finalSwayPosition + initialSwayPosition, Time.deltaTime * swaySmoothValue);
		}
	}
}
