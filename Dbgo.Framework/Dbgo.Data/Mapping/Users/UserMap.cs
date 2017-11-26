

using Dbgo.Core.Domain;

namespace Dbgo.Data.Mapping.Users
{
    public partial class UserMap : DbgoEntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.ToTable("User");

            this.HasKey(c => c.Id);
            this.Property(c => c.Name).HasMaxLength(100);
            this.Property(c => c.Pwd).HasMaxLength(100);

        }
    }
}
