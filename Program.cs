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
        // two member variables, TW records to object to write, startingLineNumber records the current Line number
        TextWriter TW;
        int startingLineNumber;
        
        // constructor
        public NumberedTextWriter(TextWriter A)
        {
            TW = A;
            startingLineNumber = 1;
        }

        // constructor with both writer object and starting line index
        public NumberedTextWriter(TextWriter A, int x)
        {
            TW = A;
            startingLineNumber = x;
        }

        // Encoding property returns the writer object's encoding
        public override Encoding Encoding
        {
            get
            {
                return TW.Encoding;
            }
        }

        // add line number and colon, space into string when calling writeline function
        public override void WriteLine(string value)
        {
            TW.WriteLine(startingLineNumber + ": " + value);
            startingLineNumber++;
        }
    }

    public class IndexedNumsStream : System.IO.Stream
    {
        // two member variables: position in stream and the length, both in long type
        long position;
        long length;

        // constructor with setting length and position
        public IndexedNumsStream(long x)
        {
            length = (x < 0 ? 0 : x);
            position = 0;
        }

        // Read function
        public override int Read(byte[] buffer, int offset, int count)
        {
            // rest is actually the number of bytes that we can read
            long rest = length - position;
            rest = rest < count ? rest : count;
            for (int i = 0; i < rest; i++)
            {
                // read to the buffer with position mod 256
                buffer[i + offset] = (byte)((position) % 256);
                if (position < length)
                    position++;
                if (position < 0)
                    position = 0;
            }
            return (int) rest;
        }

        // set the length
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
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return length; }
        }

        // position property, limited range from 0 to length
        public override long Position
        {
            get { return position; }

            set
            {
                position = value;
                if (value < 0) position = 0;
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
