using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SearchScopeAPI.SearchScope.Core.Utility
{

    public enum ProductEnum
    {
        [EnumMember(Value = "Unit Price")]
        UnitPrice,

        [EnumMember(Value = "Units In Stock")]
        UnitsInStock,

        [EnumMember(Value = "Popularity")]
        Popularity,

        [EnumMember(Value = "ProductionData")]
        ProductionData
    }
}
