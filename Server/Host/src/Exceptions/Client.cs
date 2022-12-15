using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host.Exceptions;


internal class ClientException : Exception
{
    internal ClientException(string message) : base(message)
    {
        
    }
}

internal class InvalidClientDataException : ClientException
{
    internal InvalidClientDataException(string message) : base(message)
    {
        
    }
}
