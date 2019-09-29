using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Identity;
using Shared.Links;
using Shared.Prices;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.Database
{
    public class GoodNewsContext : IdentityDbContext<User, Role, int>
    {
        public GoodNewsContext(DbContextOptions<GoodNewsContext> options) : base(options)
        {
        }

        public DbSet<Work> Works { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<SocialNetworkType> SocialNetworkTypes { get; set; }
        public DbSet<SocialNetworkLink> SocialNetworkLinks { get; set; }
        public DbSet<NetworkTypeToWork> NetworkTypeToWorks { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PriceType> PriceTypes{ get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<Interest> Interests{ get; set; }
        public DbSet<InterestToPerson> InterestToPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NetworkTypeToWork>().HasKey(nttw => new { nttw.SocialNetworkTypeId, nttw.WorkId });
            modelBuilder.Entity<NetworkTypeToWork>()
                .HasOne(nttw => nttw.Work)
                .WithMany(w => w.WantedRecources)
                .HasForeignKey(nttw => nttw.WorkId);

            modelBuilder.Entity<NetworkTypeToWork>()
                .HasOne(nttw => nttw.SocialNetworkType)
                .WithMany(snt => snt.WantedByWorks)
                .HasForeignKey(nttw => nttw.SocialNetworkTypeId);

            modelBuilder.Entity<InterestToPerson>().HasKey(itp => new { itp.InterestId, itp.UserId });
            modelBuilder.Entity<InterestToPerson>()
                .HasOne(itp => itp.User)
                .WithMany(u => u.Interests)
                .HasForeignKey(itp => itp.UserId);
            modelBuilder.Entity<InterestToPerson>()
                .HasOne(itp => itp.Interest)
                .WithMany(u => u.Persons)
                .HasForeignKey(itp => itp.InterestId);

            modelBuilder.Entity<Work>()
                .HasOne(w => w.ContactPerson)
                .WithMany(u => u.WorksAsContact)
                .HasForeignKey(w => w.ContactPersonId);

            modelBuilder.Entity<Work>()
                .HasOne(w => w.Publisher)
                .WithMany(u => u.WorksAsPublisher)
                .HasForeignKey(w => w.PublisherId);

            modelBuilder.Entity<Work>()
                .HasOne(w => w.PersonInitiator)
                .WithMany(u => u.WorksAsVolunteer)
                .HasForeignKey(w => w.PersonInitiatorId);
        }
    }
}
