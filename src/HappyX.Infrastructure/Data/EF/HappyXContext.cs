using HappyX.Domain.Internal;
using Microsoft.EntityFrameworkCore;

namespace HappyX.Infrastructure.Data.EF;

public class HappyXContext : DbContext
{
    public HappyXContext(DbContextOptions<HappyXContext> options) : base(options)
    {
    }

    public virtual DbSet<Mood> Moods { get; set; }
    public virtual DbSet<Record> Records { get; set; }
    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mood>(entity =>
        {
            entity.ToTable("moods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("name");

            entity.HasData(
                new Mood(1, "sad"),
                new Mood(2, "unhappy"),
                new Mood(3, "indifferent"),
                new Mood(4, "happy"),
                new Mood(5, "joyful"));
        });

        modelBuilder.Entity<Record>(entity =>
        {
            entity.ToTable("records");

            entity.HasIndex(e => new {e.UserId, e.CreationDate}, "unique_user_date")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CreationDate).HasColumnName("creation_date");

            entity.HasOne(d => d.Mood)
                .WithMany(p => p.Records)
                .HasForeignKey(d => d.MoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("moods_fk");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Records)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");

            entity.HasIndex(e => e.SlackId, "unique_slack_id")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.SlackId)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("slack_id");

            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("username");
            
            entity.Property(u => u.Subscribed)
                .HasColumnName("subscribed");
        });
    }
}