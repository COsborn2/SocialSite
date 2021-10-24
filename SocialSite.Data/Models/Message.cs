using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IntelliTect.Coalesce;
using IntelliTect.Coalesce.DataAnnotations;

namespace SocialSite.Data.Models
{
    [Read(SecurityPermissionLevels.AllowAll)]
    [Edit(SecurityPermissionLevels.DenyAll)]
    [Create(SecurityPermissionLevels.DenyAll)]
    [Delete(SecurityPermissionLevels.DenyAll)]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        public string OriginalId { get; set; }

        [Search]
        public string Text { get; set; }

        [InternalUse]
        public string Name { get; set; }

        public string ScreenName { get; set; }

        public DateTimeOffset Utc { get; set; }

        [DefaultOrderBy(OrderByDirection = DefaultOrderByAttribute.OrderByDirections.Descending)]
        public DateTimeOffset CreatedAt { get; set; }

        public int Favorites { get; set; }

        public int Shares { get; set; }

        [InternalUse]
        public string Language { get; set; }

        [InternalUse]
        public string Client { get; set; }

        [InternalUse]
        public string Type { get; set; }

        [InternalUse]
        public string Urls { get; set; }

        [InternalUse]
        public string Hashtags { get; set; }

        [InternalUse]
        public string Mentions { get; set; }

        [InternalUse]
        public string MediaType { get; set; }

        [InternalUse]
        public string MediaUrls { get; set; }

        [DefaultDataSource, Coalesce]
        public class MessageDefaultDataSource : StandardDataSource<Message, AppDbContext>
        {
            [Coalesce]
            public string ActiveChips { get; set; }

            public MessageDefaultDataSource(CrudContext<AppDbContext> context) : base(context)
            {
            }

            public override IQueryable<Message> ApplyListFiltering(IQueryable<Message> query, IFilterParameters parameters)
            {
                if (!string.IsNullOrWhiteSpace(ActiveChips))
                {
                    var activeChipsSeparated = ActiveChips.Split('*');
                    query = query.Where(x =>
                        activeChipsSeparated.Any(xi => x.Text.ToUpperInvariant().Contains(xi)));
                }

                if (!string.IsNullOrWhiteSpace(parameters.Search))
                {
                    query = query.Where(x =>
                        x.Text.ToUpperInvariant().Contains(parameters.Search.ToUpperInvariant()));
                }

                return query;
            }
        }
    }
}
