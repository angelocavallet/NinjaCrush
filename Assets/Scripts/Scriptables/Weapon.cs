using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string namePrefab;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwCooldown;

    [Header("Throwable")]
    [SerializeField] public Throwable throwable;

    [Header("TAG Info")]
    [SerializeField] private const string TAG_TARGET = "Enemy";
    [SerializeField] private const string TAG_SELF_THROWER = "Player";
    [SerializeField] private const string TAG_THROWABLE = "Bullet";
    [SerializeField] private const string TAG_GROUND = "Ground";

    [SerializeField] private int _maxPhysicsFrameIterations = 30;

    public Action<Throwable> onThrowed { private get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedTarget { private get; set; }
    public Action<Collider2D, Throwable, Vector2, float> onHitedSomething { private get; set; }
    public Transform transform { private get; set; }
    public LineRenderer lineRenderer { private get; set; }

    private Vector2 aimDirection;
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    private float nextShoot = 0f;
    private int instanceNumber = 1;

    public Weapon Clone(Transform aimPositionTransform)
    {
        GameObject newWeaponGameObject = Instantiate(weaponPrefab, aimPositionTransform.position, aimPositionTransform.rotation, aimPositionTransform);
        newWeaponGameObject.name = $"{namePrefab} ({++instanceNumber})";

        Weapon newWeapon = Instantiate(this);
        newWeapon.transform = aimPositionTransform;
        newWeapon.lineRenderer = newWeaponGameObject.GetComponent<LineRenderer>();

        //createdWeapon.CreatePhysicsScene();
        return newWeapon;
    }

    //@todo for mobile here need to change this to receive angle and decide angle in PlayerInput
    public void SetAim(Vector2 pointerPosition)
    {
        aimDirection = (pointerPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 90f));
        //SimulateTrajectory();
    }

    public void Throw()
    {
        if (Time.time < nextShoot) return;

        nextShoot = Time.time + throwCooldown;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Transform newTransform = transform;
        newTransform.rotation = rotation;
        Throwable newThrowable = throwable.InstantiateCloneAtTransform(transform);

        newThrowable.selfTag = TAG_THROWABLE;
        newThrowable.targetTag = TAG_TARGET;
        newThrowable.selfThrowerTag = TAG_SELF_THROWER;
        newThrowable.onThrowed = onThrowed;

        newThrowable.onHitedTarget = onHitedTarget;
        newThrowable.onHitedSomething = onHitedSomething;

        newThrowable.Throw(aimDirection * throwForce);
        newThrowable.PlayThrowAudioClip();

        instanceNumber++;
    }

    private void CreatePhysicsScene()
    {
        GameObject ghostGrid = Instantiate(GameObject.FindGameObjectsWithTag("Grid").First());

        foreach (Transform t in ghostGrid.transform)
        {
            if (t.gameObject.CompareTag(TAG_GROUND))
            {
                t.gameObject.GetComponent<Renderer>().enabled = false;

            }
            else
            {
                Destroy(t.gameObject);
            }
        }

        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        SceneManager.MoveGameObjectToScene(ghostGrid, _simulationScene);
    }

    public void SimulateTrajectory()
    {
        GameObject newThrowablePrefab = Instantiate(throwable.throwablePrefab, transform.position, transform.rotation);
        newThrowablePrefab.name = $"SIMULATE {namePrefab} ({instanceNumber})";
        Throwable newThrowable = newThrowablePrefab.GetComponent<ThrowableController>().GetThrowable();
        newThrowable.isGhost = true;
        SceneManager.MoveGameObjectToScene(newThrowablePrefab, _simulationScene);

        newThrowable.Throw(aimDirection * throwForce);

        lineRenderer.positionCount = _maxPhysicsFrameIterations;

        for (int i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            lineRenderer.SetPosition(i, newThrowablePrefab.transform.position);
        }

        Destroy(newThrowablePrefab);
    }
}