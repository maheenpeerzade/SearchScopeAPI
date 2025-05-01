using System.Runtime.Serialization;

namespace SearchScopeAPI.SearchScope.Core.Utility
{
    /// <summary>
    /// ProductEnum.
    /// </summary>
    public enum ProductEnum
    {
        /// <summary>
        /// Unit price.
        /// </summary>
        [EnumMember(Value = "Unit Price")]
        UnitPrice,

        /// <summary>
        /// Units in stock.
        /// </summary>
        [EnumMember(Value = "Units In Stock")]
        UnitsInStock,

        /// <summary>
        /// Popularity.
        /// </summary>
        [EnumMember(Value = "Popularity")]
        Popularity,

        /// <summary>
        /// Production date.
        /// </summary>
        [EnumMember(Value = "ProductionData")]
        ProductionData
    }
}
