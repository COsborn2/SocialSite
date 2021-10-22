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
    public partial class MessageDtoGen : GeneratedDto<SocialSite.Data.Models.Message>
    {
        public MessageDtoGen() { }

        private int? _MessageId;
        private string _OriginalId;
        private string _Text;
        private string _ScreenName;
        private System.DateTimeOffset? _Utc;
        private System.DateTimeOffset? _CreatedAt;
        private int? _Favorites;
        private int? _Shares;

        public int? MessageId
        {
            get => _MessageId;
            set { _MessageId = value; Changed(nameof(MessageId)); }
        }
        public string OriginalId
        {
            get => _OriginalId;
            set { _OriginalId = value; Changed(nameof(OriginalId)); }
        }
        public string Text
        {
            get => _Text;
            set { _Text = value; Changed(nameof(Text)); }
        }
        public string ScreenName
        {
            get => _ScreenName;
            set { _ScreenName = value; Changed(nameof(ScreenName)); }
        }
        public System.DateTimeOffset? Utc
        {
            get => _Utc;
            set { _Utc = value; Changed(nameof(Utc)); }
        }
        public System.DateTimeOffset? CreatedAt
        {
            get => _CreatedAt;
            set { _CreatedAt = value; Changed(nameof(CreatedAt)); }
        }
        public int? Favorites
        {
            get => _Favorites;
            set { _Favorites = value; Changed(nameof(Favorites)); }
        }
        public int? Shares
        {
            get => _Shares;
            set { _Shares = value; Changed(nameof(Shares)); }
        }

        /// <summary>
        /// Map from the domain object to the properties of the current DTO instance.
        /// </summary>
        public override void MapFrom(SocialSite.Data.Models.Message obj, IMappingContext context, IncludeTree tree = null)
        {
            if (obj == null) return;
            var includes = context.Includes;

            // Fill the properties of the object.

            this.MessageId = obj.MessageId;
            this.OriginalId = obj.OriginalId;
            this.Text = obj.Text;
            this.ScreenName = obj.ScreenName;
            this.Utc = obj.Utc;
            this.CreatedAt = obj.CreatedAt;
            this.Favorites = obj.Favorites;
            this.Shares = obj.Shares;
        }

        /// <summary>
        /// Map from the current DTO instance to the domain object.
        /// </summary>
        public override void MapTo(SocialSite.Data.Models.Message entity, IMappingContext context)
        {
            var includes = context.Includes;

            if (OnUpdate(entity, context)) return;

            if (ShouldMapTo(nameof(MessageId))) entity.MessageId = (MessageId ?? entity.MessageId);
            if (ShouldMapTo(nameof(OriginalId))) entity.OriginalId = OriginalId;
            if (ShouldMapTo(nameof(Text))) entity.Text = Text;
            if (ShouldMapTo(nameof(ScreenName))) entity.ScreenName = ScreenName;
            if (ShouldMapTo(nameof(Utc))) entity.Utc = (Utc ?? entity.Utc);
            if (ShouldMapTo(nameof(CreatedAt))) entity.CreatedAt = (CreatedAt ?? entity.CreatedAt);
            if (ShouldMapTo(nameof(Favorites))) entity.Favorites = (Favorites ?? entity.Favorites);
            if (ShouldMapTo(nameof(Shares))) entity.Shares = (Shares ?? entity.Shares);
        }
    }
}
