namespace System.Net
{
    /// <summary>
    /// Stellt ein SecureOn-Passwort bereit.
    /// </summary>
    [Serializable]
    public class SecureOnPassword
    {
        /// <summary>Die Passwortdaten des SecureOn-Passworts.</summary>
        public byte[] Password { get; private set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der System.Net.SecureOnPassword-Klasse mit dem angegebenen Passwort.
        /// </summary>
        /// <param name="password">Das Passwort als System.Byte-Array.</param>
        /// <exception cref="System.ArgumentNullException">password ist null.</exception>
        /// <exception cref="System.ArgumentException">Das System.Byte-Array password hat eine Länge ungleich 6.</exception>
        public SecureOnPassword(byte[] password)
        {
            if (password == null)
                throw new ArgumentNullException("password");
            if (password.Length != 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidPasswordLength);
            Password = password;
        }

        /// <summary>
        /// Initialisiert eine neue Instanz der System.Net.SecureOnPassword-Klasse mit dem angegebenen Passwort.
        /// </summary>
        /// <param name="password">Das Passwort als Zeichenfolge.</param>
        /// <remarks >Verwendet System.Text.Encoding.Default als Kodierung.</remarks>
        public SecureOnPassword(string password)
            : this(password, Text.Encoding.Default)
        { }


        /// <summary>
        /// Initialisiert eine neue Instanz der System.Net.SecureOnPassword-Klasse mit dem angegebenen Passwort.
        /// </summary>
        /// <param name="password">Das Passwort als Zeichenfolge.</param>
        /// <param name="encoding">Die System.Text.Encoding-Instanz für das Passwort.</param>
        /// <exception cref="System.ArgumentNullException">encoding ist null.</exception>
        /// <exception cref="System.ArgumentException">Das System.Byte-Array, welches aus dem Passwort resultiert, hat eine Länge größer 6.</exception>
        public SecureOnPassword(string password, Text.Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (password == null)
                throw new ArgumentNullException("password");

            if (string.IsNullOrEmpty(password))
            {
                Password = new byte[6];
            }
            var bytes = encoding.GetBytes(password);
            if (bytes.Length > 6)
                throw new ArgumentException(Localization.ArgumentExceptionInvalidPasswordLength);

            Password = new byte[6];
            for (int i = 0; i < bytes.Length; i++)
                Password[i] = bytes[i];
            if (bytes.Length < 6)
            {
                for (int i = bytes.Length - 1; i < 6; i++)
                    Password[i] = 0x00;
            }
        }

        /// <summary>
        /// Konvertiert SecureOn-Passwörter in die Strichnotation.
        /// </summary>
        /// <returns>Eine Zeichenfolge mit einem SecureOn-Passwort in Strichnotation.</returns>
        public override string ToString()
        {
            var f = new string[6];
            for (int i = 0; i < f.Length; i++)
                f[i] = Password[i].ToString("X2");
            return string.Join("-", f);
        }

    }
}
