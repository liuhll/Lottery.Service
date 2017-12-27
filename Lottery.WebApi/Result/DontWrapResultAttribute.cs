using System;

namespace Lottery.WebApi.Result
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class DontWrapResultAttribute : WrapResultAttribute
    {
        /// </summary>
        public DontWrapResultAttribute()
            : base(false, false)
        {

        }
    }
}