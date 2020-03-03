// GENERATED AUTOMATICALLY FROM 'Assets/Presets/Input/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e974f60b-b907-4694-988f-ec7e66b6f16e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9b98cf0a-eafc-431c-b88c-eac472b48ad4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BasicAttack"",
                    ""type"": ""Button"",
                    ""id"": ""d935575e-6768-4600-8bb6-d2b90c24a5e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DashAttack"",
                    ""type"": ""Button"",
                    ""id"": ""9b771ae4-21a1-4b52-b5c1-dca5a28beaaf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleInventory"",
                    ""type"": ""Button"",
                    ""id"": ""2f85bda1-32b1-42b6-b31e-9ebd07f0acd7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TogglePauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""df1a4726-d3b8-4b00-af35-5c06b56dbde9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""114e1dcd-7ba7-4a47-a076-446e6171039b"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7d6a604f-6593-4c6c-ad63-791c0ff687ba"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2ed59b8b-cd42-4937-a90a-ea633eb3fb3d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""134b2812-c03b-4841-93b4-78a070261c64"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""15c0e366-914f-48ef-b53f-4e7aaf5f34d1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""140ceec6-4ca7-402f-afec-1ec955ea37d4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ee023b70-cea0-42dd-b509-cf4937eca1cd"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ee25f295-0a75-487b-bd7d-73bd31a3db9e"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""15869b56-308b-4fa3-a444-71c696d69455"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4a1db329-030b-49e7-820a-3630f37830fc"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2f2f8acf-29b1-4ce2-98e1-33670bc0d159"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b7e1b47-9741-4e7a-aee4-c3e15bcf59b9"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""304e78ba-d13e-4f0b-a1ea-99b4e8f84daf"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68455b52-8367-4ee2-a853-27f4b69086ff"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BasicAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e4b35e1-2ed7-48c9-93af-4aba69e2fbec"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7aa81a80-ab47-4064-8aae-b4b4c0ec1ece"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4444081b-6d23-425f-ae4c-23dd5925a69a"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8656d377-94d4-407e-9816-6ec61f810e73"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36eb4301-ad18-4e05-92af-fec3afbd5e66"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7ba7384-96d3-4acc-85a6-77a1ff1f52d3"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4cf926a3-bbbc-48ef-85e5-3239dca5d364"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Backpack"",
            ""id"": ""36c9df3d-ce12-467d-9c19-87008014e5f0"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""f5d5f785-11a1-400b-929c-c852a5c7f868"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""b58840ea-7188-4bf4-9392-626f7c232199"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""1c24e76a-ec9a-4a48-88a9-7ad85ac679a1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0e43e368-8a81-4d38-874f-1c5e45c50a0b"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button8"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6d54e0d-9344-4f5a-9325-81c9ded8ee34"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5b47dd7-4a66-4710-8167-574dba370e9c"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69347cf4-bb07-407e-95f5-a193d0822dd2"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_BasicAttack = m_Player.FindAction("BasicAttack", throwIfNotFound: true);
        m_Player_DashAttack = m_Player.FindAction("DashAttack", throwIfNotFound: true);
        m_Player_ToggleInventory = m_Player.FindAction("ToggleInventory", throwIfNotFound: true);
        m_Player_TogglePauseMenu = m_Player.FindAction("TogglePauseMenu", throwIfNotFound: true);
        // Backpack
        m_Backpack = asset.FindActionMap("Backpack", throwIfNotFound: true);
        m_Backpack_Aim = m_Backpack.FindAction("Aim", throwIfNotFound: true);
        m_Backpack_Return = m_Backpack.FindAction("Return", throwIfNotFound: true);
        m_Backpack_Throw = m_Backpack.FindAction("Throw", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_BasicAttack;
    private readonly InputAction m_Player_DashAttack;
    private readonly InputAction m_Player_ToggleInventory;
    private readonly InputAction m_Player_TogglePauseMenu;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @BasicAttack => m_Wrapper.m_Player_BasicAttack;
        public InputAction @DashAttack => m_Wrapper.m_Player_DashAttack;
        public InputAction @ToggleInventory => m_Wrapper.m_Player_ToggleInventory;
        public InputAction @TogglePauseMenu => m_Wrapper.m_Player_TogglePauseMenu;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @BasicAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBasicAttack;
                @BasicAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnBasicAttack;
                @DashAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashAttack;
                @DashAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashAttack;
                @DashAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDashAttack;
                @ToggleInventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInventory;
                @TogglePauseMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTogglePauseMenu;
                @TogglePauseMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTogglePauseMenu;
                @TogglePauseMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTogglePauseMenu;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @BasicAttack.started += instance.OnBasicAttack;
                @BasicAttack.performed += instance.OnBasicAttack;
                @BasicAttack.canceled += instance.OnBasicAttack;
                @DashAttack.started += instance.OnDashAttack;
                @DashAttack.performed += instance.OnDashAttack;
                @DashAttack.canceled += instance.OnDashAttack;
                @ToggleInventory.started += instance.OnToggleInventory;
                @ToggleInventory.performed += instance.OnToggleInventory;
                @ToggleInventory.canceled += instance.OnToggleInventory;
                @TogglePauseMenu.started += instance.OnTogglePauseMenu;
                @TogglePauseMenu.performed += instance.OnTogglePauseMenu;
                @TogglePauseMenu.canceled += instance.OnTogglePauseMenu;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Backpack
    private readonly InputActionMap m_Backpack;
    private IBackpackActions m_BackpackActionsCallbackInterface;
    private readonly InputAction m_Backpack_Aim;
    private readonly InputAction m_Backpack_Return;
    private readonly InputAction m_Backpack_Throw;
    public struct BackpackActions
    {
        private @InputActions m_Wrapper;
        public BackpackActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_Backpack_Aim;
        public InputAction @Return => m_Wrapper.m_Backpack_Return;
        public InputAction @Throw => m_Wrapper.m_Backpack_Throw;
        public InputActionMap Get() { return m_Wrapper.m_Backpack; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BackpackActions set) { return set.Get(); }
        public void SetCallbacks(IBackpackActions instance)
        {
            if (m_Wrapper.m_BackpackActionsCallbackInterface != null)
            {
                @Aim.started -= m_Wrapper.m_BackpackActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_BackpackActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_BackpackActionsCallbackInterface.OnAim;
                @Return.started -= m_Wrapper.m_BackpackActionsCallbackInterface.OnReturn;
                @Return.performed -= m_Wrapper.m_BackpackActionsCallbackInterface.OnReturn;
                @Return.canceled -= m_Wrapper.m_BackpackActionsCallbackInterface.OnReturn;
                @Throw.started -= m_Wrapper.m_BackpackActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_BackpackActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_BackpackActionsCallbackInterface.OnThrow;
            }
            m_Wrapper.m_BackpackActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Return.started += instance.OnReturn;
                @Return.performed += instance.OnReturn;
                @Return.canceled += instance.OnReturn;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
            }
        }
    }
    public BackpackActions @Backpack => new BackpackActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnBasicAttack(InputAction.CallbackContext context);
        void OnDashAttack(InputAction.CallbackContext context);
        void OnToggleInventory(InputAction.CallbackContext context);
        void OnTogglePauseMenu(InputAction.CallbackContext context);
    }
    public interface IBackpackActions
    {
        void OnAim(InputAction.CallbackContext context);
        void OnReturn(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
    }
}
