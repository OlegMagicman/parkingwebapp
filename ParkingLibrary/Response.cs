using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLibrary
{
    public class Response
    {
        public int Status { get; set; }
        public object Data { get; set; }

        public Response(int Status, object Data)
        {
            this.Status = Status;
            this.Data = Data;
        }
    }
}
