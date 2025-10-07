using UI.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] private Button Level1, Level2;
        private SignalBus _signalBus;


        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            Level1.onClick.AddListener(() => _signalBus.Fire(new LevelChosenSignal("Level1")));
            Level2.onClick.AddListener(() => _signalBus.Fire(new LevelChosenSignal("Level2")));
        }
    }
}