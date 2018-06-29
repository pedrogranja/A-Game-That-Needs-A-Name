using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace ItemTransportation
{
    public class ItemTransportationException : System.Exception
    {

        public ItemTransportationException()
       : base()
        {
        }

        public ItemTransportationException(String message)
          : base(message)
        {
        }

        public ItemTransportationException(String message, Exception innerException)
          : base(message, innerException)
        {
        }


        protected ItemTransportationException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
