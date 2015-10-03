using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
// Custom
using OAI.Packets;

namespace OAI.Tools
{
    public class OAIUtils
    {
        public const short ENDIAN_LENGTH = 3;
        public const short BYTE_SIZE = 256;

        public const byte LINEBREAK = 0x0D;
        public const byte BREAK = 0x00;

        public static byte[] LittleEndian(int length)
        {
            byte[] data = new byte[ENDIAN_LENGTH + 1];

            double endian = length / Math.Pow(BYTE_SIZE, ENDIAN_LENGTH);

            for (int i = 0; i <= ENDIAN_LENGTH; i++)
            {
                byte tmp = (byte)((ENDIAN_LENGTH == i) ?
                    Math.Round(endian) : Math.Floor(endian));
                endian -= tmp;
                endian *= BYTE_SIZE;
                data[ENDIAN_LENGTH - i] = tmp;
            }

            return data;
        }

        public static int BigEndian(byte[] data)
        {
            int length = 0;

            if (ENDIAN_LENGTH + 1 != data.Length)
            {
                return length;
            }

            for (int i = 0; i <= ENDIAN_LENGTH; i++)
            {
                if (0 == i)
                {
                    length += data[i];
                }
                else
                {
                    length += ((int)Math.Pow(BYTE_SIZE, i)) * data[i];
                }
            }

            return length;
        }

        public static byte[] PBXString(byte[] command, bool linebreak)
        {
            int length = command.Length;

            if (linebreak)
            {
                length += 1;
            }

            if (linebreak)
            {
                return Combine(LittleEndian(length),
                    command,
                    new byte[1] { LINEBREAK });
            }

            return Combine(LittleEndian(length),
                command);
        }

        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];

            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }

            return rv;
        }

        public static byte[] GetBytes(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static string GetString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /**
         * 
         * 
         */
        public static byte[] PBXConnection(byte type, string name, string password)
        {
            byte[] nameArr = GetBytes(name);
            byte[] passArr = GetBytes(password);

            byte[] data = Combine(new byte[1] { type }, passArr,
                new byte[1] { BREAK },
                nameArr,
                new byte[1] { BREAK });

            return PBXString(data, false);
        }

        /**
         * 
         * 
         */
        public static byte[] PBXPacket(OAICommand command)
        {
            byte[] message = Combine(GetBytes(command.PacketMessage()),
                new byte[1] { LINEBREAK });

            return PBXString(message, false);
        }

        /**
         * Most packet structures are CSV, but instead of quote encapsulation
         * of strings pipes are used instead. This method splits the packet
         * csv correctly which acknowledges the pipes.
         */
        public static string[] PBXPacketSplit(string packet)
        {
            // Packet is null, bail out!
            if (null == packet)
            {
                return new string[]{};
            }

            // No string identifier found (|) so split on raw CSV basis
            if (-1 == packet.IndexOf("|"))
            {
                return packet.Split(',');
            }

            // This is pretty heavy on resources and should only be
            // used where necessary!
            return Regex.Split(packet.Trim(), 
                ",(?=(?:[^\\|]*\\|[^\\|]*\\|)*(?![^\\|]*\\|))");
        }
    }
}
