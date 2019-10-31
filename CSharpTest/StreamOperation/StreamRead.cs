using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharpTest.StreamOperation
{
    public class StreamRead
    {
        public static byte[] Read(Stream stream)
        {
            long size = stream.Length;
            byte[] buf = new byte[] { };

            try
            {
                if (size < int.MaxValue)
                {
                    buf = new byte[size];
                    stream.Read(buf, 0, (int)size);
                }
                else
                {
                    int batchSize = int.MaxValue;
                    while (size >= batchSize)
                    {
                        byte[] temp = new byte[batchSize];
                        stream.Read(temp, 0, batchSize);
                        buf = buf.Concat(temp).ToArray();
                        size -= batchSize;
                    }
                }
            }
            catch(OutOfMemoryException e)
            {
                Console.WriteLine($@"Current remaining stream size : {size},
                                     Error message : {e.Message},
                                     Error stack trace : {e.StackTrace}");
            }

            return buf;
        }
    }
}
