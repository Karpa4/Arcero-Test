using UnityEngine;

public class BaseAttack : InitAttack
{
    [SerializeField] private TargetsChecker checker;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float reloadTime;

    protected bool isMoving;
    private float currentTime;

    protected virtual void Start()
    {
        currentTime = 0;
        isMoving = false;
    }

    private void Update()
    {
        if (currentTime < reloadTime)
        {
            currentTime += Time.deltaTime;
        }

        if (!isMoving)
        {
            if (checker.mainTarget != null)
            {
                if (currentTime >= reloadTime)
                {
                    Attack(checker.mainTarget.position);
                    currentTime = 0;
                }
            }
        }
    }

    private void Attack(Vector3 targetPos)
    {
        Quaternion quat = Quaternion.LookRotation((targetPos - transform.position).normalized);
        Bullet newBullet = Instantiate<Bullet>(bulletPrefab, transform.position, quat);
        newBullet.Init(damage, bulletSpeed);
    }
}
