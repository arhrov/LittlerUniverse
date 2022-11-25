using UnityEngine;

namespace LittlerUniverse
{
    [System.Serializable]
    public class ResourceExchageDataEntry
    {
        #region Properties

        public ResourceType ResourceType => resourceType;

        public int ResourceAmount => resourceAmount;

        public int ResourceExchangedAmount => resourceUnitsExchangedAmount;

        public int ResourceToExchangeAmount => resourceAmount - resourceUnitsExchangedAmount;

        public bool IsFull => ResourceToExchangeAmount <= 0;

        #endregion

        #region Fields

        [SerializeField]
        private ResourceType resourceType = default;

        [SerializeField]
        private int resourceAmount = 10;

        private int resourceUnitsExchangedAmount = 0;

        #endregion

        #region Add

        public void AddResourceExchanged(int resourceAmount)
        {
            resourceUnitsExchangedAmount += resourceAmount;
        }

        #endregion
    }
}