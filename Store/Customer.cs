namespace Store
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Покупатель.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Адрес электронной почты.
        /// </summary>
        [Key]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }
    }
}