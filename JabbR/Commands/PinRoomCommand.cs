﻿using JabbR.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JabbR.Commands
{
    [Command("pinroom", "PinRoom_CommandInfo", "room [priority]", "admin")]
    public class PinRoomCommand : AdminCommand
    {
        public override void ExecuteAdminOperation(CommandContext context, CallerContext callerContext, ChatUser callingUser, string[] args)
        {   
            string roomName = args.Length > 0 ? args[0] : callerContext.RoomName;             

            if (String.IsNullOrEmpty(roomName))
            {
                throw new HubException(LanguageResources.PinRoom_RoomRequired);
            }                      

            ChatRoom room = context.Repository.VerifyRoom(roomName, mustBeOpen: false);
            
            // set the pin priority
            int pinnedPriority = 0;
            if (args.Length > 1)
            {
                pinnedPriority = Convert.ToInt32(String.Join(" ", args.Skip(1)).Trim());
            }                       

            context.Service.PinRoom(callingUser, room, pinnedPriority);
        }
    }
}