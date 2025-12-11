using UnityEngine;

public class Weapon: MonoBehaviour
{
    public float damage;

    public GameObject bulletPrefab;
    private string _bulletTag;

    public void SetBulletTag(string bulletTag)
    {
        _bulletTag = bulletTag;
    }

    public string GetBulletTag()
    {
        if (_bulletTag == null)
        {
            return "Untagged";
        }
        return _bulletTag;
    }

    public void Shoot()
    {       
        GameObject bulletObj = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletObj.tag = GetBulletTag();
    }

    public void Shoot(Transform shootTarget)
    {       
        Vector2 directionToTarget = (shootTarget.position - transform.position).normalized;
        GameObject bulletObj =  GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletObj.tag = GetBulletTag();
        Bullet bullectComponent = bulletObj.GetComponent<Bullet>();
        bullectComponent.SetBulletDamage(damage);
        bullectComponent.SetDirection(directionToTarget);
    }

    public void Shoot(Transform shootTarget, float bulletSpeed)
    {       
        Vector2 directionToTarget = (shootTarget.position - transform.position).normalized;
        GameObject bulletObj =  GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletObj.tag = GetBulletTag();
        Bullet bullectComponent = bulletObj.GetComponent<Bullet>();
        bullectComponent.SetBulletDamage(damage);
        bullectComponent.SetDirection(directionToTarget);
        bullectComponent.SetBulletSpeed(bulletSpeed);
    }
}
