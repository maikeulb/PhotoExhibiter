﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoExhibiter.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync (string email, string subject, string message) => Task.CompletedTask;
    }
}