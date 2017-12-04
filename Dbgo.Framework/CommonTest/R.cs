using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest
{
    public class R
    {
        public int CreatNum(int min, int max)
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            int iSeed = BitConverter.ToInt32(buffer, 0);
            Random ran = new Random(iSeed);
            int RandKey = ran.Next(min, max);
            return RandKey;
        }
        
    }
}
