﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRChartServer.Hubs
{
    public class SatisHub : Hub
    {
        public async Task SendMassageAsync()
        {
            await Clients.All.SendAsync("receiveMessage", "Merhaba Babalar.");
        }
    }
}
