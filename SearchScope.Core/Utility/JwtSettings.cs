namespace SearchScopeAPI.SearchScope.Core.Utility
{
    /// <summary>
    /// JwtSettings model.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Secret key.
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Issuer.
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Audience.
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Expiry minutes.
        /// </summary>
        public int ExpiryMinutes { get; set; }
    }
}
