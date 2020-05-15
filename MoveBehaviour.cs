using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    public float Speed { get; set; }

    private MoveProcessor _moveProcessor;

    private void Awake()
    {
        Speed = speed;
        _moveProcessor = new MoveProcessor(GetComponent<Entity>(), this);
    }

    public void Move()
    {
        _moveProcessor.Tick(Time.deltaTime);
    }

    private class MoveProcessor : IUpdateableComponent
    {
        private readonly Entity _entityToMove;
        private readonly MoveBehaviour _moveBehaviour;
        
        //сюда можно ещё будет запихнуть данные типа модификаторов движения, хз

        public MoveProcessor(Entity entityToMove, MoveBehaviour behaviour)
        {
            _entityToMove = entityToMove;
            _moveBehaviour = behaviour;
        }
        public void Tick(float deltaTime)
        {
            var movementDirection = _entityToMove.ReadInput();

            if (movementDirection == Vector3.zero) return;

            _entityToMove.transform.position += movementDirection * (deltaTime * _moveBehaviour.Speed);
        }

    }
}
