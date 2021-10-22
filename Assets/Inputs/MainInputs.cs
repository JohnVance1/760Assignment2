// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/MainInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInputs"",
    ""maps"": [
        {
            ""name"": ""FishAI"",
            ""id"": ""c260a35e-69a3-46fa-8ec3-eea480ee9f78"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""66010e7a-240e-4a46-9df1-030f92a44d76"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateX"",
                    ""type"": ""Value"",
                    ""id"": ""7d4bad5d-7b0d-4581-adf1-b223a5350693"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateY"",
                    ""type"": ""Value"",
                    ""id"": ""dcd41a05-4130-43ed-a0a8-54ac0ea76cfe"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""aef8e462-a9bf-468e-b664-003e4d9e01f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""901576e7-3adf-4d37-9195-5a32043a8ca8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""45ae3b17-a92e-4f4c-a1e2-78238078dce0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e77a46e9-3a42-42e5-beba-84a7d50d5244"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4710d6a1-c664-4dd7-ac76-183de8b298f3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""69276064-7e7c-4230-8f9e-5769efafa8cb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""20a96368-41f2-44b9-946c-afd01580389a"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9c91a02-4297-48b9-a41b-4efaba074f91"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fff6302-10af-482e-989c-020d28dcc1d1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // FishAI
        m_FishAI = asset.FindActionMap("FishAI", throwIfNotFound: true);
        m_FishAI_Move = m_FishAI.FindAction("Move", throwIfNotFound: true);
        m_FishAI_RotateX = m_FishAI.FindAction("RotateX", throwIfNotFound: true);
        m_FishAI_RotateY = m_FishAI.FindAction("RotateY", throwIfNotFound: true);
        m_FishAI_MouseClick = m_FishAI.FindAction("MouseClick", throwIfNotFound: true);
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

    // FishAI
    private readonly InputActionMap m_FishAI;
    private IFishAIActions m_FishAIActionsCallbackInterface;
    private readonly InputAction m_FishAI_Move;
    private readonly InputAction m_FishAI_RotateX;
    private readonly InputAction m_FishAI_RotateY;
    private readonly InputAction m_FishAI_MouseClick;
    public struct FishAIActions
    {
        private @MainInputs m_Wrapper;
        public FishAIActions(@MainInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_FishAI_Move;
        public InputAction @RotateX => m_Wrapper.m_FishAI_RotateX;
        public InputAction @RotateY => m_Wrapper.m_FishAI_RotateY;
        public InputAction @MouseClick => m_Wrapper.m_FishAI_MouseClick;
        public InputActionMap Get() { return m_Wrapper.m_FishAI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FishAIActions set) { return set.Get(); }
        public void SetCallbacks(IFishAIActions instance)
        {
            if (m_Wrapper.m_FishAIActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_FishAIActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_FishAIActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_FishAIActionsCallbackInterface.OnMove;
                @RotateX.started -= m_Wrapper.m_FishAIActionsCallbackInterface.OnRotateX;
                @RotateX.performed -= m_Wrapper.m_FishAIActionsCallbackInterface.OnRotateX;
                @RotateX.canceled -= m_Wrapper.m_FishAIActionsCallbackInterface.OnRotateX;
                @RotateY.started -= m_Wrapper.m_FishAIActionsCallbackInterface.OnRotateY;
                @RotateY.performed -= m_Wrapper.m_FishAIActionsCallbackInterface.OnRotateY;
                @RotateY.canceled -= m_Wrapper.m_FishAIActionsCallbackInterface.OnRotateY;
                @MouseClick.started -= m_Wrapper.m_FishAIActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_FishAIActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_FishAIActionsCallbackInterface.OnMouseClick;
            }
            m_Wrapper.m_FishAIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateX.started += instance.OnRotateX;
                @RotateX.performed += instance.OnRotateX;
                @RotateX.canceled += instance.OnRotateX;
                @RotateY.started += instance.OnRotateY;
                @RotateY.performed += instance.OnRotateY;
                @RotateY.canceled += instance.OnRotateY;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
            }
        }
    }
    public FishAIActions @FishAI => new FishAIActions(this);
    public interface IFishAIActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotateX(InputAction.CallbackContext context);
        void OnRotateY(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
    }
}
