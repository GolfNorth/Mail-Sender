using System.Linq;

namespace MailSender.Library.Service
{
    /// <summary>
    ///     Статический класс кодирования и декодирования пароля
    /// </summary>
    public static class PasswordEncoder
    {
        /// <summary>
        ///     Кодирование пароля
        /// </summary>
        /// <param name="input">Источник</param>
        /// <param name="key">Ключ кодирования</param>
        /// <returns></returns>
        public static string Encode(this string input, int key = 1)
        {
            return new string(input.Select(c => (char) (c + key)).ToArray());
        }

        /// <summary>
        ///     Декодирование пароля
        /// </summary>
        /// <param name="input">Источник</param>
        /// <param name="key">Ключ декодирования</param>
        /// <returns></returns>
        public static string Decode(this string input, int key = 1)
        {
            return new string(input.Select(c => (char) (c - key)).ToArray());
        }
    }
}