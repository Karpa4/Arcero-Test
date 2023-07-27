using System.Collections;
using UnityEngine;

public class BallisticAttack : InitAttack 
{
    [SerializeField] private FindTargets findTargets;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private float angleInDegrees;
    [SerializeField] private BulletTrigger bullet;
    [SerializeField] private float reload;
    private Transform targetTransform;
    private float g = Physics.gravity.y;

    private void Start()
    {
        findTargets.NewTargetFinded += StartFire;
    }

    private void StartFire(Collider[] targets, int count)
    {
        targetTransform = targets[0].transform;
        StartCoroutine(ShotWithDelay());
    }

    private IEnumerator ShotWithDelay()
    {
        while (true)
        {
            Shot();
            yield return new WaitForSeconds(reload);
        }
    }

    public void Shot() 
    {
        Vector3 fromTo = targetTransform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);
        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;
        float AngleInRadians = angleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        BulletTrigger newBullet = Instantiate(bullet, spawnTransform.position, Quaternion.identity);
        newBullet.Init(damage);
        newBullet.GetComponent<Rigidbody>().velocity = spawnTransform.forward * v;
    }

}
