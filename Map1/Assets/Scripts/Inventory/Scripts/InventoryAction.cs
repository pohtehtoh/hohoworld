//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Inventory/Scripts/InventoryAction.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InventoryAction : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InventoryAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InventoryAction"",
    ""maps"": [
        {
            ""name"": ""InventoryMain"",
            ""id"": ""be3d9edd-d36c-4737-b584-e25c16dd9a11"",
            ""actions"": [
                {
                    ""name"": ""Bag"",
                    ""type"": ""Button"",
                    ""id"": ""f4094948-277c-4892-a811-38306c2d83b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""842c7df5-3fb3-4708-96d3-c5c07bbec763"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GunSlot1"",
                    ""type"": ""Button"",
                    ""id"": ""484fad14-2cc7-41e7-b84c-7f6fd37bdeff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GunSlot2"",
                    ""type"": ""Button"",
                    ""id"": ""8b276d08-2ae5-4645-a0ba-0cc0db19fdaa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c5eae960-c7ad-43e9-8859-f55eee8bca1e"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74cc1f91-e344-4f98-8dfe-2db428c58a4d"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e2f4304-b761-4c9c-85aa-cc0dc5d252bd"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GunSlot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6fb50e4b-53f0-4691-a9e9-a8e94e29be29"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GunSlot2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InventoryMain
        m_InventoryMain = asset.FindActionMap("InventoryMain", throwIfNotFound: true);
        m_InventoryMain_Bag = m_InventoryMain.FindAction("Bag", throwIfNotFound: true);
        m_InventoryMain_PickUp = m_InventoryMain.FindAction("PickUp", throwIfNotFound: true);
        m_InventoryMain_GunSlot1 = m_InventoryMain.FindAction("GunSlot1", throwIfNotFound: true);
        m_InventoryMain_GunSlot2 = m_InventoryMain.FindAction("GunSlot2", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // InventoryMain
    private readonly InputActionMap m_InventoryMain;
    private IInventoryMainActions m_InventoryMainActionsCallbackInterface;
    private readonly InputAction m_InventoryMain_Bag;
    private readonly InputAction m_InventoryMain_PickUp;
    private readonly InputAction m_InventoryMain_GunSlot1;
    private readonly InputAction m_InventoryMain_GunSlot2;
    public struct InventoryMainActions
    {
        private @InventoryAction m_Wrapper;
        public InventoryMainActions(@InventoryAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Bag => m_Wrapper.m_InventoryMain_Bag;
        public InputAction @PickUp => m_Wrapper.m_InventoryMain_PickUp;
        public InputAction @GunSlot1 => m_Wrapper.m_InventoryMain_GunSlot1;
        public InputAction @GunSlot2 => m_Wrapper.m_InventoryMain_GunSlot2;
        public InputActionMap Get() { return m_Wrapper.m_InventoryMain; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryMainActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryMainActions instance)
        {
            if (m_Wrapper.m_InventoryMainActionsCallbackInterface != null)
            {
                @Bag.started -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnBag;
                @Bag.performed -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnBag;
                @Bag.canceled -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnBag;
                @PickUp.started -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnPickUp;
                @GunSlot1.started -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnGunSlot1;
                @GunSlot1.performed -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnGunSlot1;
                @GunSlot1.canceled -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnGunSlot1;
                @GunSlot2.started -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnGunSlot2;
                @GunSlot2.performed -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnGunSlot2;
                @GunSlot2.canceled -= m_Wrapper.m_InventoryMainActionsCallbackInterface.OnGunSlot2;
            }
            m_Wrapper.m_InventoryMainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Bag.started += instance.OnBag;
                @Bag.performed += instance.OnBag;
                @Bag.canceled += instance.OnBag;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @GunSlot1.started += instance.OnGunSlot1;
                @GunSlot1.performed += instance.OnGunSlot1;
                @GunSlot1.canceled += instance.OnGunSlot1;
                @GunSlot2.started += instance.OnGunSlot2;
                @GunSlot2.performed += instance.OnGunSlot2;
                @GunSlot2.canceled += instance.OnGunSlot2;
            }
        }
    }
    public InventoryMainActions @InventoryMain => new InventoryMainActions(this);
    public interface IInventoryMainActions
    {
        void OnBag(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnGunSlot1(InputAction.CallbackContext context);
        void OnGunSlot2(InputAction.CallbackContext context);
    }
}
