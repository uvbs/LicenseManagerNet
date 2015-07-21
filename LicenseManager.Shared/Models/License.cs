using LicenseManager.Shared.Enums;

namespace LicenseManager.Shared.Models
{
    public class License
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the Software.
        /// </summary>
        public int SoftwareId { get; set; }

        /// <summary>
        /// Software edition.
        /// </summary>
        /// <example>Windows 7: "Professional"</example>
        public string Edition { get; set; }

        /// <summary>
        /// The license key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Language of the product.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Type of License, like volume license.
        /// </summary>
        public LicenseType Type { get; set; }

        /// <summary>
        /// Hostname(s) of the computer(s) the license is used on.
        /// </summary>
        public string UsedOn { get; set; }

        public virtual Software Software { get; set; }
    }
}
