namespace Lottery.Dtos.Power
{
    public class PowerGrantInfo
    {
        /// <summary>
        /// Name of the permission.
        /// </summary>
        public string PowerCode { get; private set; }

        /// <summary>
        /// Is this permission granted Prohibited?
        /// </summary>
        public bool IsGranted { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="PowerGrantInfo"/>.
        /// </summary>
        /// <param name="powerCode"></param>
        /// <param name="isGranted"></param>
        public PowerGrantInfo(string powerCode, bool isGranted)
        {
            PowerCode = powerCode;
            IsGranted = isGranted;
        }
    }
}