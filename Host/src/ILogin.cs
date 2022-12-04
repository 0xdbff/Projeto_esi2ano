namespace Host;

/// <summary>
/// 
/// </summary>
internal enum UserType
{
    /// <summary> </summary>
    Admin,
    /// <summary> </summary>
    Trainer,
    /// <summary> </summary>
    Client
}

/// <summary>
/// 
/// </summary>
internal enum IpType
{
    v4,
    v6
}

/// <summary>
/// 
/// </summary>
internal struct Ip
{ 
    internal IpType Type { get; set; }
}

/// <summary>
/// 
/// </summary>
internal interface ILogin
{
    /// <summary>
    /// 
    /// </summary>
    public enum AuthType
    {
        /// <summary> </summary>
        Email,
        /// <summary> </summary>
        UserName,
    }
    
    /// <summary>
    /// 
    /// </summary>
    internal struct UsedCredentials
    {
        /// <summary>
        /// 
        /// </summary>
        internal AuthType AuthType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        internal string? HashedPassword { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string? AuthCode { get; private set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum LoginStatus
    {
        /// <summary> </summary>
        LoggedIn,
        /// <summary> </summary>
        LoggedOut,
        /// <summary> </summary>
        DeviceLimitViolated,
        /// <summary> </summary>
        InvalidCredentials,
        /// <summary> </summary>
        UnauthoryzedLocation,
        /// <summary> </summary>
        WaitingForAuthCode,
        /// <summary> </summary>
        AuthCodeExpired,
    }

    #region data

    /// <summary>
    /// 
    /// </summary>
    internal struct Data
    {
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
    }

    #endregion

    #region methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private protected LoginStatus Login();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal static List<UsedCredentials>? GetCredentialsHistory() { return default; }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    internal static List<Data>? GetLoginHistory() { return default; }
    
    #endregion
}