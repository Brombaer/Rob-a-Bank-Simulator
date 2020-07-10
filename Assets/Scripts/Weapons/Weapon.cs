using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Weapon Properties")]
	[SerializeField] private string _name;
	[SerializeField] private bool _isSingleFire;
	[SerializeField] private float _fireRate;

	[Space]
	[Header("Muzzle Settings")]
	[SerializeField] private ParticleSystem _muzzleParticle;
	[SerializeField] private Light _muzzleLight;
	[SerializeField] private Transform _muzzleLocation;
	
	[Space]
	[Header("Reload Settings")]
	[SerializeField] private int _magazineSize = 30;
	[SerializeField] private float _reloadTime;

	[Space]
	[Header("Bullet Settings")]
	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private Transform _bulletSpawnPoint;
	[SerializeField] private float _bulletForce;
	
	[Space][Space]
	[Header("Weapon Sway Settings")]
	[SerializeField] private bool weaponSway;
	[SerializeField] private float swayAmount = 0.02f;
	[SerializeField] private float maxSwayAmount = 0.06f;
	[SerializeField] private float swaySmoothValue = 4.0f;

	[Space][Space]
	[Header("FMOD Events")][FMODUnity.EventRef]
	[SerializeField] private string _shotFEvent;
	[FMODUnity.EventRef]
	[SerializeField] private string _noAmmoFEvent;


	public event System.Action Fired;
	public int CurrentAmmo { get; private set; }

	private Vector3 initialSwayPosition;
	private float _lastBulletTime;
	private float _lastReloadTime;
	private bool _isFiring;
	private float _timer = 1;




	private void Start()
	{
		initialSwayPosition = transform.localPosition;

		CurrentAmmo = _magazineSize;

		_muzzleLight.enabled = false;
	}
	
	protected virtual void Update()
	{
		if (_isFiring && !_isSingleFire && !isReloading() && CanFire())
			Fire();
	}
	
	private void LateUpdate()
	{
		WeaponSway();
	}

	public void BeginFire()
	{
		_isFiring = true;
		
		if(_isSingleFire && !isReloading() && CanFire())
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
		if (CurrentAmmo > 0)
		{
			CurrentAmmo -= 1;

			FMODUnity.RuntimeManager.PlayOneShotAttached(_shotFEvent, gameObject);

			_lastBulletTime = Time.time;

			Fired?.Invoke();

			// Spawn Bullet
			GameObject bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
			bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * _bulletForce;


			_muzzleParticle.Emit(1);
			StartCoroutine(MuzzleFlash());
		}
		else
		{
			FMODUnity.RuntimeManager.PlayOneShotAttached(_noAmmoFEvent, gameObject);
		}
	}

	System.Collections.IEnumerator MuzzleFlash()
	{
		_muzzleLight.enabled = true;

		yield return new WaitForSeconds(0.1f);
		_muzzleLight.enabled = false;
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

	public void Reload()
	{
		CurrentAmmo = _magazineSize;

		_lastReloadTime = Time.time;
	}

	private bool isReloading()
	{
		return Time.time - _lastReloadTime < _reloadTime;
	}
}
