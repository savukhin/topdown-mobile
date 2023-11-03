using UnityEngine;
using UniRx;

public class EnemyTargeterSystem : BaseSystem<EnemyTargeterSystem>
{
    private Enemy[] _enemies;
    private ReactiveProperty<Enemy> _prevEnemy = new ReactiveProperty<Enemy>(null);

    void Awake() {   
        _instance = this; 
    }

    void Start()
    {
        // TODO: subscribe to enemy spawner
        _enemies = FindObjectsOfType<Enemy>();
    }

    public Enemy TargetEnemy(Vector3 position, Vector3 direction) {
        float minDot = float.PositiveInfinity;
        Enemy result = null;

        // TODO: job + burst
        foreach (var enemy in _enemies)
        {
            if (enemy == null)
                continue;
                
            Vector3 vec = enemy.transform.position - position;
            float dot = Vector3.Dot(direction, vec);
            dot = Mathf.Abs(dot);
            dot /= vec.magnitude;

            dot = Mathf.Acos(dot);

            if (dot < minDot) {
                minDot = dot;
                result = enemy;
            }
        }

        if (_prevEnemy.Value != null && _prevEnemy.Value.gameObject != null)
            _prevEnemy.Value?.SetChosen(false);
            
        result?.SetChosen(true);
        _prevEnemy.Value = result;

        return result;
    }
}
