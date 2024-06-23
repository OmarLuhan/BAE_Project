using Microsoft.AspNetCore.Mvc;

namespace BAE_WEB.Utils.CustomFilter
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string controller, string action) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { controller, action };
        }
    }
}