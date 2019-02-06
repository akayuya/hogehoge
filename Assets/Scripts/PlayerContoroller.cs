using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoroller : MonoBehaviour {

	private int _playerHP = 5;

	private const int PLAYER_HP_FULL = 5;
	private const int DESTROY_PLAYER_INERVAL = 1;

	[SerializeField] private SpawnController spawnController;


	public void HitPlayer()
	{
		_playerHP--;
		print("PlayerHP" + _playerHP);

		RecoverPLayerHP();

		if(_playerHP == 0)
		{
			StartCoroutine(DeadPlayer());
		}
	}

	private void RecoverPLayerHP()
    {
        if (_playerHP > 0) return;

        _playerHP = PLAYER_HP_FULL;
        print(_playerHP + "PlayerHP回復");
    }

	private IEnumerator DeadPlayer()
	{
		yield return new WaitForSeconds(DESTROY_PLAYER_INERVAL);

		Destroy(this);
		spawnController.SpawnPlayer();
	}

}
