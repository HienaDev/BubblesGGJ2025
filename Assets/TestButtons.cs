using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestButtons : MonoBehaviour
{

    [SerializeField] private InputActionAsset actionMap;



    private InputAction redBuzz1;
    public bool redBuzz1Pressed = false;
    private InputAction blue1;
    public bool blue1Pressed = false;
    private InputAction orange1;
    public bool orange1Pressed = false;
    private InputAction green1;
    public bool green1Pressed = false;
    private InputAction yellow1;
    public bool yellow1Pressed = false;
    public Vector2 buzz1;

    private InputAction redBuzz2;
    public bool redBuzz2Pressed = false;
    private InputAction blue2;
    public bool blue2Pressed = false;
    private InputAction orange2;
    public bool orange2Pressed = false;
    private InputAction green2;
    public bool green2Pressed = false;
    private InputAction yellow2;
    public bool yellow2Pressed = false;
    public Vector2 buzz2;

    private InputAction redBuzz3;
    public bool redBuzz3Pressed = false;
    private InputAction blue3;
    public bool blue3Pressed = false;
    private InputAction orange3;
    public bool orange3Pressed = false;
    private InputAction green3;
    public bool green3Pressed = false;
    private InputAction yellow3;
    public bool yellow3Pressed = false;
    public Vector2 buzz3;

    private InputAction redBuzz4;
    public bool redBuzz4Pressed = false;
    private InputAction blue4;
    public bool blue4Pressed = false;
    private InputAction orange4;
    public bool orange4Pressed = false;
    private InputAction green4;
    public bool green4Pressed = false;
    private InputAction yellow4;
    public bool yellow4Pressed = false;
    public Vector2 buzz4;

    private InputAction controllerJoyStick;
    public Vector2 joystickValues;
    private InputAction controllerSouth;
    public bool controllerSouthPressed = false;

    public Vector2 keyboard1;
    public bool keyboard1Pressed = false;   
    public Vector2 keyboard2;
    public bool keyboard2Pressed = false;
    public Vector2 keyboard3;
    public bool keyboard3Pressed = false;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buzz1 = Vector2.zero;
        buzz2 = Vector2.zero;
        buzz3 = Vector2.zero;
        buzz4 = Vector2.zero;

        joystickValues = Vector2.zero;

        keyboard1 = Vector2.zero;
        keyboard2 = Vector2.zero;
        keyboard3 = Vector2.zero;

        redBuzz1 = actionMap.FindAction("Player1 Buzz Press");
        redBuzz1.performed += RedBuzz1Press;
        redBuzz1.canceled += RedBuzz1Release;

        blue1 = actionMap.FindAction("Player1 Blue");
        blue1.performed += Blue1Press;
        blue1.canceled += Blue1Release;

        orange1 = actionMap.FindAction("Player1 Orange");
        orange1.performed += Orange1Press;
        orange1.canceled += Orange1Release;

        green1 = actionMap.FindAction("Player1 Green");
        green1.performed += Green1Press;
        green1.canceled += Green1Release;

        yellow1 = actionMap.FindAction("Player1 Yellow");
        yellow1.performed += Yellow1Press;
        yellow1.canceled += Yellow1Release;

        redBuzz2 = actionMap.FindAction("Player2 Buzz Press");
        redBuzz2.performed += RedBuzz2Press;
        redBuzz2.canceled += RedBuzz2Release;

        blue2 = actionMap.FindAction("Player2 Blue");
        blue2.performed += Blue2Press;
        blue2.canceled += Blue2Release;

        orange2 = actionMap.FindAction("Player2 Orange");
        orange2.performed += Orange2Press;
        orange2.canceled += Orange2Release;

        green2 = actionMap.FindAction("Player2 Green");
        green2.performed += Green2Press;
        green2.canceled += Green2Release;

        yellow2 = actionMap.FindAction("Player2 Yellow");
        yellow2.performed += Yellow2Press;
        yellow2.canceled += Yellow2Release;


        redBuzz3 = actionMap.FindAction("Player3 Buzz Press");
        redBuzz3.performed += RedBuzz3Press;
        redBuzz3.canceled += RedBuzz3Release;

        blue3 = actionMap.FindAction("Player3 Blue");
        blue3.performed += Blue3Press;
        blue3.canceled += Blue3Release;

        orange3 = actionMap.FindAction("Player3 Orange");
        orange3.performed += Orange3Press;
        orange3.canceled += Orange3Release;

        green3 = actionMap.FindAction("Player3 Green");
        green3.performed += Green3Press;
        green3.canceled += Green3Release;

        yellow3 = actionMap.FindAction("Player3 Yellow");
        yellow3.performed += Yellow3Press;
        yellow3.canceled += Yellow3Release;

        redBuzz4 = actionMap.FindAction("Player4 Buzz Press");
        redBuzz4.performed += RedBuzz4Press;
        redBuzz4.canceled += RedBuzz4Release;

        blue4 = actionMap.FindAction("Player4 Blue");
        blue4.performed += Blue4Press;
        blue4.canceled += Blue4Release;

        orange4 = actionMap.FindAction("Player4 Orange");
        orange4.performed += Orange4Press;
        orange4.canceled += Orange4Release;

        green4 = actionMap.FindAction("Player4 Green");
        green4.performed += Green4Press;
        green4.canceled += Green4Release;

        yellow4 = actionMap.FindAction("Player4 Yellow");
        yellow4.performed += Yellow4Press;
        yellow4.canceled += Yellow4Release;

        controllerSouth = actionMap.FindAction("ControllerSouth");
        controllerSouth.performed += ControllerSouthPress;
        controllerSouth.canceled += ControllerSouthRelease;

        controllerJoyStick = actionMap.FindAction("ControllerLeftJoyStick"); // Replace "Move" with your action name

        controllerJoyStick.Enable();
    }


    private void Update()
    {
        keyboard1Pressed = false;
        keyboard2Pressed = false;
        keyboard3Pressed = false;

        buzz1 = Vector2.zero;
        buzz2 = Vector2.zero;
        buzz3 = Vector2.zero;
        buzz4 = Vector2.zero;

        joystickValues = Vector2.zero;

        keyboard1 = Vector2.zero;
        keyboard2 = Vector2.zero;
        keyboard3 = Vector2.zero;

        // Read the joystick input as a Vector2
        joystickValues = controllerJoyStick.ReadValue<Vector2>();

        if(Input.GetKey(KeyCode.W))
        {
            keyboard1.y += 1;
            keyboard1Pressed = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            keyboard1.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            keyboard1.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            keyboard1.x -= 1;
        }

        if (Input.GetKey(KeyCode.I))
        {
            keyboard2.y += 1;
            keyboard2Pressed = true;
        }
        if (Input.GetKey(KeyCode.K))
        {
            keyboard2.y -= 1;
        }
        if (Input.GetKey(KeyCode.L))
        {
            keyboard2.x += 1;
        }
        if (Input.GetKey(KeyCode.J))
        {
            keyboard2.x -= 1;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            keyboard3.y += 1;
            keyboard3Pressed = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            keyboard3.y -= 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            keyboard3.x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            keyboard3.x -= 1;
        }

        if (blue1Pressed)
        {
            buzz1.y += 1;
        }
        if (orange1Pressed)
        {
            buzz1.y -= 1;
        }
        if (green1Pressed)
        {
            buzz1.x += 1;
        }
        if (yellow1Pressed)
        {
            buzz1.x -= 1;
        }

        if (blue2Pressed)
        {
            buzz2.y += 1;
        }
        if (orange2Pressed)
        {
            buzz2.y -= 1;
        }
        if (green2Pressed)
        {
            buzz2.x += 1;
        }
        if (yellow2Pressed)
        {
            buzz2.x -= 1;
        }

        if (blue3Pressed)
        {
            buzz3.y += 1;
        }
        if (orange3Pressed)
        {
            buzz3.y -= 1;
        }
        if (green3Pressed)
        {
            buzz3.x += 1;
        }
        if (yellow3Pressed)
        {
            buzz3.x -= 1;
        }

        if (blue4Pressed)
        {
            buzz4.y += 1;
        }
        if (orange4Pressed)
        {
            buzz4.y -= 1;
        }
        if (green4Pressed)
        {
            buzz4.x += 1;
        }
        if (yellow4Pressed)
        {
            buzz4.x -= 1;
        }

    }

    private void ControllerSouthPress(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Controller South press");
            controllerSouthPressed = true;
        }

    }

    private void ControllerSouthRelease(InputAction.CallbackContext context)
    {
        Debug.Log("Controller South release");
        controllerSouthPressed = false;
    }

    private void RedBuzz1Press(InputAction.CallbackContext context)
    {
        if(context.control.IsPressed())
        {
            Debug.Log("Red buzz 1 press");
            redBuzz1Pressed = true;
        }
            
    }

    private void RedBuzz1Release(InputAction.CallbackContext context)
    {
        Debug.Log("Red buzz 1 release");
        redBuzz1Pressed = false;
    }

    private void Blue1Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Blue 1 press");
            blue1Pressed = true;
        }
            
    }

    private void Blue1Release(InputAction.CallbackContext context)
    {
        Debug.Log("Blue 1 release");
        blue1Pressed = false;
    }

    private void Orange1Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Orange 1 press");
            orange1Pressed = true;
        }
            
    }

    private void Orange1Release(InputAction.CallbackContext context)
    {
        Debug.Log("Orange 1 release");
        orange1Pressed = false;

    }

    private void Green1Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Green 1 press");
            green1Pressed = true;
        }
            
    }

    private void Green1Release(InputAction.CallbackContext context)
    {
        Debug.Log("Green 1 release");
        green1Pressed = false;
    }

    private void Yellow1Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Yellow 1 press");
            yellow1Pressed = true;
        }
    }

    private void Yellow1Release(InputAction.CallbackContext context)
    {
        Debug.Log("Yellow 1 release");
        yellow1Pressed = false;
    }

    private void RedBuzz2Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Red buzz 2 press");
            redBuzz2Pressed = true;
        }

    }

    private void RedBuzz2Release(InputAction.CallbackContext context)
    {
        Debug.Log("Red buzz 2 release");
        redBuzz2Pressed = false;
    }

    private void Blue2Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Blue 2 press");
            blue2Pressed = true;
        }

    }

    private void Blue2Release(InputAction.CallbackContext context)
    {
        Debug.Log("Blue 2 release");
        blue2Pressed = false;
    }

    private void Orange2Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Orange 2 press");
            orange2Pressed = true;
        }

    }

    private void Orange2Release(InputAction.CallbackContext context)
    {
        Debug.Log("Orange 2 release");
        orange2Pressed = false;

    }

    private void Green2Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Green 2 press");
            green2Pressed = true;
        }

    }

    private void Green2Release(InputAction.CallbackContext context)
    {
        Debug.Log("Green 2 release");
        green2Pressed = false;
    }

    private void Yellow2Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Yellow 2 press");
            yellow2Pressed = true;
        }
    }

    private void Yellow2Release(InputAction.CallbackContext context)
    {
        Debug.Log("Yellow 2 release");
        yellow2Pressed = false;
    }

    private void RedBuzz3Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Red buzz 3 press");
            redBuzz3Pressed = true;
        }

    }

    private void RedBuzz3Release(InputAction.CallbackContext context)
    {
        Debug.Log("Red buzz 3 release");
        redBuzz3Pressed = false;
    }

    private void Blue3Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Blue 3 press");
            blue3Pressed = true;
        }

    }

    private void Blue3Release(InputAction.CallbackContext context)
    {
        Debug.Log("Blue 3 release");
        blue3Pressed = false;
    }

    private void Orange3Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Orange 3 press");
            orange3Pressed = true;
        }

    }

    private void Orange3Release(InputAction.CallbackContext context)
    {
        Debug.Log("Orange 3 release");
        orange3Pressed = false;

    }

    private void Green3Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Green 3 press");
            green3Pressed = true;
        }

    }

    private void Green3Release(InputAction.CallbackContext context)
    {
        Debug.Log("Green 3 release");
        green3Pressed = false;
    }

    private void Yellow3Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Yellow 3 press");
            yellow3Pressed = true;
        }
    }

    private void Yellow3Release(InputAction.CallbackContext context)
    {
        Debug.Log("Yellow 3 release");
        yellow3Pressed = false;
    }

    private void RedBuzz4Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Red buzz 4 press");
            redBuzz4Pressed = true;
        }

    }

    private void RedBuzz4Release(InputAction.CallbackContext context)
    {
        Debug.Log("Red buzz 4 release");
        redBuzz4Pressed = false;
    }

    private void Blue4Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Blue 4 press");
            blue4Pressed = true;
        }

    }

    private void Blue4Release(InputAction.CallbackContext context)
    {
        Debug.Log("Blue 4 release");
        blue4Pressed = false;
    }

    private void Orange4Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Orange 4 press");
            orange4Pressed = true;
        }

    }

    private void Orange4Release(InputAction.CallbackContext context)
    {
        Debug.Log("Orange 4 release");
        orange4Pressed = false;

    }

    private void Green4Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Green 4 press");
            green4Pressed = true;
        }

    }

    private void Green4Release(InputAction.CallbackContext context)
    {
        Debug.Log("Green 4 release");
        green4Pressed = false;
    }

    private void Yellow4Press(InputAction.CallbackContext context)
    {
        if (context.control.IsPressed())
        {
            Debug.Log("Yellow 4 press");
            yellow4Pressed = true;
        }
    }

    private void Yellow4Release(InputAction.CallbackContext context)
    {
        Debug.Log("Yellow 4 release");
        yellow4Pressed = false;
    }
}
