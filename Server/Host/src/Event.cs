namespace Host.Event;

#region enum types

internal enum Session
{
    DidNotAttend,
    RegisteredEntry,
    UnAuthorized,
}

/// <summary>
///     A client can make reservations.
///     This enumerator defines all the types the event could be.
/// </summary>
internal enum Type
{
    /// <summary> A clients's reqwest to an exterior space. </summary>
    ExteriorSpaceReqwest,
    /// <summary>
    ///     A clients's reqwest to a session with a personal trainer.
    /// </summary>
    PersonalTrainerReqwest,
    /// <summary> A clients's reqwest to a free training session. </summary>
    FreeTrainingReqwest,
}

/// <summary>
///     A client can make reservations.
///     This enumerator defines all the types the event could be.
/// </summary>
internal enum ReservationStatus
{
    /// <summary> Reservation is awaiting aproval by a trainer. </summary>
    AwaitingAproval,
    /// <summary> Reservation is a related payment. </summary>
    AwaitingPayment,
    /// <summary> Reservation is a unavailable at the moment. </summary>
    Unavailable,
    /// <summary> Reservation was canceled. </summary>
    Canceled,
    /// <summary> Reservation is aproved. </summary>
    Aproved,
}

#endregion

#region attributes

/// <summary>
///     A client can make reservations.
/// </summary>
internal struct Reservation
{
    /// <summary>
    ///     Start event Date
    /// </summary>
    internal DateTime StartDate { get; set; }

    /// <summary>
    ///     End event Data
    /// </summary>
    internal DateTime EndDate { get; set; }

    /// <summary>
    ///     Comments
    /// </summary>
    internal string? Comments { get; set; }
}

#endregion
