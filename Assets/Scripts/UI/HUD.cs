using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] private GameObject _playerPrefab;

	[Header("Wallet")]
	[SerializeField] private Text _walletAmount;

	[Header("Weapon")]
	[SerializeField] private Text _weaponName;
	[SerializeField] private Text _currentAmmo;
	[SerializeField] private Text _maxAmmo;
	[SerializeField] private Image _weaponIcon;

	private PlayerCharacter _playerCharacterRef;
	private PlayerInteractController _interactController;


	private void Awake()
	{
		_playerCharacterRef = _playerPrefab.GetComponent<PlayerCharacter>();
		_interactController = _playerPrefab.GetComponent<PlayerInteractController>();
	}

	private void Update()
	{
		UpdateWeaponInfo();
		UpdateWalletInfo();
	}

	private void UpdateWeaponInfo()
	{
		_maxAmmo.text = _playerCharacterRef.CurrentWeapon.MagazineSize.ToString();

		_weaponName.text = _playerCharacterRef.CurrentWeapon.Name;

		_currentAmmo.text = _playerCharacterRef.CurrentWeapon.CurrentAmmo.ToString();

		_weaponIcon.sprite = _playerCharacterRef.CurrentWeapon.WeaponIcon;
	}

	private void UpdateWalletInfo()
	{
		_walletAmount.text = _interactController.WalletAmount.ToString();
	}
}
