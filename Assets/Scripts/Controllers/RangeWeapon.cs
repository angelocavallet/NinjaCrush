using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RangeWeapon : Weapon
{
    [SerializeField]
    private RangeWeaponScriptableObject rangeWeaponData;

    private string tagSelfThrower;
    private string tagThrowable;
    private string tagGround;
    private int maxPhysicsFrameIterations = 30;

    private Vector2 aimDirection;
    //@TODO: move to rangeweapon
    private Scene simulationScene;
    private PhysicsScene physicsScene;
    private float throwForce;
    private float throwCooldown;

    private Throwable throwable;

    public void Awake()
    {
        base.Awake();

        throwCooldown = weaponData.attackCooldownSeconds;

        throwable = rangeWeaponData.throwable;
        throwForce = rangeWeaponData.throwForce;

        tagSelfThrower = rangeWeaponData.tagSelfThrower;
        tagThrowable = rangeWeaponData.tagThrowable;
        tagGround = rangeWeaponData.tagGround;

        maxPhysicsFrameIterations = rangeWeaponData.maxPhysicsFrameIterations;
    }

    //@todo for mobile here need to change this to receive angle and decide angle in PlayerInput
    public void SetAim(Vector2 pointerPosition)
    {
        //@TODO: move to rangeweapon ? playerInput ?
        aimDirection = (pointerPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90f));
        //SimulateTrajectory();
    }

    public void Throw()
    {
        StartAttack();
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        throwable.transform.position = transform.position;
        throwable.transform.rotation = rotation;

        Throwable newThrowable = Instantiate(throwable);

        newThrowable.targetTag = tagTarget;
        newThrowable.selfThrowerTag = tagSelfThrower;
        newThrowable.onThrowed = onThrowed;

        newThrowable.onHitedTarget = onHitedTarget;
        newThrowable.onHitedSomething = onHitedSomething;

        newThrowable.Throw(aimDirection * throwForce);
    }

    private void CreatePhysicsScene()
    {
        GameObject ghostGrid = Instantiate(GameObject.FindGameObjectsWithTag("Grid").First());

        foreach (Transform t in ghostGrid.transform)
        {
            if (t.gameObject.CompareTag(tagGround))
            {
                t.gameObject.GetComponent<Renderer>().enabled = false;

            }
            else
            {
                Destroy(t.gameObject);
            }
        }

        simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
        physicsScene = simulationScene.GetPhysicsScene();

        SceneManager.MoveGameObjectToScene(ghostGrid, simulationScene);
    }

    public void SimulateTrajectory()
    {/*
        GameObject newThrowablePrefab = Instantiate(throwable.gameObject, transform.position, transform.rotation);
        newThrowablePrefab.name = $"SIMULATE({instanceNumber})";
        Throwable newThrowable = newThrowablePrefab.GetComponent<Throwable>();
        newThrowable.isGhost = true;
        SceneManager.MoveGameObjectToScene(newThrowablePrefab, simulationScene);

        newThrowable.Throw(aimDirection * throwForce);

        lineRenderer.positionCount = _maxPhysicsFrameIterations;

        for (int i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            physicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, newThrowablePrefab.transform.position);
        }

        Destroy(newThrowablePrefab);
        */
    }
}
