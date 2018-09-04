﻿using Microsoft.EntityFrameworkCore;
using Anabi.DataAccess.Ef.DbModels;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Anabi.DataAccess.Ef.EntityConfigurators
{
    public class AssetConfig : IEntityConfig
    {
        public void SetupEntity(ModelBuilder modelBuilder)
        {

            var entity = modelBuilder.Entity<AssetDb>();
            entity.ToTable("Assets");

            entity.HasKey(k => k.Id);
            entity.HasOne(a => a.Address)
                .WithMany(b => b.Assets)
                .HasForeignKey(k => k.AddressId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Assets_Addresses")
                .IsRequired();

            entity.HasOne(k => k.Category)
                .WithMany(b => b.Assets)
                .HasForeignKey(k => k.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Assets_Categories")
                .IsRequired();

            

            entity.HasOne(d => d.CurrentDecision)
                .WithMany(b => b.Assets)
                .HasForeignKey(k => k.DecisionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Assets_Decisions")
                .IsRequired();

            entity.Property(b => b.IsDeleted)
                .IsRequired();

            entity.Property(p => p.UserCodeAdd)
              .HasMaxLength(20)
              .IsRequired();

            entity.Property(p => p.UserCodeLastChange)
                .HasMaxLength(20);


            entity.Property(p => p.AddedDate)
                .HasColumnType("DateTime")
                .IsRequired();

            entity.Property(p => p.LastChangeDate)
                .HasColumnType("Datetime");

            entity.Property(p => p.Identifier)
                .HasMaxLength(100);

            entity.Property(p => p.NecessaryVolume)
                .HasColumnType("Decimal(20, 2)");

        }
    }
}
