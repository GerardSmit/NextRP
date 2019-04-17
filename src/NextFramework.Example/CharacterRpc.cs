using System;
using System.Collections.Generic;
using System.Text;
using NextFramework.Attributes;
using NextFramework.Rpc;
using NextFramework.Scripting;

namespace NextFramework.Example
{
    public class CharacterRpc : IRpcListener
    {
        [RpcCommand("login")]
        public string Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                return "Very secure my friend, but welcome!";
            }

            return "Invalid username and password!";
        }
    }
}
