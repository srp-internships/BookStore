using System.Security.Cryptography;
using System.Text;

namespace PaymentService.Domain.Shared.Helpers
{
	// source https://ru.stackoverflow.com/questions/1035394/%D0%A8%D0%B8%D1%84%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D1%81-%D0%BE%D1%82%D0%BA%D1%80%D1%8B%D1%82%D1%8B%D0%BC-%D0%BA%D0%BB%D1%8E%D1%87%D0%BE%D0%BC-%D0%BD%D0%B0%D0%BE%D0%B1%D0%BE%D1%80%D0%BE%D1%82-c-%D0%B8-php

	/// <summary>
	/// Класс шифровки и дешифровки с использованием асимметричного метода шифрования (RSA - Rivest–Shamir–Adleman). Класс создает публичный и частный ключи используемые для шифровки и дешифровки. А также поддерживает шифрование на основе стороннего публичного ключа.
	/// </summary>
	public static class RSAEncryptor
	{
		private enum KeySizes
		{
			Size512 = 512,
			Size1024 = 1024,
			Size2048 = 2048,
			Size952 = 952,
			Size136 = 136
		};

		/// <summary>
		/// Частный ключ для дешифрования
		/// </summary>
		private static string PrivateKey { get; set; }

		/// <summary>
		/// Публичный код для шифрования
		/// </summary>
		private static string PublicKey { get; set; }

		static RSAEncryptor()
		{
			GenerateKeys();
		}

		/// <summary>
		/// Метод генерирует два ключа публичный и частный и сохраняет их в памяти
		/// </summary>
		private static void GenerateKeys()
		{
			using var rsa = new RSACryptoServiceProvider((int)KeySizes.Size2048);
			rsa.PersistKeyInCsp = false;
			PublicKey = rsa.ToXmlString(false);

			PrivateKey = rsa.ToXmlString(true);
		}

		/// <summary>
		/// Метод шифрует строку используя публичный ключ
		/// </summary>
		/// <param name="text">Текст для шифрования</param>
		/// <returns>Зашифрованный текст</returns>
		public static string Encrypt(string text)
		{
			byte[] encContent = Encrypt(ToByte(text));
			return Convert.ToBase64String(encContent);
		}

		/// <summary>
		/// Метод шифрует строку используя переданный публичный ключ
		/// </summary>
		/// <param name="text">Текст для шифрования</param>
		/// <param name="publicKey">Сторонний публичный ключ</param>
		/// <returns>Зашифрованный текст</returns>
		public static string Encrypt(string text, string publicKey)
		{
			byte[] encContent = Encrypt(ToByte(text), publicKey);
			return Convert.ToBase64String(encContent);
		}

		/// <summary>
		/// Метод дешифрует строку используя частный ключ
		/// </summary>
		/// <param name="text">Текст для дешифрования</param>
		/// <returns>Дешифрованный текст</returns>
		/// <exception cref="ArgumentException">Выбрасывается когда <paramref name="text"/> не валидный текст, который не может быть дешифрован.</exception>
		public static string Decrypt(string text)
		{
			try
			{
				byte[] decrContent = Decrypt(Convert.FromBase64String(text));
				return ToString(decrContent);
			}
			catch (Exception ex)
			{
				throw new ArgumentException($"Invalid encrypted text.", nameof(text), ex);
			}
		}

		/// <summary>
		/// Метод конвертирует байты в текст используя кодировку UTF8
		/// </summary>
		/// <param name="decrContent">Массив байтов</param>
		/// <returns>Текст в кодировке UTF8</returns>
		public static string ToString(byte[] decrContent)
		{
			return Encoding.UTF8.GetString(decrContent);
		}

		/// <summary>
		/// Метод конвертирует текст в байты сохраняя кодировку UTF8
		/// </summary>
		/// <param name="text">Текст</param>
		/// <returns>Массив байтов</returns>
		public static byte[] ToByte(string text)
		{
			return Encoding.UTF8.GetBytes(text);
		}

		/// <summary>
		/// Возвращает публичный ключ
		/// </summary>
		/// <returns>Публичный ключ</returns>
		public static string GetPublicKey()
		{
			return PublicKey;
		}

		/// <summary>
		/// Метод шифрует байты используя публичный ключ
		/// </summary>
		/// <param name="data">Байты для шифрования</param>
		/// <returns>Зашифрованные байты</returns>
		public static byte[] Encrypt(byte[] data)
		{
			return Encrypt(data, PublicKey);
		}

		/// <summary>
		/// Метод шифрует байты используя переданный публичный ключ
		/// </summary>
		/// <param name="data">Байты для шифрования</param>
		/// <param name="publicKey">Сторонний публичный ключ</param>
		/// <returns>Зашифрованные байты</returns>
		public static byte[] Encrypt(byte[] data, string publicKey)
		{
			RSACryptoServiceProvider rsa = new();
			rsa.FromXmlString(publicKey);
			return rsa.Encrypt(data, true);
		}

		/// <summary>
		/// Метод дешифрует байты используя частный ключ
		/// </summary>
		/// <param name="text">Байты для дешифрования</param>
		/// <returns>Дешифрованные байты</returns>
		public static byte[] Decrypt(byte[] cipherdata)
		{
			RSACryptoServiceProvider rsa = new();
			rsa.FromXmlString(PrivateKey);
			return rsa.Decrypt(cipherdata, true);
		}
	}
}
