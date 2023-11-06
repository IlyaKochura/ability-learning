using System;
using Code.Abilities.Definition;
using Code.Abilities.Views;
using UnityEngine;
using Zenject;

namespace Code.Configs
{
    [Serializable][CreateAssetMenu(menuName = "Configs/MainConfig")]
    public class MainConfig : ScriptableObjectInstaller
    {
        [Header("Finance")]
        [SerializeField] private int _addPointsStep;
        [Header("Prefabs")]
        [SerializeField] private AbilityNodeView _abilityNodeViewPrefab;
        [Header("AbilityDefinitions")]
        [SerializeField] private AbilityDefinition[] _abilityDefinitions;
        [SerializeField] private int _baseAbilityIndex;

        public int AddPointsStep => _addPointsStep;
        public AbilityDefinition[] AbilityDefinitions => _abilityDefinitions;
        public AbilityNodeView AbilityNodeViewPrefab => _abilityNodeViewPrefab;
        public int BaseAbilityIndex => _baseAbilityIndex;

        public override void InstallBindings()
        {
            Container.Bind<MainConfig>().FromInstance(this).AsSingle();
        }
    }
}