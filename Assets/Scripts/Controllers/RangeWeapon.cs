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

    private Scene simulationScene;
    private PhysicsScene physicsScene;
    private float throwForce;
    private float throwCooldown;

    private Throwable throwable;
    private float offset;

    public override void Awake()
    {
        base.Awake();

        throwCooldown = weaponData.attackCooldownSeconds;

        throwable = rangeWeaponData.throwable;
        offset = rangeWeaponData.offset;
        throwForce = rangeWeaponData.throwForce;

        tagSelfThrower = rangeWeaponData.tagSelfThrower;
        tagThrowable = rangeWeaponData.tagThrowable;
        tagGround = rangeWeaponData.tagGround;

        maxPhysicsFrameIterations = rangeWeaponData.maxPhysicsFrameIterations;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void SetAim(Vector2 aimPosition)
    {
        base.SetAim(aimPosition);
        //@todo do trajectory
        //SimulateTrajectory();
    }

    public override void Attack()
    {
        if (Time.time >= base.lastAttackTime + base.attackCooldownSeconds)
        {
            base.Attack();
            throwNewThrowable();
        }
    }

    private void throwNewThrowable()
    {
        throwable.transform.position = new Vector2(transform.position.x, transform.position.y) + aimDirection * offset;
        throwable.transform.rotation = transform.rotation;

        Throwable newThrowable = Instantiate(throwable);

        newThrowable.targetTag = tagTarget;
        newThrowable.selfThrowerTag = tagSelfThrower;
        newThrowable.onThrowed = onThrowed;
        newThrowable.onHitedTarget = onHitedTarget;
        newThrowable.onHitedSomething = onHitedSomething;

        newThrowable.Throw(base.aimDirection * throwForce);
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

        newThrowable.Throw(base.aimDirection * throwForce);

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
