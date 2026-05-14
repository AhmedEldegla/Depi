using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Companies;
using Depi.Domain.Enums;
using Depi.Domain.Entities.Community;




namespace DEPI.Domain.Entities.Community;

public class CommunityPost : AuditableEntity
{
    public Guid AuthorId { get; set; }
    public Guid? CompanyId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public PostType Type { get; set; } = PostType.Text;
    public PostStatus Status { get; set; } = PostStatus.Published;
    public int ViewsCount { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int SharesCount { get; set; }
    public int BookmarksCount { get; set; }
    public bool IsPinned { get; set; }
    public bool IsFeatured { get; set; }
    public bool AllowComments { get; set; } = true;
    public string Tags { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal SentimentScore { get; set; }

    public virtual User Author { get; set; } = null!;
    public virtual Company? Company { get; set; }
    public virtual ICollection<PostComment> Comments { get; set; } = new List<PostComment>();
    public virtual ICollection<PostLike> Likes { get; set; } = new List<PostLike>();
    public virtual ICollection<PostBookmark> Bookmarks { get; set; } = new List<PostBookmark>();
    public virtual ICollection<PostShare> Shares { get; set; } = new List<PostShare>();
}







