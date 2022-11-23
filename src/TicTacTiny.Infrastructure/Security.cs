using System.Text;
using jkdmyrs.TicTacTiny.Infrastructure.Exceptions;

namespace jkdmyrs.TicTacTiny.Infrastructure
{
	public static class Security
    {
        private readonly static Encoding _utf8 = Encoding.UTF8;
        private const int SALT_LENGTH = 10;
        private readonly static Random random = new Random();

        private static byte[] GenerateSalt()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return _utf8.GetBytes(new string(Enumerable.Repeat(chars, SALT_LENGTH)
                .Select(s => s[random.Next(s.Length)]).ToArray()));
        }

        public static byte[] SecurePassword(string rawpass)
        {
            return HashAndSalt(rawpass, GenerateSalt());
        }

		private static byte[] HashAndSalt(string rawpass, byte[] rawsalt, int? saltOffset = null)
        {
            if (!saltOffset.HasValue)
            {
                saltOffset = random.Next(9);
            }

            var hash = System.Security.Cryptography.SHA256.HashData(_utf8.GetBytes(rawpass).Concat(rawsalt).ToArray());

            var h0 = hash.Take(saltOffset.Value);
            var h1 = rawsalt;
            var h2 = hash.Skip(saltOffset.Value);
            var h3 = BitConverter.GetBytes(saltOffset.Value);
             
            return h0.Concat(h1).Concat(h2).Concat(h3).ToArray();
        }

        private static int GetOffset(byte[] securePass)
        {
            return BitConverter.ToInt32(securePass.Skip(Math.Max(0, securePass.Length - 4)).ToArray());
        }

        private static byte[] GetSalt(byte[] securePass)
        {
            var offset = GetOffset(securePass);
            return securePass.Skip(offset).Take(SALT_LENGTH).ToArray();
        }

        public static void VerifyPassword(string rawpass, byte[] securepass)
        {
            var calculated = HashAndSalt(rawpass, GetSalt(securepass), GetOffset(securepass));
            if(!securepass.SequenceEqual(calculated))
            {
                throw new InvalidPasswordException();
            }
        }
	}
}