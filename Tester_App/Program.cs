﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester_App
{
    class Program
    {
        static void Main(string[] args)
        {
            MyLoggerLib.ILogger logger = MyLoggerLib.LoggerFactory.GetLogger(1);
            logger.Log("yahoo");
        }
    }
}
