using System;
using Code.Abilities;
using Code.Abilities.Views;
using Code.Finance.Models;
using UnityEngine;
using Zenject;

namespace Code.Configs
{
    [Serializable][CreateAssetMenu(menuName = "Configs/MainConfig")]
    public class MainConfig : ScriptableObjectInstaller
    {
        [SerializeField] private AbilityNodeView _abilityNodeViewPrefab;
        [SerializeField] private FinanceStorage _financeStorage;
        [SerializeField] private AbilityDefinition[] _abilityDefinitions;
        [SerializeField] private int _baseAbilityIndex;

        public FinanceStorage FinanceStorage => _financeStorage;
        public AbilityDefinition[] AbilityDefinitions => _abilityDefinitions;
        public AbilityNodeView AbilityNodeViewPrefab => _abilityNodeViewPrefab;
        public int BaseAbilityIndex => _baseAbilityIndex;

        public override void InstallBindings()
        {
            Container.Bind<MainConfig>().FromInstance(this).AsSingle();
        }
    }
}