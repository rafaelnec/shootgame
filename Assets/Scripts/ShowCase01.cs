using UnityEngine;

public class ShowCase01 : MonoBehaviour
{
    public Player playerPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        // player.PickupWeapon(new Weapon("MachineGun", 50));

        // Enemy enemy1 = new ShooterEnemy();
        // Enemy enemy2 = new MeleeEnemy();

        // Weapon weapon1 = new Weapon("Golf Club", 9999);
        // Weapon weapon2 = new Weapon("Gun Club", 9999 + 1);
    }

}
