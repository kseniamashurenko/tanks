using UnityEngine;

public class PlayerCont : MonoBehaviour
{

    [SerializeField] private InputManager inputManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        InputManager.OnSpacePressed += OnSpacePress;
        InputManager.OnFPressed += OnFPress;
        //InputManager.OnMovementPressed += OnMovePress;
        InputManager.OnLeftMouseButtonPressed += OnLeftMouseButtonPress;
    }
    private void OnDisable()
    {
        InputManager.OnSpacePressed -= OnSpacePress;
        InputManager.OnFPressed -= OnFPress;
        //InputManager.OnMovementPressed -= OnMovePress;
        InputManager.OnLeftMouseButtonPressed -= OnLeftMouseButtonPress;
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
        Debug.Log("LeftButton");
    }
    private void OnMovePress()
    {

    }
}
