using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS422
{
    

    public class NumberedTextWriter : System.IO.TextWriter
    {
        TextWriter TW;
        int startingLineNumber;
        public NumberedTextWriter(TextWriter A)
        {
            TW = A;
            startingLineNumber = 1;
        }
        public NumberedTextWriter(TextWriter A, int x)
        {
            TW = A;
            startingLineNumber = x;
        }
        public override Encoding Encoding
        {
            get
            {
                return TW.Encoding;
            }
        }
        public override void WriteLine(string value)
        {
            TW.WriteLine(startingLineNumber+": " +value);
            startingLineNumber++;
        }
    }

    public class IndexedNumsStream : System.IO.Stream
    {
        long position;
        long length;

        public IndexedNumsStream(long x)
        {
            length = (x < 0 ? 0 : x);
            position = 0;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                buffer[i + offset] = (byte)((position) % 256);
                if(position < length)
                    position++;
                if (position < 0)
                    position = 0;
            }
            return count;
        }

        public override void SetLength(long value)
        {
            length = (value < 0 ? 0 : value);
            if (position > length)
                position = length;
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true;}
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get{return length;}
        }

        public override long Position
        {
            get{ return position;}

            set
            {
                position = value;
                if (value < 0)position = 0;
                if (value > length) position = length;
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
