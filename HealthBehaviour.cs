using System;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private float startHealthPoints = 100;
    [SerializeField] private HealthBar healthBar;

    public Action onDieAction;

    private HealthProcessor _healthProcessor;
    private void Awake()
    {
        _healthProcessor = new HealthProcessor(startHealthPoints)
        {
            onDieAction = onDieAction,
            onDamageAction = HealthProcessor_OnDamageAction,
            onHealAction = HealthProcessor_OnHealAction
        };
    }

    #region HealthEventsFunctions

    private void HealthProcessor_OnDamageAction(HealthProcessor.HealthModifyArgs healthModifyArgs)
    {
        //Create some graphics;
    }
    
    private void HealthProcessor_OnHealAction(HealthProcessor.HealthModifyArgs healthModifyArgs)
    {
        //Create some graphics;
    }

    #endregion


    public void ModifyHealth(float amount)
    {
        _healthProcessor.ModifyHealth(amount);
        
    }
    
    internal class HealthProcessor
    {
        public Action onDieAction;
        
        public Action<HealthModifyArgs> onDamageAction;
        public Action<HealthModifyArgs> onHealAction;

        public class HealthModifyArgs
        {
            public float healthAmount;
        }

        private float _startHealthPoints;
        private float _currentHealthPoints;

        public float GetHealthPercentage() => _currentHealthPoints / _startHealthPoints;

        public HealthProcessor(float startHealthPoints)
        {
            _startHealthPoints = startHealthPoints;
            _currentHealthPoints = _startHealthPoints;
        }

        public void ModifyHealth(float amount)
        {
            _currentHealthPoints -= amount;
            
            //damage graphics with floating text maybe
            if (amount < 0)
            {
                onDamageAction?.Invoke(new HealthModifyArgs {healthAmount = amount});
            }
            //heal graphics with floating text maybe
            else if (amount > 0)
            {
                onHealAction?.Invoke(new HealthModifyArgs {healthAmount = amount});
            }

            if (_currentHealthPoints < 0)
            {
                _currentHealthPoints = 0;
                //смэрть
                onDieAction?.Invoke();
            }
        }
    }
}
