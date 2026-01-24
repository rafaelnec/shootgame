using UnityEngine;

public class Weapon: MonoBehaviour
{
    public float damage;

    public GameObject bulletPrefab;
    private string _bulletTag;
    
    [SerializeField] private AudioClip shootSound;

    public void SetBulletTag(string bulletTag)
    {
        _bulletTag = bulletTag;
    }

    public void setBulletColour(Color bulletColour)
    {
        bulletPrefab.GetComponent<SpriteRenderer>().color = bulletColour;
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
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
        GameObject bulletObj = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletObj.tag = GetBulletTag();
    }

    public void Shoot(Transform shootTarget)
    {       
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
        Vector2 directionToTarget = (shootTarget.position - transform.position).normalized;
        GameObject bulletObj =  GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletObj.tag = GetBulletTag();
        Bullet bullectComponent = bulletObj.GetComponent<Bullet>();
        bullectComponent.SetBulletDamage(damage);
        bullectComponent.SetDirection(directionToTarget);
    }

    public void Shoot(Transform shootTarget, float bulletSpeed)
    {       
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
        Vector2 directionToTarget = (shootTarget.position - transform.position).normalized;
        GameObject bulletObj =  GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletObj.tag = GetBulletTag();
        Bullet bullectComponent = bulletObj.GetComponent<Bullet>();
        bullectComponent.SetBulletDamage(damage);
        bullectComponent.SetDirection(directionToTarget);
        bullectComponent.SetBulletSpeed(bulletSpeed);
    }
}
