using System;
using Code.Finance.Models;
using UnityEngine;
using Zenject;

namespace Code.Configs
{
    [Serializable][CreateAssetMenu(menuName = "Configs/MainConfig")]
    public class MainConfig : ScriptableObjectInstaller
    {
        [SerializeField] private FinanceStorage _financeStorage;

        public FinanceStorage FinanceStorage => _financeStorage;

        public override void InstallBindings()
        {
            Container.Bind<MainConfig>().FromInstance(this).AsSingle();
        }
    }
}