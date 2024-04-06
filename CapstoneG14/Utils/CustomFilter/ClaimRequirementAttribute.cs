
using Microsoft.AspNetCore.Mvc;

namespace CapstoneG14.Utilities.CustomFilter
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string controller, string action) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { controller, action };
        }
    }
}