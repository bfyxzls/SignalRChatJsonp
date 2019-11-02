using System.Collections.Generic;
using System;
using SignalRChatForMVC.Model;

namespace SignalRChatForMVC.Utils
{
    public class TokenContext
    {
        static Dictionary<string, UserDetail> TokenList = new Dictionary<string, UserDetail>();
        public static void SetToken(string key, UserDetail userDetail)
        {
            TokenList.Add(key, userDetail);
        }

        public static UserDetail GetUserByToken(string key)
        {
            if (!TokenList.ContainsKey(key))
                throw new ArgumentException("没有找到这个token");
            return TokenList[key];
     
        }
    }
}