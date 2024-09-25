
using Microsoft.EntityFrameworkCore;
using RenadWebApp.Models.DataModel;
using RenadWebApp.Models.DataModel.FinaicalModels;

namespace RenadWebApp.DTOModels
{
    public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {
            }


        public DbSet<LoginModel> Login { get; set; }

        public DbSet<ClientModels> Clients { get; set; }

            public DbSet<ClientContract> ClientContract { get; set; }


            public DbSet<ContractModel> Contracts { get; set; }

            public DbSet<PaymentModel> Payment { get; set; }
            public DbSet<ContractPayment> ContractPayment { get; set; }

            public DbSet<EngModel> Eng { get; set; }
            public DbSet<EngContract> EngContract { get; set; }

            public DbSet<FinaicalRequest> FinaicalRequests { get; set; }
            public DbSet<ContractFinaical> ContractFinaical { get; set; }
            

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ClientModels>()
           .HasKey(p => new
           {
               p.Id
           });

            builder.Entity<ClientContract>()
              .HasKey(up => new
              {
                  up.ContractId,
                  up.ClientId
              });

            builder.Entity<ClientContract>()
              .HasOne(up => up.Contract)
              .WithMany(u => u.ClientContract)
              .HasForeignKey(u => u.ContractId)
               .OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<ClientContract>()
              .HasOne(up => up.Client)
              .WithMany(p => p.ClientContract)
              .HasForeignKey(p => p.ClientId)
              .OnDelete(DeleteBehavior.NoAction);
            ///////////////////////////////////////////////////////////////////

            builder.Entity<EngModel>()
           .HasKey(p => new
           {
               p.Id
           });

            builder.Entity<EngContract>()
              .HasKey(up => new
              {
                  up.ContractId,
                  up.EngId
              });

            builder.Entity<EngContract>()
              .HasOne(up => up.Contract)
              .WithMany(u => u.EngContract)
              .HasForeignKey(u => u.ContractId)
               .OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<EngContract>()
              .HasOne(up => up.Eng)
              .WithMany(p => p.EngContract)
              .HasForeignKey(p => p.EngId)
              .OnDelete(DeleteBehavior.NoAction);
            /////////////////////////////////////////////////////////


           // builder.Entity<ClientModels>()
           //.HasKey(p => new
           //{
           //    p.Id
           //});

           // builder.Entity<ClientPayment>()
           //   .HasKey(up => new
           //   {
           //       up.PaymentId,
           //       up.ClientId
           //   });

           // builder.Entity<ClientPayment>()
           //   .HasOne(up => up.Payment)
           //   .WithMany(u => u.ClientPayment)
           //   .HasForeignKey(u => u.PaymentId)
           //    .OnDelete(DeleteBehavior.NoAction); 

           // builder.Entity<ClientPayment>()
           //   .HasOne(up => up.Client)
           //   .WithMany(p => p.ClientPayment)
           //   .HasForeignKey(p => p.ClientId)
           //   .OnDelete(DeleteBehavior.NoAction);

            ///////////////////////////////////////////////////////
            ///

            builder.Entity<PaymentModel>()
                .HasKey(p => new
                {
                   p.Id
                });


            builder.Entity<ContractPayment>()
              .HasKey(up => new
              {
                  up.ContractId,
                  up.PaymentId,
              });

            builder.Entity<ContractPayment>()
              .HasOne(up => up.Contract)
              .WithMany(u => u.ContractPayment)
              .HasForeignKey(u => u.ContractId)
               .OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<ContractPayment>()
              .HasOne(up => up.Payment)
              .WithMany(p => p.ContractPayment)
              .HasForeignKey(p => p.PaymentId)
              .OnDelete(DeleteBehavior.NoAction);


            ////////////////////////////////////////////////////////////////////////////////////
            ///


            //builder.Entity<FinaicalRequest>()
            //  .HasKey(p => new
            //  {
            //      p.Id
            //  });


            //builder.Entity<ClientFinaical>()
            //  .HasKey(up => new
            //  {
            //      up.ClientId,
            //      up.FinaicalId,
            //  });

            //builder.Entity<ClientFinaical>()
            //  .HasOne(up => up.Finaical)
            //  .WithMany(u => u.ClientFinaical)
            //  .HasForeignKey(u => u.FinaicalId)
            //   .OnDelete(DeleteBehavior.NoAction); ;

            //builder.Entity<ClientFinaical>()
            //  .HasOne(up => up.Client)
            //  .WithMany(p => p.ClientFinaical)
            //  .HasForeignKey(p => p.ClientId)
            //  .OnDelete(DeleteBehavior.NoAction);

            //////////////////////////////////////////////////
            builder.Entity<FinaicalRequest>()
             .HasKey(p => new
             {
                 p.Id
             });


            builder.Entity<ContractFinaical>()
              .HasKey(up => new
              {
                  up.ContractId,
                  up.FinaicalId,
              });

            builder.Entity<ContractFinaical>()
              .HasOne(up => up.Contract)
              .WithMany(u => u.ContractFinaical)
              .HasForeignKey(u => u.ContractId)
               .OnDelete(DeleteBehavior.NoAction); ;

            builder.Entity<ContractFinaical>()
              .HasOne(up => up.Finaical)
              .WithMany(p => p.ContractFinaical)
              .HasForeignKey(p => p.FinaicalId)
              .OnDelete(DeleteBehavior.NoAction);




        }

    }

    }
