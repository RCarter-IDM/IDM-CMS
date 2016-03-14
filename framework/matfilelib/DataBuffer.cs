using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace matfilelib
{
    abstract class DataBuffer<T> : IElement
    {
        protected readonly IEnumerable<T> Data;
        private readonly MatlabType _matlabType;
        private readonly uint _dataElementSize;

        protected DataBuffer(IEnumerable<T> data, MatlabType matlabType, uint dataElementSize)
        {
            Data = data;
            _matlabType = matlabType;
            _dataElementSize = dataElementSize;
        }

        public void WriteToStream(BinaryWriter binaryWriter)
        {
            binaryWriter.Write((uint)_matlabType);
            binaryWriter.Write((uint) (Data.Count()*_dataElementSize));
            uint count = WriteDataElements(binaryWriter);
            while ((count & 7) != 0)
            {
                binaryWriter.Write('\0');
                count++;
            }
        }

        protected virtual uint WriteDataElements(BinaryWriter binaryWriter)
        {
            throw new NotImplementedException();
        }

        public uint Size
        {
            get
            {
                var size = (uint)(8 + (((Data.Count() * _dataElementSize) + 7) & ~7));

                return size;
            }
        }
    }
}