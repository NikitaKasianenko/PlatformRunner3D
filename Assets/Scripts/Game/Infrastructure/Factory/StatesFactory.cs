using Game.Infrastructure.States;
using Game.Infrastructure.States.StateMachine;
using Zenject;

namespace Game.Infrastructure.Factory
{

    public class StatesFactory {
        private DiContainer _diContainer;

        public StatesFactory (DiContainer diContainer) {
            _diContainer = diContainer;
        }

        public TState Create<TState> () where TState : class, IExitableState { 
            return _diContainer.Instantiate<TState>();
        }
    }
}