
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Api;
using IntelliTect.Coalesce.Api.Controllers;
using IntelliTect.Coalesce.Api.DataSources;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Mapping.IncludeTrees;
using IntelliTect.Coalesce.Models;
using IntelliTect.Coalesce.TypeDefinition;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialSite.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialSite.Web.Api
{
    [Route("api/User")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class UserController
        : BaseApiController<SocialSite.Data.Models.User, UserDtoGen, SocialSite.Data.AppDbContext>
    {
        public UserController(SocialSite.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<SocialSite.Data.Models.User>();
        }

        // Methods from data class exposed through API Controller.

        /// <summary>
        /// Method: GetUsersOnPage
        /// </summary>
        [HttpPost("GetUsersOnPage")]
        [AllowAnonymous]
        public virtual async Task<ItemResult<ICollection<UserDtoGen>>> GetUsersOnPage([FromServices] SocialSite.Data.AppDbContext db, ICollection<string> screenNames)
        {
            IncludeTree includeTree = null;
            var _mappingContext = new MappingContext(User);
            var _methodResult = await SocialSite.Data.Models.User.GetUsersOnPage(db, screenNames.ToArray());
            var _result = new ItemResult<ICollection<UserDtoGen>>();
            _result.Object = _methodResult?.ToList().Select(o => Mapper.MapToDto<SocialSite.Data.Models.User, UserDtoGen>(o, _mappingContext, includeTree)).ToList();
            return _result;
        }
    }
}
