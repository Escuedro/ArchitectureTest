using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(HealthBehaviour))]
public class Entity : MonoBehaviour
{
    private MoveBehaviour _moveBehaviour;
    private HealthBehaviour _healthBehaviour;

    //чтобы другие Entity могли брать компонент и дамажить/хилить
    public HealthBehaviour HealthBehaviour
    {
        get => _healthBehaviour;
        set => _healthBehaviour = value;
    }

    private void Awake()
    {
        _moveBehaviour = GetComponent<MoveBehaviour>();
        _healthBehaviour = GetComponent<HealthBehaviour>();
        
        //почему подписка здесь? потому что можно получить отсюда нужные данные без GetComponent
        _healthBehaviour.onDieAction += () => Debug.Log("Death");
    }
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        _moveBehaviour.Move();
    }
    
    
    //в идеале надо перенести в другой компонент, отвечающий за инпут
    public Vector3 ReadInput()
    {
        var normalizedDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        return normalizedDirection;
    }
}
