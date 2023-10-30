using System;
using UnityEngine;

namespace Code.Finance.Models
{
    [Serializable][CreateAssetMenu(menuName = "Configs/Finance/FinanceStorage")]
    public class FinanceStorage : ScriptableObject
    {
        [SerializeField] private int _pointsCount;

        public int PointsCount => _pointsCount;

        public void SetPoints(int points)
        {
            _pointsCount = points;
        }
    }
}