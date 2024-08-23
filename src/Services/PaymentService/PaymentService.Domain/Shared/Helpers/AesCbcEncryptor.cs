using System.Security.Cryptography;
using System.Text;

namespace PaymentService.Domain.Shared.Helpers
{
	/// <summary>
	/// A service for performing symmetric encryption and decryption method based on AES (Advanced Encryption Standard) algorithm with CBC (Cipher Block Chaining) mode.
	/// </summary>
	public static class AesCbcEncryptor
	{
		private static readonly string AESKEY = GenerateRandomString(32);

		/// <summary>
		/// Encrypts a plain text using the default key.
		/// </summary>
		/// <param name="plainText">The plain text to be encrypted.</param>
		/// <returns>The base64-encoded encrypted text.</returns>
		public static string Encrypt(string plainText)
		{
			return Convert.ToBase64String(Encrypt(ToByte(plainText)));
		}

		/// <summary>
		/// Encrypts a plain text using a specific key.
		/// </summary>
		/// <param name="plainText">The plain text to be encrypted.</param>
		/// <param name="key">The encryption key. WARNING Key length must not be less than 1 and greater than 32.</param>
		/// <returns>The base64-encoded encrypted text.</returns>
		/// <exception cref="ArgumentException">Thrown when <paramref name="plainBytes"/> length is less than 1 or greater than 32.</exception>
		public static string Encrypt(string plainText, string key)
		{
			return Convert.ToBase64String(Encrypt(ToByte(plainText), key));
		}

		/// <summary>
		/// Encrypts a byte array using the default key.
		/// </summary>
		/// <param name="plainBytes">The byte array to be encrypted.</param>
		/// <returns>The encrypted byte array.</returns>
		public static byte[] Encrypt(byte[] plainBytes)
		{
			return Encrypt(plainBytes, AESKEY);
		}

		/// <summary>
		/// Encrypts a byte array using a specific key.
		/// </summary>
		/// <param name="plainBytes">The byte array to be encrypted.</param>
		/// <param name="key">The encryption key. WARNING Key length must not be less than 1 and greater than 32.</param>
		/// <returns>The encrypted byte array.</returns>
		/// <exception cref="ArgumentException">Thrown when <paramref name="plainBytes"/> length is less than 1 or greater than 32.</exception>
		public static byte[] Encrypt(byte[] plainBytes, string key)
		{
			if ((key?.Length > 0 && key?.Length <= 32) == false)
			{
				throw new ArgumentException($"The '{nameof(key)}' must have length greater that 0 and less or equal to 32.");
			}

			byte[] keyBytes = Encoding.UTF8.GetBytes(key);

			using var aesAlg = Aes.Create();
			aesAlg.Key = keyBytes;
			aesAlg.Mode = CipherMode.CBC;

			// Generate an initialization vector (IV)
			aesAlg.GenerateIV();

			// Encrypt the data
			using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
			byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
			byte[] combinedBytes = new byte[aesAlg.IV.Length + encryptedBytes.Length];
			Array.Copy(aesAlg.IV, combinedBytes, aesAlg.IV.Length);
			Array.Copy(encryptedBytes, 0, combinedBytes, aesAlg.IV.Length, encryptedBytes.Length);
			return combinedBytes;
		}

		/// <summary>
		/// Decrypts an encrypted text using the default key.
		/// </summary>
		/// <param name="encryptedText">The base64-encoded encrypted text.</param>
		/// <returns>The decrypted plain text.</returns>
		public static string Decrypt(string encryptedText)
		{
			return ToString(Decrypt(Convert.FromBase64String(encryptedText), AESKEY));
		}

		/// <summary>
		/// Decrypts an encrypted text using a specific key.
		/// </summary>
		/// <param name="encryptedText">The base64-encoded encrypted text.</param>
		/// <param name="key">The encryption key. WARNING Key length must not be less than 1 and greater than 32.</param>
		/// <returns>The decrypted plain text.</returns>
		/// <exception cref="ArgumentException">Thrown when <paramref name="plainBytes"/> length is less than 1 or greater than 32.</exception>
		public static string Decrypt(string encryptedText, string key)
		{
			return ToString(Decrypt(Convert.FromBase64String(encryptedText), key));
		}

		/// <summary>
		/// Decrypts an encrypted byte array using the default key.
		/// </summary>
		/// <param name="combinedBytes">The encrypted byte array.</param>
		/// <returns>The decrypted byte array.</returns>
		public static byte[] Decrypt(byte[] combinedBytes)
		{
			return Decrypt(combinedBytes, AESKEY);
		}

		/// <summary>
		/// Decrypts an encrypted byte array using a specific key.
		/// </summary>
		/// <param name="combinedBytes">The encrypted byte array.</param>
		/// <param name="key">The encryption key. WARNING Key length must not be less than 1 and greater than 32.</param>
		/// <returns>The decrypted byte array.</returns>
		/// <exception cref="ArgumentException">Thrown when <paramref name="plainBytes"/> length is less than 1 or greater than 32.</exception>
		public static byte[] Decrypt(byte[] combinedBytes, string key)
		{
			if ((key?.Length > 0 && key?.Length <= 32) == false)
			{
				throw new ArgumentException($"The '{nameof(key)}' must have length greater that 0 and less or equal to 32.");
			}

			byte[] keyBytes = Encoding.UTF8.GetBytes(key);

			using var aesAlg = Aes.Create();
			aesAlg.Key = keyBytes;
			aesAlg.Mode = CipherMode.CBC;

			// Extract the IV from the combined bytes
			byte[] iv = new byte[aesAlg.BlockSize / 8];
			Array.Copy(combinedBytes, iv, iv.Length);

			// Extract the encrypted data from the combined bytes
			byte[] encryptedBytes = new byte[combinedBytes.Length - iv.Length];
			Array.Copy(combinedBytes, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

			// Decrypt the data
			using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, iv);
			byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
			return decryptedBytes;
		}

		/// <summary>
		/// Converts a byte array to a string using UTF-8 encoding.
		/// </summary>
		/// <param name="decrContent">The byte array to be converted.</param>
		/// <returns>The resulting string.</returns>
		public static string ToString(byte[] decrContent)
		{
			return Encoding.UTF8.GetString(decrContent);
		}

		/// <summary>
		/// Converts a string to a byte array using UTF-8 encoding.
		/// </summary>
		/// <param name="text">The string to be converted.</param>
		/// <returns>The resulting byte array.</returns>
		public static byte[] ToByte(string text)
		{
			return Encoding.UTF8.GetBytes(text);
		}

		/// <summary>
		/// Retrieves the AES key used for encryption and decryption.
		/// </summary>
		/// <returns>The AES encryption key.</returns>
		public static string GetKey()
		{
			return AESKEY;
		}

		/// <summary>
		/// Generates a random string with a specified length and characters.
		/// </summary>
		/// <param name="length">The length of the random string to generate.</param>
		/// <returns>The generated random string.</returns>
		public static string GenerateRandomString(int length)
		{
			const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-\n_\t=+[]{}|;:'\"\\,.<>?/ `~";

			byte[] randomBytes = new byte[length];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomBytes);
			}

			StringBuilder result = new(length);
			foreach (byte b in randomBytes)
			{
				result.Append(chars[b % chars.Length]);
			}

			return result.ToString();
		}
	}
}
