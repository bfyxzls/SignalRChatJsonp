﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChatForMVC.Model
{
    public class UserDetail
    {

        /// <summary>
        /// 连接ＩＤ
        /// </summary>
        public string ConnectionId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// 消息总数
        /// </summary>
        public int MessageCount { get; set; }
        /// <summary>
        /// 当前标识
        /// </summary>
        public string Token { get; set; }
    }


    public class CurrentUser
    {
        public CurrentUser()
        {
            MessageList = new List<Message>();
        }
        public string ConnectionId { get; set; }
        public string UserID { get; set; }
        /// <summary>
        /// 消息总数
        /// </summary>
        public int MessageCount { get; set; }
        /// <summary>
        ///用户的消息
        /// </summary>
        public ICollection<Message> MessageList { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
    }

    public class Message
    {
        public string ID { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Info { get; set; }
        public int IsNew { get; set; }
    }
}