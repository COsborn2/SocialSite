
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
    [Route("api/Message")]
    [Authorize]
    [ServiceFilter(typeof(IApiActionFilter))]
    public partial class MessageController
        : BaseApiController<SocialSite.Data.Models.Message, MessageDtoGen, SocialSite.Data.AppDbContext>
    {
        public MessageController(SocialSite.Data.AppDbContext db) : base(db)
        {
            GeneratedForClassViewModel = ReflectionRepository.Global.GetClassViewModel<SocialSite.Data.Models.Message>();
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public virtual Task<ItemResult<MessageDtoGen>> Get(
            int id,
            DataSourceParameters parameters,
            IDataSource<SocialSite.Data.Models.Message> dataSource)
            => GetImplementation(id, parameters, dataSource);

        [HttpGet("list")]
        [Authorize]
        public virtual Task<ListResult<MessageDtoGen>> List(
            ListParameters parameters,
            IDataSource<SocialSite.Data.Models.Message> dataSource)
            => ListImplementation(parameters, dataSource);

        [HttpGet("count")]
        [Authorize]
        public virtual Task<ItemResult<int>> Count(
            FilterParameters parameters,
            IDataSource<SocialSite.Data.Models.Message> dataSource)
            => CountImplementation(parameters, dataSource);

        [HttpPost("save")]
        [Authorize]
        public virtual Task<ItemResult<MessageDtoGen>> Save(
            MessageDtoGen dto,
            [FromQuery] DataSourceParameters parameters,
            IDataSource<SocialSite.Data.Models.Message> dataSource,
            IBehaviors<SocialSite.Data.Models.Message> behaviors)
            => SaveImplementation(dto, parameters, dataSource, behaviors);

        [HttpPost("delete/{id}")]
        [Authorize]
        public virtual Task<ItemResult<MessageDtoGen>> Delete(
            int id,
            IBehaviors<SocialSite.Data.Models.Message> behaviors,
            IDataSource<SocialSite.Data.Models.Message> dataSource)
            => DeleteImplementation(id, new DataSourceParameters(), dataSource, behaviors);
    }
}
