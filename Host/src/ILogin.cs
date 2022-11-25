namespace Host;

internal interface ILogin
{
    public struct UsedCredentials
    {
        /// <summary>
        /// 
        /// </summary>
        public string? UserName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string HashedPassword { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string? AuthCode { get; private set; }
    }

    public enum LoginStatus
    {
        LoggedIn,
        LoggedOut,
        DeviceLimitViolated,
        InvalidCredentials,
        UnauthoryzedLocation,

    }

    #region atributes

    /// <summary>
    /// 
    /// </summary>
    internal string? Username { get; set; }
    /// <summary>
    /// 
    /// </summary>
    internal string? HashedPassword { get; set; }

    /// <summary>
    /// 
    /// </summary>
    internal DateTime LastLogin { get; set; }

    #endregion

    #region methods

    internal LoginStatus Login() { return default; }

    #endregion


}
