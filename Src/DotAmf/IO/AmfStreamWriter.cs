﻿// Copyright (c) 2012 Artem Abashev (http://abashev.me)
// All rights reserved.
// Licensed under the Microsoft Public License (Ms-PL)
// http://opensource.org/licenses/ms-pl.html

using System;
using System.IO;
using System.Text;

namespace DotAmf.IO
{
    /// <summary>
    /// AMF stream writer.
    /// </summary>
    sealed internal class AmfStreamWriter : BinaryWriter
    {
        #region .ctor
        public AmfStreamWriter(Stream stream)
            : base(stream)
        {
        }

        public AmfStreamWriter(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
        }
        #endregion

        #region Data
        /// <summary>
        /// Delegate to use for preparing bytes before writing.
        /// </summary>
        /// <remarks>
        /// AMF messages have a big endian (network) byte order.
        /// </remarks>
        private static readonly PrepareBytes PrepareBytes = ByteConverter.IsLittleEndian ?
                                             (PrepareBytes) ByteConverter.ChangeEndianness :
                                                            ByteConverter.KeepEndianness;
        #endregion

        #region Public methods
        public override void Write(byte value) { base.Write(value); }

        public override void Write(sbyte value) { throw new NotSupportedException(); }

        public override void Write(short value) { Write(PrepareBytes(BitConverter.GetBytes(value))); }

        public override void Write(ushort value) { Write(PrepareBytes(BitConverter.GetBytes(value))); }

        public override void Write(int value) { Write(PrepareBytes(BitConverter.GetBytes(value))); }

        public override void Write(uint value) { Write(PrepareBytes(BitConverter.GetBytes(value))); }

        public override void Write(long value) { throw new NotSupportedException(); }

        public override void Write(ulong value) { throw new NotSupportedException(); }

        public override void Write(float value) { throw new NotSupportedException(); }

        public override void Write(double value) { Write(PrepareBytes(BitConverter.GetBytes(value))); }

        public override void Write(string value) { throw new NotSupportedException(); }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// Disposes writer, but leaves the underlying stream open.
        /// </summary>
        public new void Dispose()
        {
            BaseStream.Flush();
        }
        #endregion
    }
}
