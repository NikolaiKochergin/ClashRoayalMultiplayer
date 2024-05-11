using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        [Inject]
        private void Construct(Game game) => 
            _game = game;

        private void Start() => 
            _game.Start();
    }
}