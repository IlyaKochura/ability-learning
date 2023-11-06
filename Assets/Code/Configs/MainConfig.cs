using System;
using System.Runtime.InteropServices;
using Code.Abilities.Definition;
using Code.Abilities.Views;
using UnityEngine;
using UnityEngine.UI.Extensions;
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
        [SerializeField] private UILineRenderer _linePrefab;
        [Header("AbilityDefinitions")]
        [SerializeField] private AbilityDefinition[] _abilityDefinitions;
        [SerializeField] private int _baseAbilityIndex;

        public int AddPointsStep => _addPointsStep;
        public AbilityDefinition[] AbilityDefinitions => _abilityDefinitions;
        public AbilityNodeView AbilityNodeViewPrefab => _abilityNodeViewPrefab;
        public UILineRenderer LinePrefab => _linePrefab;
        public int BaseAbilityIndex => _baseAbilityIndex;

        public override void InstallBindings()
        {
            Container.Bind<MainConfig>().FromInstance(this).AsSingle();
        }
    }
}