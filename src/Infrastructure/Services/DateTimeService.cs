using PersonalManager.Application.Common.Interfaces;
using System;

namespace PersonalManager.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
