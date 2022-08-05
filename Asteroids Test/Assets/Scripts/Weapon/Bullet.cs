using DefaultNamespace;
using Factories;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Bullet : MonoBehaviour
{
    public IBulletFactory OriginFactory { get; set; }
    
    [SerializeField] private float _unitPerSecond;
    
    private Vector3 _direction;
    private float _maxDistance;
    private float _distanceTraveled;
    private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
    {
        _maxDistance = ScreenBoundSize.Size.x;
    }

    public void Init(Vector2 position, Vector3 direction, Color color)
    {
        _direction = direction;
        transform.position = position;
        _spriteRenderer.color = color;
    }

    protected abstract void OnCollisionEnter2D(Collision2D other);

    private void Update()
    {
        TryMove(_direction);
    }

    private void TryMove(Vector3 direction)
    {
        float deltaMove = _unitPerSecond * Time.deltaTime;
        
        if (_distanceTraveled >= _maxDistance)
        {
            Dispose();
            return;
        }

        transform.position += direction * deltaMove;
        _distanceTraveled += deltaMove;
    }

    private void Dispose()
    {
        _distanceTraveled = 0f;
            
        OriginFactory.Reclaim(this);
    }
}
