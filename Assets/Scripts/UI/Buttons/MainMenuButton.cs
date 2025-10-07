using Game.Infrastructure.States;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button  _button;
        private GameStateMachine _gameStateMachine;


        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleMainMenuButton);
        }

        private void HandleMainMenuButton()
        {
            _gameStateMachine.Enter<MainMenuState>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleMainMenuButton);
        }
    }
}