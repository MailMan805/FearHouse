using UnityEngine;
using UnityEngine.InputSystem;

// Define your input actions class
public class PlayerControls : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;

    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
            ""name"": ""PlayerControls"",
            ""maps"": [
                {
                    ""name"": ""Player1"",
                    ""id"": ""1c4a7e83-0a91-4f82-8257-ec4aa7d24c0e"",
                    ""actions"": [
                        {
                            ""name"": ""Move"",
                            ""type"": ""Value"",
                            ""id"": ""13c0d6d7-84f6-43d3-8773-49bc0d0aa9b7"",
                            ""expectedControlType"": ""Vector2"",
                            ""processors"": """",
                            ""interactions"": """"
                        }
                    ],
                    ""bindings"": [
                        {
                            ""name"": ""WASD"",
                            ""path"": ""2DVector"",
                            ""interactions"": """",
                            ""processors"": """",
                            ""groups"": ""Keyboard&Mouse"",
                            ""action"": ""Move"",
                            ""isComposite"": true,
                            ""isPartOfComposite"": false
                        },
                        {
                            ""name"": ""up"",
                            ""path"": ""<Keyboard>/w"",
                            ""action"": ""Move"",
                            ""isCompositePart"": true
                        },
                        {
                            ""name"": ""down"",
                            ""path"": ""<Keyboard>/s"",
                            ""action"": ""Move"",
                            ""isCompositePart"": true
                        },
                        {
                            ""name"": ""left"",
                            ""path"": ""<Keyboard>/a"",
                            ""action"": ""Move"",
                            ""isCompositePart"": true
                        },
                        {
                            ""name"": ""right"",
                            ""path"": ""<Keyboard>/d"",
                            ""action"": ""Move"",
                            ""isCompositePart"": true
                        }
                    ]
                },
                {
                    ""name"": ""Player2"",
                    ""id"": ""db64b947-405b-4040-b79f-4b5fcdb92d6d"",
                    ""actions"": [
                        {
                            ""name"": ""Move"",
                            ""type"": ""Value"",
                            ""id"": ""e8a9e81c-e7a4-4c76-bb77-e4f287d69da0"",
                            ""expectedControlType"": ""Vector2"",
                            ""processors"": """",
                            ""interactions"": """"
                        }
                    ],
                    ""bindings"": [
                        {
                            ""name"": ""Joystick"",
                            ""path"": ""<Gamepad>/leftStick"",
                            ""action"": ""Move"",
                            ""isCompositePart"": false
                        }
                    ]
                }
            ],
            ""controlSchemes"": []
        }");

        // Initialize the action maps
        Player1 = asset.FindActionMap("Player1", true);
        Player2 = asset.FindActionMap("Player2", true);

        // Initialize actions
        MovePlayer1 = Player1.FindAction("Move", true);
        MovePlayer2 = Player2.FindAction("Move", true);
    }

    public InputActionMap Player1 { get; }
    public InputActionMap Player2 { get; }

    public InputAction MovePlayer1 { get; }
    public InputAction MovePlayer2 { get; }

    public void Dispose() { }
    public void Enable() => asset.Enable();
    public void Disable() => asset.Disable();
}