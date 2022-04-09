using Football_Manager.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager_Tests.TestHelpers
{
    internal static class StadiumHelper
    {
        public static Stadium GetMockStadium()
        {
            return new Stadium()
            {
                Name = "Test Stadium",
                Country = "South Africa",
                City = "Cape Town",
                Capacity = 550,
                Sport = "Soccer"
            };
        }
    }
}
