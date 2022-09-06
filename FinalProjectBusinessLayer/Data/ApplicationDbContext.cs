using FinalProjectBusinessLayer.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectBusinessLayer.Data

{
    public class ApplicationDbContext : IdentityDbContext<Member>
    {
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<TeamLeader> TeamLeaders { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkAttachment> WorkAttachments { get; set; }
        public DbSet<ProjectsMembers> projectsMembers { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<WorkHistory> WorkHistories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Member>()
                .HasOne(x => x.band)
                .WithMany(x => x.Members).HasForeignKey(x => x.BandId);

            builder.Entity<Project>()
                .HasMany(x => x.projectsMembers)
                .WithOne(x => x.Project).HasForeignKey(x => x.ProjectId);



            builder.Entity<Member>()
                .HasMany(x => x.projectsMembers)
                .WithOne(x => x.Member).HasForeignKey(x => x.Id);

            builder.Entity<ProjectsMembers>().HasKey(x => new { x.ProjectId, x.Id });


            builder.Entity<Project>()
                .HasMany(x => x.Sprints)
                .WithOne(x => x.Project).HasForeignKey(x => x.ProjectId);

            builder.Entity<Sprint>()
               .HasOne(x => x.TeamLeader)
               .WithMany(x => x.Sprints).HasForeignKey(x => x.TeamLeaderId);
            builder.Entity<Sprint>()
             .HasMany(x => x.Duties)
             .WithOne(x => x.Sprint).HasForeignKey(x => x.SprintId);

            builder.Entity<Duty>()
             .HasOne(x => x.TeamMember)
             .WithMany(x => x.Duties).HasForeignKey(x => x.TeamMemberId);

            builder.Entity<Work>()
          .HasOne(x => x.Duty)
          .WithMany(x => x.Works).HasForeignKey(x => x.DutyId);

            builder.Entity<Status>()
          .HasMany(x => x.Projects)
          .WithOne(x => x.Status).HasForeignKey(x => x.StatusId);

            builder.Entity<Status>()
          .HasMany(x => x.Sprints)
          .WithOne(x => x.Status).HasForeignKey(x => x.StatusId);

            builder.Entity<Status>()
          .HasMany(x => x.Works)
          .WithOne(x => x.Status).HasForeignKey(x => x.StatusId);
            builder.Entity<Status>()
         .HasMany(x => x.Duties)
         .WithOne(x => x.Status).HasForeignKey(x => x.StatusId);
            builder.Entity<WorkHistory>()
     .HasOne(x => x.Work)
     .WithMany(x => x.WorkHistoryList).HasForeignKey(x => x.WorkId);
            builder.Entity<WorkAttachment>()
                .HasOne(s => s.Work)
                .WithMany(s => s.WorkAttachments)
                .HasForeignKey(s => s.WorkId);

        }
    }
}
