using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Otodom.Models;

namespace Otodom
{
    public partial class OtodomContext : DbContext
    {
        public OtodomContext()
        {
        }

        public OtodomContext(DbContextOptions<OtodomContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agencja> Agencjas { get; set; } = null!;
        public virtual DbSet<Klient> Klients { get; set; } = null!;
        public virtual DbSet<Nieruchomosc> Nieruchomoscs { get; set; } = null!;
        public virtual DbSet<Ogloszenie> Ogloszenies { get; set; } = null!;
        public virtual DbSet<Zdjecie> Zdjecies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=Otodom;User=sa;Password=zaq1@WSX;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agencja>(entity =>
            {
                entity.HasKey(e => e.IdAgencji)
                    .HasName("agencja_PK");

                entity.ToTable("agencja");

                entity.Property(e => e.IdAgencji).HasColumnName("ID_agencji");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.NazwaAgencji)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nazwa_agencji");

                entity.Property(e => e.Nip)
                    .HasColumnType("numeric(20, 0)")
                    .HasColumnName("nip");

                entity.Property(e => e.NrTelefonuAgencji)
                    .HasColumnType("numeric(9, 0)")
                    .HasColumnName("nr_telefonu_agencji");
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.HasKey(e => e.IdKlienta)
                    .HasName("klient_PK");

                entity.ToTable("klient");

                entity.Property(e => e.IdKlienta).HasColumnName("ID_klienta");

                entity.Property(e => e.AgencjaIdAgencji).HasColumnName("agencja_ID_agencji");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Imie)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("imie");

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nazwisko");

                entity.Property(e => e.NrTelefonuKlienta)
                    .HasColumnType("numeric(9, 0)")
                    .HasColumnName("nr_telefonu_klienta");

                entity.HasOne(d => d.AgencjaIdAgencjiNavigation)
                    .WithMany(p => p.Klients)
                    .HasForeignKey(d => d.AgencjaIdAgencji)
                    .HasConstraintName("klient_agencja_FK");
            });

            modelBuilder.Entity<Nieruchomosc>(entity =>
            {
                entity.HasKey(e => e.IdNieruchomosci)
                    .HasName("nieruchomosc_PK");

                entity.ToTable("nieruchomosc");

                entity.Property(e => e.IdNieruchomosci).HasColumnName("ID_nieruchomosci");

                entity.Property(e => e.KodPocztowy)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("kod_pocztowy");

                entity.Property(e => e.LiczbaPieter)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("liczba_pieter");

                entity.Property(e => e.Miasto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("miasto");

                entity.Property(e => e.NrDomu)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("nr_domu");

                entity.Property(e => e.PowierzchniaDomu)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("powierzchnia_domu");

                entity.Property(e => e.RodzajOkna)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rodzaj_okna");

                entity.Property(e => e.RodzajZabudowy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("rodzaj_zabudowy");

                entity.Property(e => e.RokBudowy)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("rok_budowy");

                entity.Property(e => e.StanWykonczenia)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("stan_wykonczenia");

                entity.Property(e => e.TypOgrzewania)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("typ_ogrzewania");

                entity.Property(e => e.Ulica)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ulica");

                entity.Property(e => e.Wojewodztwo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wojewodztwo");
            });

            modelBuilder.Entity<Ogloszenie>(entity =>
            {
                entity.HasKey(e => e.IdOgloszenia)
                    .HasName("ogloszenie_PK");

                entity.ToTable("ogloszenie");

                entity.Property(e => e.IdOgloszenia).HasColumnName("ID_ogloszenia");

                entity.Property(e => e.Cena)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("cena");

                entity.Property(e => e.DataDodania)
                    .HasColumnType("date")
                    .HasColumnName("data_dodania");

                entity.Property(e => e.KlientIdKlienta).HasColumnName("klient_ID_klienta");

                entity.Property(e => e.NieruchomoscIdNieruchomosci).HasColumnName("nieruchomosc_ID_nieruchomosci");

                entity.Property(e => e.Opis)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("opis");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tytul)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tytul");

                entity.HasOne(d => d.KlientIdKlientaNavigation)
                    .WithMany(p => p.Ogloszenies)
                    .HasForeignKey(d => d.KlientIdKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ogloszenie_klient_FK");

                entity.HasOne(d => d.NieruchomoscIdNieruchomosciNavigation)
                    .WithMany(p => p.Ogloszenies)
                    .HasForeignKey(d => d.NieruchomoscIdNieruchomosci)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ogloszenie_nieruchomosc_FK");
            });

            modelBuilder.Entity<Zdjecie>(entity =>
            {
                entity.HasKey(e => e.IdZdjecia)
                    .HasName("zdjecie_PK");

                entity.ToTable("zdjecie");

                entity.Property(e => e.IdZdjecia).HasColumnName("ID_zdjecia");

                entity.Property(e => e.NieruchomoscIdNieruchomosci).HasColumnName("nieruchomosc_ID_nieruchomosci");

                entity.Property(e => e.ZdjecieData)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("zdjecie_data");

                entity.HasOne(d => d.NieruchomoscIdNieruchomosciNavigation)
                    .WithMany(p => p.Zdjecies)
                    .HasForeignKey(d => d.NieruchomoscIdNieruchomosci)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zdjecie_nieruchomosc_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
