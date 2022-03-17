/*-
 * Copyright (c) 2020, 2022 Oracle and/or its affiliates. All rights reserved.
 *
 * Licensed under the Universal Permissive License v 1.0 as shown at
 *  https://oss.oracle.com/licenses/upl/
 */

namespace Oracle.NoSQL.SDK.BinaryProtocol
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using static Protocol;

    internal partial class RequestSerializer : IRequestSerializer
    {
        private const short DefaultSerialVersion = V3;

        private volatile short serialVersion = DefaultSerialVersion;

        private static long GetUnixMillisOrZero(DateTime? dateTime)
        {
            return dateTime.HasValue ? DateTimeUtils.GetUnixMillis(
                dateTime.Value) : 0;
        }

        private void WriteOpcode(MemoryStream stream, Opcode opcode) =>
            Protocol.WriteOpcode(stream, opcode, serialVersion);

        public short SerialVersion => serialVersion;

        public bool DecrementSerialVersion()
        {
            if (serialVersion == V3) {
                serialVersion = V2;
                return true;
            }

            return false;
        }

        public void ReadAndCheckError(MemoryStream stream)
        {
            var statusCode = ReadByte(stream);
            if (statusCode != 0)
            {
                var message = ReadString(stream);
                throw MapException((ErrorCode)statusCode, message);
            }
        }

        public string ContentType => "application/octet-stream";

    }

}
