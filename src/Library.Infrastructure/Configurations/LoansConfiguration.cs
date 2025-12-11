using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Persistence.Configurations
{
    public class LoansConfiguration : IEntityTypeConfiguration<Loans>
    {
        public void Configure(EntityTypeBuilder<Loans> builder)
        {
            builder.ToTable("Loans");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.StudentName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(l => l.LoanDate)
                .IsRequired();

            builder.Property(l => l.ReturnDate)
                .IsRequired(false);

            builder.Property(l => l.Status)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(l => l.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Relación con Books (N - 1)
            builder.HasOne<Books>()
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
