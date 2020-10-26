using System;
using System.Collections.Generic;
using System.Text;
using Kavenegar;

namespace Authentication.Core.Classes
{
 public   class SMS
    {

        public void Send(string To,string Body)
        {
            var sender = "";

            var receptor = To;
            var message = Body;

            var api = new KavenegarApi("");
            api.Send(sender, receptor, message);
        }


        
    }
}
