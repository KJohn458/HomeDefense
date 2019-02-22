using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject); 
        }
    }
    private static bool TestGamepadConected()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
    private float Move_Horizontal()
    {
        float r = 0.0f;

        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Horizontal");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Horizontal");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("Horizontal");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }
    }
    private float Move_Vertical()
    {
        float r = 0.0f;

        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Vertical");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Vertical");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("Vertical");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }
    }
    public Vector3 Move()
    {
        if (TestGamepadConected())
        {
            return new Vector3(Move_Horizontal(),0.0f, Move_Vertical());

        }else
        {
            float x = 0.0f;
            float y = 0.0f;

            x += Input.GetAxis("Horizontal");
            y += Input.GetAxis("Vertical");

            return new Vector3 (x, 0.0f,y);
        }
    }
    private float Looking_Horizontal()
    {
        float r = 0.0f;
        
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Looking_Horizontal_Joystick_MacOSX");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Looking_Horizontal_Joystick_Windows");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("looking_Horizontal_Joystick_Linux");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }
    }
    private float Looking_Vertical()
    {
        float r = 0.0f;
        
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                r += Input.GetAxis("Looking_Vertical_Joystick_MacOSX");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Windows:

                r += Input.GetAxis("Looking_Vertical_Joystick_Windows");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            case OperatingSystemFamily.Linux:

                r += Input.GetAxis("looking_Vertical_Joystick_Linux");
                return Mathf.Clamp(r, -1.0f, 1.0f);

            default:
                return 0;
        }
    }
    public Vector3 Looking()
    {
        if (TestGamepadConected())
        {
            return new Vector3(Looking_Horizontal(), Looking_Vertical(), 0.0f);
            
        }else
        {
            float lookingHorizontal = Input.GetAxisRaw("Looking_Horizontal_Keyboard");
            float lookingVertical = Input.GetAxisRaw("Looking_Vertical_Keyboard");

            return new Vector3 (lookingHorizontal, lookingVertical, 0.0f);
        }
    }
    public bool Pause()
    {
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                if (Input.GetButtonDown("Pause_Keyboard") || Input.GetButtonDown("Pause_MacOSX"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Windows:

                if (Input.GetButtonDown("Pause_Keyboard") || Input.GetButtonDown("Pause_Windows"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Linux:

                if (Input.GetButtonDown("Pause_Keyboard") || Input.GetButtonDown("Pause_Linux"))
                {
                    return true;
                }
                else return false;

            default:
                return false;
        }
    }
    public bool Submit()
    {
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                if (Input.GetKeyDown("Submit_Keyboard") || Input.GetButtonDown("Submit_MacOSX"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Windows:

                if (Input.GetKeyDown("Submit_Keyboard") || Input.GetButtonDown("Submit_Windows"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Linux:

                if (Input.GetKeyDown("Submit_Keyboard") || Input.GetButtonDown("Submit_Linux"))
                {
                    return true;
                }
                else return false;

            default:
                return false;
        }
    }
    public bool Cancel()
    {
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                if (Input.GetKeyDown("Cancel_Keyboard") || Input.GetButtonDown("Cancel_MacOSX"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Windows:

                if (Input.GetKeyDown("Cancel_Keyboard") || Input.GetButtonDown("Cancel_Windows"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Linux:

                if (Input.GetKeyDown("Cancel_Keyboard") || Input.GetButtonDown("Cancel_Linux"))
                {
                    return true;
                }
                else return false;

            default:
                return false;
        }
    }

    public bool Sprint()
    {
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                if (Input.GetButton("Sprint_Keyboard") || Input.GetButton("Sprint_MacOSX"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Windows:

                if (Input.GetButton("Sprint_Keyboard") || Input.GetButton("Sprint_Windows"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Linux:

                if (Input.GetButton("Sprint_Keyboard") || Input.GetButton("Sprint_Linux"))
                {
                    return true;
                }
                else return false;

            default:
                return false;
        }
    }
        public bool Attack()
    {
        switch (SystemInfo.operatingSystemFamily)
        {
            case OperatingSystemFamily.MacOSX:

                if (Input.GetButtonDown("Attack_Keyboard") || Input.GetButton("Attack_MacOSX"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Windows:

                if (Input.GetButtonDown("Attack_Keyboard") || Input.GetButton("Attack_Windows"))
                {
                    return true;
                }
                else return false;

            case OperatingSystemFamily.Linux:

                if (Input.GetButtonDown("Attack_Keyboard") || Input.GetButton("Attack_Linux"))
                {
                    return true;
                }
                else return false;

            default:
                return false;
        }
    }
}
