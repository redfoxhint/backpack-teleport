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
                    ""id"": ""ddd4dc62-a1dc-4a95-a547-e68af0e2118e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CursorControl"",
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
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""a12b2e74-9e92-4b6d-9f70-96a031fbef58"",
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
                    ""processors"": """",
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
                    ""id"": ""fa3885de-533c-4083-b82a-8f9f12451d6c"",
                    ""path"": ""<Gamepad>/buttonWest"",
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
                    ""id"": ""47c26c9c-97eb-445e-82f6-69361cea7111"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1eb5517d-29da-4a6e-836b-7d554ccc6c0f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
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
                    ""id"": ""eb83a45c-bbad-486d-8db0-dfa4504a2c0e"",
                    ""path"": ""<Gamepad>/select"",
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
                    ""id"": ""3256a3bd-724b-4b86-bd8e-375f9bdaf0b0"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TogglePauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""7412c716-f16a-4c75-a3fc-d45691361496"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorControl"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e5fd33bd-0e91-4a1d-8878-c6d52704ded0"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""242b2263-d89f-4d21-8d29-dcf3afdd82f2"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a2e6f73f-3ea2-490f-8bc5-35c4c48eee3a"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""21a347d1-2bfe-483a-9e39-de7c366f57d4"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""687a4a9b-4539-4a28-bec8-40d6c372865d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""597fe5cb-6163-4ac4-9bd0-6f2db0a08d17"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Stick"",
                    ""id"": ""1b571946-c310-4784-90d8-42e86abb9d39"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""debeab54-4714-4bf0-95db-24f435a99dc3"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9a1a094d-1994-4e96-b20c-599f136d3b53"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c61d7cb3-bd0c-40c5-9b08-b682f820f646"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6635dec8-1333-4c35-afb8-773910fdb0d8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
                    ""expectedControlType"": ""Button"",
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
                    ""name"": ""PlaceMarker"",
                    ""type"": ""Button"",
                    ""id"": ""c7944641-8114-4f70-a492-12a395b29102"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Teleport"",
                    ""type"": ""Button"",
                    ""id"": ""b8d536c2-db9f-4e70-91e3-eb4356151faa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThrowBackpack"",
                    ""type"": ""Button"",
                    ""id"": ""48065590-68da-4025-9a8e-d2247b18e267"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0e43e368-8a81-4d38-874f-1c5e45c50a0b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
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
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e06a780-5d1e-44f5-8d5d-26b38e85bc2c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceMarker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""347c4f89-53db-4d02-aae0-b1872d3186ad"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PlaceMarker"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a922bda-226d-4bac-a289-b45f6fda8bae"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Teleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f823da7-eacf-421c-8532-a21514c7e74b"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Teleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f95f2ebf-e908-435c-83e1-11a5a4d2697c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowBackpack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ce4914e-184b-4e5a-94c9-8a58479d0f45"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowBackpack"",
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
        m_Player_CursorControl = m_Player.FindAction("CursorControl", throwIfNotFound: true);
        m_Player_BasicAttack = m_Player.FindAction("BasicAttack", throwIfNotFound: true);
        m_Player_DashAttack = m_Player.FindAction("DashAttack", throwIfNotFound: true);
        m_Player_ToggleInventory = m_Player.FindAction("ToggleInventory", throwIfNotFound: true);
        m_Player_TogglePauseMenu = m_Player.FindAction("TogglePauseMenu", throwIfNotFound: true);
        m_Player_Interact = m_Player.FindAction("Interact", throwIfNotFound: true);
        // Backpack
        m_Backpack = asset.FindActionMap("Backpack", throwIfNotFound: true);
        m_Backpack_Aim = m_Backpack.FindAction("Aim", throwIfNotFound: true);
        m_Backpack_Return = m_Backpack.FindAction("Return", throwIfNotFound: true);
        m_Backpack_PlaceMarker = m_Backpack.FindAction("PlaceMarker", throwIfNotFound: true);
        m_Backpack_Teleport = m_Backpack.FindAction("Teleport", throwIfNotFound: true);
        m_Backpack_ThrowBackpack = m_Backpack.FindAction("ThrowBackpack", throwIfNotFound: true);
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
    private readonly InputAction m_Player_CursorControl;
    private readonly InputAction m_Player_BasicAttack;
    private readonly InputAction m_Player_DashAttack;
    private readonly InputAction m_Player_ToggleInventory;
    private readonly InputAction m_Player_TogglePauseMenu;
    private readonly InputAction m_Player_Interact;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @CursorControl => m_Wrapper.m_Player_CursorControl;
        public InputAction @BasicAttack => m_Wrapper.m_Player_BasicAttack;
        public InputAction @DashAttack => m_Wrapper.m_Player_DashAttack;
        public InputAction @ToggleInventory => m_Wrapper.m_Player_ToggleInventory;
        public InputAction @TogglePauseMenu => m_Wrapper.m_Player_TogglePauseMenu;
        public InputAction @Interact => m_Wrapper.m_Player_Interact;
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
                @CursorControl.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCursorControl;
                @CursorControl.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCursorControl;
                @CursorControl.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCursorControl;
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
                @Interact.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @CursorControl.started += instance.OnCursorControl;
                @CursorControl.performed += instance.OnCursorControl;
                @CursorControl.canceled += instance.OnCursorControl;
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
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Backpack
    private readonly InputActionMap m_Backpack;
    private IBackpackActions m_BackpackActionsCallbackInterface;
    private readonly InputAction m_Backpack_Aim;
    private readonly InputAction m_Backpack_Return;
    private readonly InputAction m_Backpack_PlaceMarker;
    private readonly InputAction m_Backpack_Teleport;
    private readonly InputAction m_Backpack_ThrowBackpack;
    public struct BackpackActions
    {
        private @InputActions m_Wrapper;
        public BackpackActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_Backpack_Aim;
        public InputAction @Return => m_Wrapper.m_Backpack_Return;
        public InputAction @PlaceMarker => m_Wrapper.m_Backpack_PlaceMarker;
        public InputAction @Teleport => m_Wrapper.m_Backpack_Teleport;
        public InputAction @ThrowBackpack => m_Wrapper.m_Backpack_ThrowBackpack;
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
                @PlaceMarker.started -= m_Wrapper.m_BackpackActionsCallbackInterface.OnPlaceMarker;
                @PlaceMarker.performed -= m_Wrapper.m_BackpackActionsCallbackInterface.OnPlaceMarker;
                @PlaceMarker.canceled -= m_Wrapper.m_BackpackActionsCallbackInterface.OnPlaceMarker;
                @Teleport.started -= m_Wrapper.m_BackpackActionsCallbackInterface.OnTeleport;
                @Teleport.performed -= m_Wrapper.m_BackpackActionsCallbackInterface.OnTeleport;
                @Teleport.canceled -= m_Wrapper.m_BackpackActionsCallbackInterface.OnTeleport;
                @ThrowBackpack.started -= m_Wrapper.m_BackpackActionsCallbackInterface.OnThrowBackpack;
                @ThrowBackpack.performed -= m_Wrapper.m_BackpackActionsCallbackInterface.OnThrowBackpack;
                @ThrowBackpack.canceled -= m_Wrapper.m_BackpackActionsCallbackInterface.OnThrowBackpack;
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
                @PlaceMarker.started += instance.OnPlaceMarker;
                @PlaceMarker.performed += instance.OnPlaceMarker;
                @PlaceMarker.canceled += instance.OnPlaceMarker;
                @Teleport.started += instance.OnTeleport;
                @Teleport.performed += instance.OnTeleport;
                @Teleport.canceled += instance.OnTeleport;
                @ThrowBackpack.started += instance.OnThrowBackpack;
                @ThrowBackpack.performed += instance.OnThrowBackpack;
                @ThrowBackpack.canceled += instance.OnThrowBackpack;
            }
        }
    }
    public BackpackActions @Backpack => new BackpackActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCursorControl(InputAction.CallbackContext context);
        void OnBasicAttack(InputAction.CallbackContext context);
        void OnDashAttack(InputAction.CallbackContext context);
        void OnToggleInventory(InputAction.CallbackContext context);
        void OnTogglePauseMenu(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
    public interface IBackpackActions
    {
        void OnAim(InputAction.CallbackContext context);
        void OnReturn(InputAction.CallbackContext context);
        void OnPlaceMarker(InputAction.CallbackContext context);
        void OnTeleport(InputAction.CallbackContext context);
        void OnThrowBackpack(InputAction.CallbackContext context);
    }
}
