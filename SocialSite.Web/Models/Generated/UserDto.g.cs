using IntelliTect.Coalesce;
using IntelliTect.Coalesce.Mapping;
using IntelliTect.Coalesce.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SocialSite.Web.Models
{
    public partial class UserDtoGen : GeneratedDto<SocialSite.Data.Models.User>
    {
        public UserDtoGen() { }

        private int? _UserId;
        private string _ScreenName;
        private string _ProfilePictureLink;

        public int? UserId
        {
            get => _UserId;
            set { _UserId = value; Changed(nameof(UserId)); }
        }
        public string ScreenName
        {
            get => _ScreenName;
            set { _ScreenName = value; Changed(nameof(ScreenName)); }
        }
        public string ProfilePictureLink
        {
            get => _ProfilePictureLink;
            set { _ProfilePictureLink = value; Changed(nameof(ProfilePictureLink)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(SocialSite.Data.Models.User obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.UserId = obj.UserId;
            this.ScreenName = obj.ScreenName;
            this.ProfilePictureLink = obj.ProfilePictureLink;
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(SocialSite.Data.Models.User entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(UserId))) entity.UserId = (UserId ?? entity.UserId);
            if (ShouldMapTo(nameof(ScreenName))) entity.ScreenName = ScreenName;
            if (ShouldMapTo(nameof(ProfilePictureLink))) entity.ProfilePictureLink = ProfilePictureLink;
        }
    }
}
