using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateBallData()
        {
            return new BallData();
        }
        
    }
    internal class BallData : DataAbstractApi
    {
        //Nothing
    }

}
