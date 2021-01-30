// GENERATED AUTOMATICALLY FROM 'Assets/Settings/DefaultControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DefaultControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultControls"",
    ""maps"": [
        {
            ""name"": ""DefaultMovement"",
            ""id"": ""edcebcc5-ba63-461a-b1c9-f6ad71e0fe64"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""eccad802-f5f6-48ee-8bb1-2494c440723c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Value"",
                    ""id"": ""c5a87050-e983-4546-a749-93f2f3a55e8d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""560d3afd-a62f-442c-adbc-ab59237007ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b1d14df9-86c7-4fff-a786-3e77e15cf234"",
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
                    ""id"": ""31209103-a52c-494f-8b34-7912f5988c53"",
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
                    ""id"": ""ef8dfd2f-2f04-4490-b722-d2c08ee761dc"",
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
                    ""id"": ""ce6d5ec7-d2d0-4c88-a732-64290de07d13"",
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
                    ""id"": ""3728adb5-421a-4b23-8a80-e03b761a7e56"",
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
                    ""id"": ""31510920-02b2-4a4f-a973-479feb27a3ce"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3427e5b1-864a-414d-8749-49b718270d5e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DefaultMovement
        m_DefaultMovement = asset.FindActionMap("DefaultMovement", throwIfNotFound: true);
        m_DefaultMovement_Movement = m_DefaultMovement.FindAction("Movement", throwIfNotFound: true);
        m_DefaultMovement_Turn = m_DefaultMovement.FindAction("Turn", throwIfNotFound: true);
        m_DefaultMovement_Use = m_DefaultMovement.FindAction("Use", throwIfNotFound: true);
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

    // DefaultMovement
    private readonly InputActionMap m_DefaultMovement;
    private IDefaultMovementActions m_DefaultMovementActionsCallbackInterface;
    private readonly InputAction m_DefaultMovement_Movement;
    private readonly InputAction m_DefaultMovement_Turn;
    private readonly InputAction m_DefaultMovement_Use;
    public struct DefaultMovementActions
    {
        private @DefaultControls m_Wrapper;
        public DefaultMovementActions(@DefaultControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_DefaultMovement_Movement;
        public InputAction @Turn => m_Wrapper.m_DefaultMovement_Turn;
        public InputAction @Use => m_Wrapper.m_DefaultMovement_Use;
        public InputActionMap Get() { return m_Wrapper.m_DefaultMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultMovementActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultMovementActions instance)
        {
            if (m_Wrapper.m_DefaultMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnMovement;
                @Turn.started -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnTurn;
                @Use.started -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_DefaultMovementActionsCallbackInterface.OnUse;
            }
            m_Wrapper.m_DefaultMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
            }
        }
    }
    public DefaultMovementActions @DefaultMovement => new DefaultMovementActions(this);
    public interface IDefaultMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
    }
}
