using System;
using System.Collections.Generic;
using DEPI.Domain.Common.Base;
using DEPI.Domain.Entities.Identity;
using DEPI.Domain.Entities.Companies;

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

public class PostComment : AuditableEntity
{
    public Guid PostId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid? ParentCommentId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int LikesCount { get; set; }
    public int RepliesCount { get; set; }
    public bool IsEdited { get; set; }
    public bool IsDeleted { get; set; }
    public int Depth { get; set; }

    public virtual CommunityPost Post { get; set; } = null!;
    public virtual User Author { get; set; } = null!;
    public virtual PostComment? ParentComment { get; set; }
    public virtual ICollection<PostComment> Replies { get; set; } = new List<PostComment>();
}

public class PostLike : AuditableEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

    public virtual CommunityPost Post { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}

public class PostBookmark : AuditableEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }

    public virtual CommunityPost Post { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}

public class PostShare : AuditableEntity
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public virtual CommunityPost Post { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}

public class ForumThread : AuditableEntity
{
    public Guid AuthorId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public ThreadStatus Status { get; set; } = ThreadStatus.Open;
    public int ViewsCount { get; set; }
    public int RepliesCount { get; set; }
    public int LikesCount { get; set; }
    public bool IsPinned { get; set; }
    public bool IsLocked { get; set; }
    public bool IsSolved { get; set; }
    public string Tags { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }

    public virtual User Author { get; set; } = null!;
    public virtual ForumCategory Category { get; set; } = null!;
    public virtual ICollection<ForumReply> Replies { get; set; } = new List<ForumReply>();
}

public class ForumCategory : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Color { get; set; } = "#007bff";
    public bool IsActive { get; set; } = true;
    public int ThreadsCount { get; set; }
    public int DisplayOrder { get; set; }

    public virtual ICollection<ForumThread> Threads { get; set; } = new List<ForumThread>();
}

public class ForumReply : AuditableEntity
{
    public Guid ThreadId { get; set; }
    public Guid AuthorId { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsAcceptedAnswer { get; set; }
    public bool IsEdited { get; set; }
    public int LikesCount { get; set; }
    public int Depth { get; set; }

    public virtual ForumThread Thread { get; set; } = null!;
    public virtual User Author { get; set; } = null!;
}

public enum PostType
{
    Text,
    Image,
    Video,
    Link,
    Poll,
    Announcement
}

public enum PostStatus
{
    Draft,
    Published,
    Scheduled,
    Archived,
    Removed
}

public enum ThreadStatus
{
    Open,
    Closed,
    Solved,
    Archived
}