using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("friend_status", new[] { "pending", "accepted", "rejected" })
            .HasPostgresEnum("message_status",
                new[] { "sent", "delivered", "read", "sending", "failed", "deleted", "recalled" })
            .HasPostgresEnum("message_type", new[] { "text", "image" })
            .HasPostgresEnum("user_role", new[] { "admin", "user" });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conversations_pkey");

            entity.ToTable("conversations");

            entity.HasIndex(e => new { e.User1Id, e.User2Id }, "conversations_user1_id_user2_id_key").IsUnique();

            entity.HasIndex(e => e.UpdatedAt, "idx_conversation_updated_at").IsDescending();

            entity.HasIndex(e => e.User1Id, "idx_conversation_user1");

            entity.HasIndex(e => e.User2Id, "idx_conversation_user2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LastMessage).HasColumnName("last_message");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.User1Id).HasColumnName("user1_id");
            entity.Property(e => e.User2Id).HasColumnName("user2_id");

            entity.HasOne<User>().WithMany()
                .HasForeignKey(d => d.User1Id)
                .HasConstraintName("conversations_user1_id_fkey");

            entity.HasOne<User>().WithMany()
                .HasForeignKey(d => d.User2Id)
                .HasConstraintName("conversations_user2_id_fkey");
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("friends_pkey");

            entity.ToTable("friends");

            entity.HasIndex(e => new { e.UserId, e.FriendId }, "friends_user_id_friend_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status).HasColumnName("status").HasConversion<string>()
                .HasDefaultValue(FriendStatus.Pending);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.FriendId).HasColumnName("friend_id");
            entity.Property(e => e.BlockedByUserId)
                .HasDefaultValue(null)
                .HasColumnName("blocked_by_user_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne<User>().WithMany()
                .HasForeignKey(d => d.FriendId)
                .HasConstraintName("friends_friend_id_fkey");

            entity.HasOne<User>().WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("friends_user_id_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.HasIndex(e => e.ConversationId, "idx_message_conversation");

            entity.HasIndex(e => e.Timestamp, "idx_message_timestamp");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Status).HasColumnName("status").HasConversion<string>().HasDefaultValue(MessageStatus.Sending);
            entity.Property(e => e.Type).HasColumnName("type").HasConversion<string>().HasDefaultValue(MessageType.Text);
            entity.Property(e => e.ConversationId).HasColumnName("conversation_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("timestamp");

            entity.HasOne<Conversation>().WithMany()
                .HasForeignKey(d => d.ConversationId)
                .HasConstraintName("messages_conversation_id_fkey");

            entity.HasOne<User>().WithMany()
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("messages_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();
            
            entity.HasIndex(e => e.DisplayName, "users_display_name_key");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Role)
                .HasColumnName("role")
                .HasConversion<string>()
                .HasMaxLength(10)
                .HasDefaultValue(RoleType.User);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("display_name");
            entity.Property(e => e.IsLocked)
                .HasDefaultValue(false)
                .HasColumnName("is_locked");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_status_pkey");

            entity.ToTable("user_status");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.IsOnline)
                .HasDefaultValue(false)
                .HasColumnName("is_online");
            entity.Property(e => e.LastSeen)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("last_seen");

            entity.HasOne<User>().WithOne()
                .HasForeignKey<UserStatus>(d => d.UserId)
                .HasConstraintName("user_status_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}