using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerCont : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private int _health=10;
    [SerializeField] private ProjectTileCont projecttilePref;
    [SerializeField] private Transform cameraPivot;
   private List<ProjectTileCont> projectile = new List<ProjectTileCont>();
    private int projectileSize = 20;
    private int projectileIndex = 0;
   public bool isAlive=>_health>0;
    public int Hp => _health;
    private float cordz;
    private float cordx;
    [SerializeField] private Transform _ShutPoint;
    private float _shutRange;
    private Vector3 _moveVector;
    [SerializeField] float cameraRotationSpeed = 3f;
    [SerializeField] float cameraReturnSpeed = 3f;
    private float cameraYawSpeed;
    private float cameraPitchSpeed;
    private float cameraDefalutRotation;
    private float cameraDefalutRotation2;
   

    [SerializeField] private float minCameraPitch = -70f;
    [SerializeField] private float maxCameraPitch = 70f;


    private float lookDelta;




   // Start is called once before the first execution of Update after the MonoBehaviour is created
   void Start()
    {
        
    }
    public void TakeDamage(int damage)
    {
        if (_health < 0)
        {
            return;
        }
        _health -= damage;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move();
        RotateTank();
        RotateShutPint();
        RotateCamera();
    }
    private void OnEnable()
    {
        InputManager.OnSpacePressed += OnSpacePress;
        InputManager.OnFPressed += OnFPress;
        InputManager.OnMovementPressed += ReadMoveInput;
        InputManager.OnLookPressed += ReadLookInput;
        InputManager.OnLeftMouseButtonPressed += OnLeftMouseButtonPress;
        for(int i=0; i<projectileSize; i++)
        {
            
            var projectille = Instantiate(projecttilePref, _ShutPoint.position, Quaternion.identity);
            projectille.gameObject.SetActive(false);
            projectile.Add(projectille);
            
        }
       
    }
    private void OnDisable()
    {
        InputManager.OnSpacePressed -= OnSpacePress;
        InputManager.OnFPressed -= OnFPress;
        InputManager.OnMovementPressed -= ReadMoveInput;
        InputManager.OnLeftMouseButtonPressed -= OnLeftMouseButtonPress;
        InputManager.OnLookPressed -= ReadLookInput;
    }
     private void OnSpacePress()
    {
        Debug.Log("Space");
    }

    private void OnFPress()
    {
        Debug.Log("F");
    }
    private void OnLeftMouseButtonPress()
    {
       LaunchProjectile();
    }
    private void ReadMoveInput(Vector2 inputVector)
    {
        cordx = inputVector.x;
        cordz = inputVector.y;
       
    }
    private void ReadLookInput(Vector2 inputVector)
    {
        lookDelta = inputVector.x;
        cameraPitchSpeed = inputVector.x;
        cameraYawSpeed = inputVector.y;

    }
    private void Move()
    {
        _moveVector = transform.forward * cordz+ transform.right*cordx;
        if (_moveVector.magnitude > 1f)
        {
            _moveVector.Normalize();
        }
        _moveVector *= _speed * Time.deltaTime;
        _rb.MovePosition(_moveVector + _rb.position);
       
        
    }
    private void RotateTank()
    {
        //if (cordx == 0)
        //{
        //    _isRotating = false;
        //    return;
        //}
        //_isRotating = true;
        transform.Rotate(Vector3.up, lookDelta * _rotationSpeed * Time.deltaTime);
    }
    private void RotateShutPint()
    {
        cameraPivot.Rotate(Vector3.up * lookDelta * _rotationSpeed * Time.deltaTime);
    }
    private void RotateCamera()
    {
        cameraDefalutRotation +=  cameraPitchSpeed* cameraRotationSpeed * Time.deltaTime;
        cameraDefalutRotation = Mathf.Clamp(cameraDefalutRotation, minCameraPitch, maxCameraPitch);

        cameraDefalutRotation2 += cameraYawSpeed * cameraRotationSpeed * Time.deltaTime;
        cameraDefalutRotation2 = Mathf.Lerp(cameraDefalutRotation2, 0f,cameraReturnSpeed*Time.deltaTime);
        cameraPivot.localRotation = Quaternion.Euler(cameraDefalutRotation, cameraDefalutRotation2, 0f);
    }

    private void LaunchProjectile()
    {
        projectile[projectileIndex].transform.position = _ShutPoint.position;
        projectile[projectileIndex].gameObject.SetActive(true);
        projectile[projectileIndex].Initialized(1, transform.forward);
        projectileIndex = (projectileIndex + 1) % projectile.Count;
    }
}
