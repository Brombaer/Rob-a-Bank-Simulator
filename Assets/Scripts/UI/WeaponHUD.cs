using UnityEngine;
using UnityEngine.UI;

public class WeaponHUD : MonoBehaviour
{
	[SerializeField] private PlayerCharacter _playerRef;

	[SerializeField] private Text _weaponName;
	[SerializeField] private Text _currentAmmo;
	[SerializeField] private Text _maxAmmo;

	private int _maxAmount;


	private void Start()
	{
		_maxAmmo.text = _playerRef.CurrentWeapon.MagazineSize.ToString();
	}

	private void Update()
	{
		UpdateWeaponInfo();
	}

	private void UpdateWeaponInfo()
	{
		_weaponName.text = _playerRef.CurrentWeapon.Name;

		_currentAmmo.text = _playerRef.CurrentWeapon.CurrentAmmo.ToString();
	}
}
