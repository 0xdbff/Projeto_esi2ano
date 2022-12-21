using Host;

namespace Host;

/// <summary>
///
/// </summary>
internal class Trainer : Person, ILogin
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="gender"></param>
    /// <param name="dateOfBirth"></param>
    /// <param name="nif"></param>
    /// <param name="address"></param>
    public Trainer(string firstName, string lastName, Gender gender,
                   DateTime dateOfBirth, ulong nif, Address address, string email)
        : base(firstName, lastName, gender, dateOfBirth, nif, address, email) { }

    private protected override Task InsertUser(Person user)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogIn() { throw new NotImplementedException(); }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    LoginStatus ILogin.LogOut() { throw new NotImplementedException(); }
}
