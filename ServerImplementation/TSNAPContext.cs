using System;
using System.Data.Entity.Core.Objects.DataClasses;
using System.IO;
using System.Web.Services.Description;

using DBClassesLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace ServerImplementation
{
    public partial class TSNAPContext : DbContext
    {
        private readonly string _schemaName;
        private string Login;
        private string Password;
        public TSNAPContext(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public TSNAPContext()
        {
            Login = "postgres";
            Password = "1riffik1";
        }

        public TSNAPContext(DbContextOptions<TSNAPContext> options, IConfiguration configuration)
            : base(options)
        {
            _schemaName = configuration["DbFunctionSchema"];
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Addressfull> Addressfulls { get; set; }
        public virtual DbSet<Benefit> Benefits { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Contractorsland> Contractorslands { get; set; }
        public virtual DbSet<Contractorslandfull> Contractorslandfulls { get; set; }
        public virtual DbSet<Contractorsproperty> Contractorsproperties { get; set; }
        public virtual DbSet<Contractorspropertyfull> Contractorspropertyfulls { get; set; }
        public virtual DbSet<Counterparty> Counterparties { get; set; }
        public virtual DbSet<Counterpartyandbenefit> Counterpartyandbenefits { get; set; }
        public virtual DbSet<Counterpartyfull> Counterpartyfulls { get; set; }
        public virtual DbSet<Counterpartygroup> Counterpartygroups { get; set; }
        public virtual DbSet<Counterpartyorder> Counterpartyorders { get; set; }
        public virtual DbSet<Counterpartytax> Counterpartytaxes { get; set; }
        public virtual DbSet<Counterpartytype> Counterpartytypes { get; set; }
        public virtual DbSet<Debt> Debts { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Landplot> Landplots { get; set; }
        public virtual DbSet<Landplotfull> Landplotfulls { get; set; }
        public virtual DbSet<Monetaryvaluation> Monetaryvaluations { get; set; }
        public virtual DbSet<Nacechapter> Nacechapters { get; set; }
        public virtual DbSet<Naceclass> Naceclasses { get; set; }
        public virtual DbSet<Nacegroup> Nacegroups { get; set; }
        public virtual DbSet<Nacesection> Nacesections { get; set; }
        public virtual DbSet<Ownershiptype> Ownershiptypes { get; set; }
        public virtual DbSet<Propertysquare> Propertysquares { get; set; }
        public virtual DbSet<Propertytype> Propertytypes { get; set; }
        public virtual DbSet<Quarterincome> Quarterincomes { get; set; }
        public virtual DbSet<Quartertax> Quartertaxes { get; set; }
        public virtual DbSet<Realproperty> Realproperties { get; set; }
        public virtual DbSet<Realpropertyfull> Realpropertyfulls { get; set; }
        public virtual DbSet<Selectionaddress> Selectionaddresses { get; set; }
        public virtual DbSet<Specialpurposechapter> Specialpurposechapters { get; set; }
        public virtual DbSet<Specialpurposeland> Specialpurposelands { get; set; }
        public virtual DbSet<Specialpurposesection> Specialpurposesections { get; set; }
        public virtual DbSet<Specialpurposesubgroup> Specialpurposesubgroups { get; set; }
        public virtual DbSet<Squarelandplot> Squarelandplots { get; set; }
        public virtual DbSet<Standartvaluation> Standartvaluations { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<User> Users { get; set; }


        [EdmFunction("TSNAP", "get_group_name")]
        public string get_group_name(string login)
        {
            throw new NotSupportedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                // установка пути к текущему каталогу
                builder.SetBasePath(Directory.GetCurrentDirectory());
                // получаем конфигурацию из файла appsettings.json
                builder.AddJsonFile("appsettings.json");
                // создаем конфигурацию
                var config = builder.Build();
                // получаем строку подключения
                string connectionString = config.GetConnectionString("DefaultConnection");

                optionsBuilder.UseNpgsql(string.Format(connectionString, Login, Password));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Ukrainian_Ukraine.1251");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => new { e.Citykey, e.Streetkey })
                    .HasName("address_pkey");

                entity.ToTable("address");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.HasOne(d => d.CitykeyNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.Citykey)
                    .HasConstraintName("address_citykey_fkey");

                entity.HasOne(d => d.StreetkeyNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.Streetkey)
                    .HasConstraintName("address_streetkey_fkey");
            });

            modelBuilder.Entity<Addressfull>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("addressfull");

                entity.Property(e => e.City)
                    .HasColumnType("character varying")
                    .HasColumnName("city");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Street)
                    .HasColumnType("character varying")
                    .HasColumnName("street");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");
            });

            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.HasKey(e => e.Benefitskey)
                    .HasName("benefits_pkey");

                entity.ToTable("benefits");

                entity.HasIndex(e => e.Benefit1, "benefits_benefit_key")
                    .IsUnique();

                entity.Property(e => e.Benefitskey)
                    .HasColumnName("benefitskey")
                    .HasDefaultValueSql("nextval('benefits_key_seq'::regclass)");

                entity.Property(e => e.Benefit1)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("benefit");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Citykey)
                    .HasName("city_pkey");

                entity.ToTable("city");

                entity.HasIndex(e => e.City1, "city_city_key")
                    .IsUnique();

                entity.Property(e => e.Citykey)
                    .HasColumnName("citykey")
                    .HasDefaultValueSql("nextval('city_key_seq'::regclass)");

                entity.Property(e => e.City1)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("city");
            });

            modelBuilder.Entity<Contractorsland>(entity =>
            {
                entity.HasKey(e => e.Contractorslandkey)
                    .HasName("contractorsland_pkey");

                entity.ToTable("contractorsland");

                entity.HasIndex(e => new { e.Landplotkey, e.Counterpartykey, e.Contractorslanddate }, "contractorsland_landplotkey_counterpartykey_date_key")
                    .IsUnique();

                entity.Property(e => e.Contractorslandkey)
                    .HasColumnName("contractorslandkey")
                    .HasDefaultValueSql("nextval('contractorsland_key_seq'::regclass)");

                entity.Property(e => e.Contractorslanddate)
                    .HasColumnType("date")
                    .HasColumnName("contractorslanddate");

                entity.Property(e => e.Counterpartykey).HasColumnName("counterpartykey");

                entity.Property(e => e.Landplotkey).HasColumnName("landplotkey");

                entity.Property(e => e.Share)
                    .HasColumnName("share")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Withouttax)
                    .HasColumnName("withouttax")
                    .HasDefaultValueSql("false");

                entity.HasOne(d => d.CounterpartykeyNavigation)
                    .WithMany(p => p.Contractorslands)
                    .HasForeignKey(d => d.Counterpartykey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contractorsland_counterpartykey_fkey");

                entity.HasOne(d => d.LandplotkeyNavigation)
                    .WithMany(p => p.Contractorslands)
                    .HasForeignKey(d => d.Landplotkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contractorsland_landplotkey_fkey");
            });

            modelBuilder.Entity<Contractorslandfull>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("contractorslandfull");

                entity.Property(e => e.Contractorslanddate)
                    .HasColumnType("date")
                    .HasColumnName("contractorslanddate");

                entity.Property(e => e.Contractorslandkey).HasColumnName("contractorslandkey");

                entity.Property(e => e.Counterpartykey).HasColumnName("counterpartykey");

                entity.Property(e => e.Counterpartyname)
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartyname");

                entity.Property(e => e.Landplotkey).HasColumnName("landplotkey");

                entity.Property(e => e.Share).HasColumnName("share");

                entity.Property(e => e.Withouttax).HasColumnName("withouttax");
            });

            modelBuilder.Entity<Contractorsproperty>(entity =>
            {
                entity.HasKey(e => e.Contractorspropertykey)
                    .HasName("contractorsproperty_pkey");

                entity.ToTable("contractorsproperty");

                entity.HasIndex(e => new { e.Realpropertykey, e.Counterpartykey, e.Contractorspropertydate }, "contractorsproperty_propertykey_counterpartykey_date_key")
                    .IsUnique();

                entity.Property(e => e.Contractorspropertykey)
                    .HasColumnName("contractorspropertykey")
                    .HasDefaultValueSql("nextval('contractorsproperty_key_seq'::regclass)");

                entity.Property(e => e.Contractorspropertydate)
                    .HasColumnType("date")
                    .HasColumnName("contractorspropertydate");

                entity.Property(e => e.Counterpartykey).HasColumnName("counterpartykey");

                entity.Property(e => e.Realpropertykey).HasColumnName("realpropertykey");

                entity.Property(e => e.Share)
                    .HasColumnName("share")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.CounterpartykeyNavigation)
                    .WithMany(p => p.Contractorsproperties)
                    .HasForeignKey(d => d.Counterpartykey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contractorsproperty_counterpartykey_fkey");

                entity.HasOne(d => d.RealpropertykeyNavigation)
                    .WithMany(p => p.Contractorsproperties)
                    .HasForeignKey(d => d.Realpropertykey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("contractorsproperty_propertykey_fkey");
            });

            modelBuilder.Entity<Contractorspropertyfull>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("contractorspropertyfull");

                entity.Property(e => e.Contractorspropertydate)
                    .HasColumnType("date")
                    .HasColumnName("contractorspropertydate");

                entity.Property(e => e.Contractorspropertykey).HasColumnName("contractorspropertykey");

                entity.Property(e => e.Counterpartykey).HasColumnName("counterpartykey");

                entity.Property(e => e.Counterpartyname)
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartyname");

                entity.Property(e => e.Realpropertykey).HasColumnName("realpropertykey");

                entity.Property(e => e.Share).HasColumnName("share");
            });

            modelBuilder.Entity<Counterparty>(entity =>
            {
                entity.HasKey(e => e.Counterpartykey)
                    .HasName("counterparty_pkey");

                entity.ToTable("counterparty");

                entity.HasIndex(e => e.Itn, "counterparty_itn_key")
                    .IsUnique();

                entity.HasIndex(e => e.Counterpartyname, "counterparty_name_key")
                    .IsUnique();

                entity.Property(e => e.Counterpartykey)
                    .HasColumnName("counterpartykey")
                    .HasDefaultValueSql("nextval('counterparty_key_seq'::regclass)");

                entity.Property(e => e.Chapterkey).HasColumnName("chapterkey");

                entity.Property(e => e.Classkey).HasColumnName("classkey");

                entity.Property(e => e.Counterpartycitykey).HasColumnName("counterpartycitykey");

                entity.Property(e => e.Counterpartygroupkey).HasColumnName("counterpartygroupkey");

                entity.Property(e => e.Counterpartyname)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartyname");

                entity.Property(e => e.Counterpartytypekey).HasColumnName("counterpartytypekey");

                entity.Property(e => e.Duty)
                    .HasColumnType("money")
                    .HasColumnName("duty")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Extrainformation)
                    .HasColumnType("character varying")
                    .HasColumnName("extrainformation");

                entity.Property(e => e.Flattaxgroup).HasColumnName("flattaxgroup");

                entity.Property(e => e.Groupkey).HasColumnName("groupkey");

                entity.Property(e => e.Housenum)
                    .HasColumnType("character varying")
                    .HasColumnName("housenum");

                entity.Property(e => e.Itn)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("itn");

                entity.Property(e => e.Legalcity).HasColumnName("legalcity");

                entity.Property(e => e.Legalhousenum)
                    .HasColumnType("character varying")
                    .HasColumnName("legalhousenum");

                entity.Property(e => e.Legalstreet).HasColumnName("legalstreet");

                entity.Property(e => e.Phonenum)
                    .HasMaxLength(15)
                    .HasColumnName("phonenum");

                entity.Property(e => e.Selectionaddresskey).HasColumnName("selectionaddresskey");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.HasOne(d => d.CounterpartygroupkeyNavigation)
                    .WithMany(p => p.Counterparties)
                    .HasForeignKey(d => d.Counterpartygroupkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterparty_counterpartygroup_fkey");

                entity.HasOne(d => d.CounterpartytypekeyNavigation)
                    .WithMany(p => p.Counterparties)
                    .HasForeignKey(d => d.Counterpartytypekey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterparty_type_fkey");

                entity.HasOne(d => d.SelectionaddresskeyNavigation)
                    .WithMany(p => p.Counterparties)
                    .HasForeignKey(d => d.Selectionaddresskey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterparty_selectionaddress_fkey");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.CounterpartyAddresses)
                    .HasForeignKey(d => new { d.Counterpartycitykey, d.Streetkey })
                    .HasConstraintName("counterparty_city_street_fkey");

                entity.HasOne(d => d.Legal)
                    .WithMany(p => p.CounterpartyLegals)
                    .HasForeignKey(d => new { d.Legalcity, d.Legalstreet })
                    .HasConstraintName("counterparty_legalcity_legalstreet_fkey");

                entity.HasOne(d => d.Naceclass)
                    .WithMany(p => p.Counterparties)
                    .HasForeignKey(d => new { d.Classkey, d.Groupkey, d.Chapterkey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterparty_classkey_groupkey_chapterkey_fkey");
            });

            modelBuilder.Entity<Counterpartyandbenefit>(entity =>
            {
                entity.HasKey(e => new { e.Counterpartykey, e.Benefitskey })
                    .HasName("counterpartyandbenefits_pkey");

                entity.ToTable("counterpartyandbenefits");

                entity.Property(e => e.Counterpartykey).HasColumnName("counterpartykey");

                entity.Property(e => e.Benefitskey).HasColumnName("benefitskey");

                entity.Property(e => e.Counterpartyandbenefitsdate)
                    .HasColumnType("date")
                    .HasColumnName("counterpartyandbenefitsdate");

                entity.HasOne(d => d.BenefitskeyNavigation)
                    .WithMany(p => p.Counterpartyandbenefits)
                    .HasForeignKey(d => d.Benefitskey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterpartyandbenefits_benefitskey_fkey");

                entity.HasOne(d => d.CounterpartykeyNavigation)
                    .WithMany(p => p.Counterpartyandbenefits)
                    .HasForeignKey(d => d.Counterpartykey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterpartyandbenefits_counterpartykey_fkey");
            });

            modelBuilder.Entity<Counterpartyfull>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("counterpartyfull");

                entity.Property(e => e.Benefits).HasColumnName("benefits");

                entity.Property(e => e.Benefitskeys).HasColumnName("benefitskeys");

                entity.Property(e => e.Chapter)
                    .HasColumnType("character varying")
                    .HasColumnName("chapter");

                entity.Property(e => e.Chapterkey).HasColumnName("chapterkey");

                entity.Property(e => e.City)
                    .HasColumnType("character varying")
                    .HasColumnName("city");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Class)
                    .HasColumnType("character varying")
                    .HasColumnName("class");

                entity.Property(e => e.Classkey).HasColumnName("classkey");

                entity.Property(e => e.Counterpartycitykey).HasColumnName("counterpartycitykey");

                entity.Property(e => e.Counterpartygroup)
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartygroup");

                entity.Property(e => e.Counterpartygroupkey).HasColumnName("counterpartygroupkey");

                entity.Property(e => e.Counterpartykey).HasColumnName("counterpartykey");

                entity.Property(e => e.Counterpartyname)
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartyname");

                entity.Property(e => e.Counterpartytypekey).HasColumnName("counterpartytypekey");

                entity.Property(e => e.Duty)
                    .HasColumnType("money")
                    .HasColumnName("duty");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Extrainformation)
                    .HasColumnType("character varying")
                    .HasColumnName("extrainformation");

                entity.Property(e => e.Flattaxgroup).HasColumnName("flattaxgroup");

                entity.Property(e => e.Groupkey).HasColumnName("groupkey");

                entity.Property(e => e.Grouptext)
                    .HasColumnType("character varying")
                    .HasColumnName("grouptext");

                entity.Property(e => e.Housenum)
                    .HasColumnType("character varying")
                    .HasColumnName("housenum");

                entity.Property(e => e.Itn)
                    .HasMaxLength(10)
                    .HasColumnName("itn");

                entity.Property(e => e.Legalcity).HasColumnName("legalcity");

                entity.Property(e => e.Legalhousenum)
                    .HasColumnType("character varying")
                    .HasColumnName("legalhousenum");

                entity.Property(e => e.Legalstreet).HasColumnName("legalstreet");

                entity.Property(e => e.Phonenum)
                    .HasMaxLength(15)
                    .HasColumnName("phonenum");

                entity.Property(e => e.Section)
                    .HasColumnType("character varying")
                    .HasColumnName("section");

                entity.Property(e => e.Sectionkey)
                    .HasMaxLength(1)
                    .HasColumnName("sectionkey");

                entity.Property(e => e.Selectionaddresskey).HasColumnName("selectionaddresskey");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.Property(e => e.Type)
                    .HasColumnType("character varying")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Counterpartygroup>(entity =>
            {
                entity.HasKey(e => e.Counterpartygroupkey)
                    .HasName("counterpartygroup_pkey");

                entity.ToTable("counterpartygroup");

                entity.HasIndex(e => e.Counterpartygroup1, "counterpartygroup_name_key")
                    .IsUnique();

                entity.Property(e => e.Counterpartygroupkey)
                    .HasColumnName("counterpartygroupkey")
                    .HasDefaultValueSql("nextval('counterpartygroup_key_seq'::regclass)");

                entity.Property(e => e.Counterpartygroup1)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartygroup");
            });

            modelBuilder.Entity<Counterpartyorder>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("counterpartyorder_pkey");

                entity.ToTable("counterpartyorder");

                entity.Property(e => e.Key).HasColumnName("key");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Itn)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("itn");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("purpose");

                entity.HasOne(d => d.ItnNavigation)
                    .WithMany(p => p.Counterpartyorders)
                    .HasPrincipalKey(p => p.Itn)
                    .HasForeignKey(d => d.Itn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterpartyorder_itn_fkey");
            });

            modelBuilder.Entity<Counterpartytax>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("counterpartytax_pkey");

                entity.ToTable("counterpartytax");

                entity.Property(e => e.Key).HasColumnName("key");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Itn)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("itn");

                entity.Property(e => e.Purpose)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("purpose");

                entity.HasOne(d => d.ItnNavigation)
                    .WithMany(p => p.Counterpartytaxes)
                    .HasPrincipalKey(p => p.Itn)
                    .HasForeignKey(d => d.Itn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("counterpartytax_itn_fkey");
            });

            modelBuilder.Entity<Counterpartytype>(entity =>
            {
                entity.HasKey(e => e.Counterpartytypekey)
                    .HasName("counterpartytype_pkey");

                entity.ToTable("counterpartytype");

                entity.Property(e => e.Counterpartytypekey)
                    .HasColumnName("counterpartytypekey")
                    .HasDefaultValueSql("nextval('counterpartytype_key_seq'::regclass)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Debt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("debt");

                entity.Property(e => e.Benefits).HasColumnName("benefits");

                entity.Property(e => e.Benefitskeys).HasColumnName("benefitskeys");

                entity.Property(e => e.Chapter)
                    .HasColumnType("character varying")
                    .HasColumnName("chapter");

                entity.Property(e => e.Chapterkey).HasColumnName("chapterkey");

                entity.Property(e => e.City)
                    .HasColumnType("character varying")
                    .HasColumnName("city");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Class)
                    .HasColumnType("character varying")
                    .HasColumnName("class");

                entity.Property(e => e.Classkey).HasColumnName("classkey");

                entity.Property(e => e.Counterpartycitykey).HasColumnName("counterpartycitykey");

                entity.Property(e => e.Counterpartygroup)
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartygroup");

                entity.Property(e => e.Counterpartygroupkey).HasColumnName("counterpartygroupkey");

                entity.Property(e => e.Counterpartykey).HasColumnName("counterpartykey");

                entity.Property(e => e.Counterpartyname)
                    .HasColumnType("character varying")
                    .HasColumnName("counterpartyname");

                entity.Property(e => e.Counterpartytypekey).HasColumnName("counterpartytypekey");

                entity.Property(e => e.Duty)
                    .HasColumnType("money")
                    .HasColumnName("duty");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .HasColumnName("email");

                entity.Property(e => e.Extrainformation)
                    .HasColumnType("character varying")
                    .HasColumnName("extrainformation");

                entity.Property(e => e.Flattaxgroup).HasColumnName("flattaxgroup");

                entity.Property(e => e.Groupkey).HasColumnName("groupkey");

                entity.Property(e => e.Grouptext)
                    .HasColumnType("character varying")
                    .HasColumnName("grouptext");

                entity.Property(e => e.Housenum)
                    .HasColumnType("character varying")
                    .HasColumnName("housenum");

                entity.Property(e => e.Itn)
                    .HasMaxLength(10)
                    .HasColumnName("itn");

                entity.Property(e => e.Legalcity).HasColumnName("legalcity");

                entity.Property(e => e.Legalhousenum)
                    .HasColumnType("character varying")
                    .HasColumnName("legalhousenum");

                entity.Property(e => e.Legalstreet).HasColumnName("legalstreet");

                entity.Property(e => e.Phonenum)
                    .HasMaxLength(15)
                    .HasColumnName("phonenum");

                entity.Property(e => e.Section)
                    .HasColumnType("character varying")
                    .HasColumnName("section");

                entity.Property(e => e.Sectionkey)
                    .HasMaxLength(1)
                    .HasColumnName("sectionkey");

                entity.Property(e => e.Selectionaddresskey).HasColumnName("selectionaddresskey");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.Property(e => e.Type)
                    .HasColumnType("character varying")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("income");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Incomeperday).HasColumnName("incomeperday");

                entity.Property(e => e.Totalincome).HasColumnName("totalincome");
            });

            modelBuilder.Entity<Landplot>(entity =>
            {
                entity.HasKey(e => e.Landplotkey)
                    .HasName("landplot_pkey");

                entity.ToTable("landplot");

                entity.HasIndex(e => e.Cadastralnumber, "landplot_cadastralnumber_key")
                    .IsUnique();

                entity.HasIndex(e => e.Landplotname, "name_unq")
                    .IsUnique();

                entity.Property(e => e.Landplotkey)
                    .HasColumnName("landplotkey")
                    .HasDefaultValueSql("nextval('landplot_key_seq'::regclass)");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Cadastralnumber)
                    .IsRequired()
                    .HasMaxLength(22)
                    .HasColumnName("cadastralnumber")
                    .IsFixedLength(true);

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Extrainformation)
                    .HasColumnType("character varying")
                    .HasColumnName("extrainformation");

                entity.Property(e => e.Housenum)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("housenum");

                entity.Property(e => e.Landplotname)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("landplotname");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Ownershiptypekey).HasColumnName("ownershiptypekey");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.HasOne(d => d.OwnershiptypekeyNavigation)
                    .WithMany(p => p.Landplots)
                    .HasForeignKey(d => d.Ownershiptypekey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("landplot_ownershiptype_fkey");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Landplots)
                    .HasForeignKey(d => new { d.Citykey, d.Streetkey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("landplot_city_street_fkey");
            });

            modelBuilder.Entity<Landplotfull>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("landplotfull");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Bidinside).HasColumnName("bidinside");

                entity.Property(e => e.Bidoutside).HasColumnName("bidoutside");

                entity.Property(e => e.Cadastralnumber)
                    .HasMaxLength(22)
                    .HasColumnName("cadastralnumber")
                    .IsFixedLength(true);

                entity.Property(e => e.Chapter)
                    .HasColumnType("character varying")
                    .HasColumnName("chapter");

                entity.Property(e => e.Chapterkey).HasColumnName("chapterkey");

                entity.Property(e => e.City)
                    .HasColumnType("character varying")
                    .HasColumnName("city");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Counterpartykeys).HasColumnName("counterpartykeys");

                entity.Property(e => e.Counterpartynames).HasColumnName("counterpartynames");

                entity.Property(e => e.Extrainformation)
                    .HasColumnType("character varying")
                    .HasColumnName("extrainformation");

                entity.Property(e => e.Groupkey).HasColumnName("groupkey");

                entity.Property(e => e.Housenum)
                    .HasColumnType("character varying")
                    .HasColumnName("housenum");

                entity.Property(e => e.Landplotkey).HasColumnName("landplotkey");

                entity.Property(e => e.Landplotname)
                    .HasColumnType("character varying")
                    .HasColumnName("landplotname");

                entity.Property(e => e.Location).HasColumnName("location");

                entity.Property(e => e.Monetaryvaluation)
                    .HasColumnType("money")
                    .HasColumnName("monetaryvaluation");

                entity.Property(e => e.Ownershiptypekey).HasColumnName("ownershiptypekey");

                entity.Property(e => e.Section)
                    .HasColumnType("character varying")
                    .HasColumnName("section");

                entity.Property(e => e.Sectionkey)
                    .HasMaxLength(1)
                    .HasColumnName("sectionkey");

                entity.Property(e => e.Square).HasColumnName("square");

                entity.Property(e => e.Standartvaluation)
                    .HasColumnType("money")
                    .HasColumnName("standartvaluation");

                entity.Property(e => e.Street)
                    .HasColumnType("character varying")
                    .HasColumnName("street");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.Property(e => e.Subgrouptext)
                    .HasColumnType("character varying")
                    .HasColumnName("subgrouptext");

                entity.Property(e => e.Type)
                    .HasColumnType("character varying")
                    .HasColumnName("type");

                entity.Property(e => e.Withouttax).HasColumnName("withouttax");
            });

            modelBuilder.Entity<Monetaryvaluation>(entity =>
            {
                entity.HasKey(e => e.Monetaryvaluationkey)
                    .HasName("monetaryvaluation_pkey");

                entity.ToTable("monetaryvaluation");

                entity.HasIndex(e => new { e.Landplotkey, e.Monetaryvaluation1, e.Monetaryvaluationdate }, "monetaryvaluation_landplotkey_monetaryvaluation_date_key")
                    .IsUnique();

                entity.Property(e => e.Monetaryvaluationkey)
                    .HasColumnName("monetaryvaluationkey")
                    .HasDefaultValueSql("nextval('monetaryvaluation_key_seq'::regclass)");

                entity.Property(e => e.Landplotkey).HasColumnName("landplotkey");

                entity.Property(e => e.Monetaryvaluation1)
                    .HasColumnType("money")
                    .HasColumnName("monetaryvaluation");

                entity.Property(e => e.Monetaryvaluationdate)
                    .HasColumnType("date")
                    .HasColumnName("monetaryvaluationdate")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.LandplotkeyNavigation)
                    .WithMany(p => p.Monetaryvaluations)
                    .HasForeignKey(d => d.Landplotkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("monetaryvaluation_landplotkey_fkey");
            });

            modelBuilder.Entity<Nacechapter>(entity =>
            {
                entity.HasKey(e => e.Chapterkey)
                    .HasName("nacechapter_pkey");

                entity.ToTable("nacechapter");

                entity.HasIndex(e => e.Chapter, "nacechapter_chapter_key")
                    .IsUnique();

                entity.Property(e => e.Chapterkey)
                    .ValueGeneratedNever()
                    .HasColumnName("chapterkey");

                entity.Property(e => e.Chapter)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("chapter");

                entity.Property(e => e.Sectionkey)
                    .HasMaxLength(1)
                    .HasColumnName("sectionkey");

                entity.HasOne(d => d.SectionkeyNavigation)
                    .WithMany(p => p.Nacechapters)
                    .HasForeignKey(d => d.Sectionkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nacechapter_sectionkey_fkey");
            });

            modelBuilder.Entity<Naceclass>(entity =>
            {
                entity.HasKey(e => new { e.Classkey, e.Groupkey, e.Chapterkey })
                    .HasName("naceclass_pkey");

                entity.ToTable("naceclass");

                entity.HasIndex(e => e.Class, "naceclass_class_key")
                    .IsUnique();

                entity.Property(e => e.Classkey).HasColumnName("classkey");

                entity.Property(e => e.Groupkey).HasColumnName("groupkey");

                entity.Property(e => e.Chapterkey).HasColumnName("chapterkey");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("class");

                entity.HasOne(d => d.Nacegroup)
                    .WithMany(p => p.Naceclasses)
                    .HasForeignKey(d => new { d.Groupkey, d.Chapterkey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("naceclass_groupkey_chapterkey_fkey");
            });

            modelBuilder.Entity<Nacegroup>(entity =>
            {
                entity.HasKey(e => new { e.Groupkey, e.Chapterkey })
                    .HasName("nacegroup_pkey");

                entity.ToTable("nacegroup");

                entity.HasIndex(e => e.Grouptext, "nacegroup_grouptext_key")
                    .IsUnique();

                entity.Property(e => e.Groupkey).HasColumnName("groupkey");

                entity.Property(e => e.Chapterkey).HasColumnName("chapterkey");

                entity.Property(e => e.Grouptext)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("grouptext");

                entity.HasOne(d => d.ChapterkeyNavigation)
                    .WithMany(p => p.Nacegroups)
                    .HasForeignKey(d => d.Chapterkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("nacegroup_chapterkey_fkey");
            });

            modelBuilder.Entity<Nacesection>(entity =>
            {
                entity.HasKey(e => e.Sectionkey)
                    .HasName("nacesection_pkey");

                entity.ToTable("nacesection");

                entity.HasIndex(e => e.Section, "nacesection_section_key")
                    .IsUnique();

                entity.Property(e => e.Sectionkey)
                    .HasMaxLength(1)
                    .ValueGeneratedNever()
                    .HasColumnName("sectionkey");

                entity.Property(e => e.Section)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("section");
            });

            modelBuilder.Entity<Ownershiptype>(entity =>
            {
                entity.HasKey(e => e.Ownershiptypekey)
                    .HasName("ownershiptype_pkey");

                entity.ToTable("ownershiptype");

                entity.HasIndex(e => e.Type, "ownershiptype_name_key")
                    .IsUnique();

                entity.Property(e => e.Ownershiptypekey)
                    .HasColumnName("ownershiptypekey")
                    .HasDefaultValueSql("nextval('ownershiptype_key_seq'::regclass)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Propertysquare>(entity =>
            {
                entity.HasKey(e => e.Propertysquarekey)
                    .HasName("propertysquare_pkey");

                entity.ToTable("propertysquare");

                entity.HasIndex(e => new { e.Realpropertykey, e.Square, e.Propertysquaredate }, "propertysquare_propertykey_square_date_key")
                    .IsUnique();

                entity.Property(e => e.Propertysquarekey)
                    .HasColumnName("propertysquarekey")
                    .HasDefaultValueSql("nextval('propertysquare_key_seq'::regclass)");

                entity.Property(e => e.Propertysquaredate)
                    .HasColumnType("date")
                    .HasColumnName("propertysquaredate");

                entity.Property(e => e.Realpropertykey).HasColumnName("realpropertykey");

                entity.Property(e => e.Square).HasColumnName("square");

                entity.HasOne(d => d.RealpropertykeyNavigation)
                    .WithMany(p => p.Propertysquares)
                    .HasForeignKey(d => d.Realpropertykey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("propertysquare_propertykey_fkey");
            });

            modelBuilder.Entity<Propertytype>(entity =>
            {
                entity.HasKey(e => e.Propertytypekey)
                    .HasName("propertytype_pkey");

                entity.ToTable("propertytype");

                entity.HasIndex(e => e.Type, "propertytype_type_key")
                    .IsUnique();

                entity.Property(e => e.Propertytypekey)
                    .HasColumnName("propertytypekey")
                    .HasDefaultValueSql("nextval('propertytype_key_seq'::regclass)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Quarterincome>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("quarterincome");

                entity.Property(e => e.Quarter).HasColumnName("quarter");

                entity.Property(e => e.Quarteramount).HasColumnName("quarteramount");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Quartertax>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("quartertax");

                entity.Property(e => e.Quarter).HasColumnName("quarter");

                entity.Property(e => e.Quarteramount).HasColumnName("quarteramount");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Realproperty>(entity =>
            {
                entity.HasKey(e => e.Realpropertykey)
                    .HasName("realproperty_pkey");

                entity.ToTable("realproperty");

                entity.HasIndex(e => e.Realpropertyname, "realproperty_name_unq")
                    .IsUnique();

                entity.Property(e => e.Realpropertykey)
                    .HasColumnName("realpropertykey")
                    .HasDefaultValueSql("nextval('realproperty_key_seq'::regclass)");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Extrainformation)
                    .HasColumnType("character varying")
                    .HasColumnName("extrainformation");

                entity.Property(e => e.Housenum)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("housenum");

                entity.Property(e => e.Propertytypekey).HasColumnName("propertytypekey");

                entity.Property(e => e.Purpose).HasColumnName("purpose");

                entity.Property(e => e.Realpropertyname)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("realpropertyname");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.Property(e => e.View).HasColumnName("view");

                entity.HasOne(d => d.PropertytypekeyNavigation)
                    .WithMany(p => p.Realproperties)
                    .HasForeignKey(d => d.Propertytypekey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("realproperty_type_fkey");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Realproperties)
                    .HasForeignKey(d => new { d.Citykey, d.Streetkey })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("realproperty_city_street_fkey");
            });

            modelBuilder.Entity<Realpropertyfull>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("realpropertyfull");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.City)
                    .HasColumnType("character varying")
                    .HasColumnName("city");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.Property(e => e.Contractorspropertydate)
                    .HasColumnType("date")
                    .HasColumnName("contractorspropertydate");

                entity.Property(e => e.Counterpartykeys).HasColumnName("counterpartykeys");

                entity.Property(e => e.Counterpartynames).HasColumnName("counterpartynames");

                entity.Property(e => e.Dateoflastchangedsquareinrealproperty)
                    .HasColumnType("date")
                    .HasColumnName("dateoflastchangedsquareinrealproperty");

                entity.Property(e => e.Extrainformation)
                    .HasColumnType("character varying")
                    .HasColumnName("extrainformation");

                entity.Property(e => e.Housenum)
                    .HasColumnType("character varying")
                    .HasColumnName("housenum");

                entity.Property(e => e.Propertytypekey).HasColumnName("propertytypekey");

                entity.Property(e => e.Purpose).HasColumnName("purpose");

                entity.Property(e => e.Realpropertykey).HasColumnName("realpropertykey");

                entity.Property(e => e.Realpropertyname)
                    .HasColumnType("character varying")
                    .HasColumnName("realpropertyname");

                entity.Property(e => e.Street)
                    .HasColumnType("character varying")
                    .HasColumnName("street");

                entity.Property(e => e.Streetkey).HasColumnName("streetkey");

                entity.Property(e => e.Type)
                    .HasColumnType("character varying")
                    .HasColumnName("type");

                entity.Property(e => e.View).HasColumnName("view");
            });

            modelBuilder.Entity<Selectionaddress>(entity =>
            {
                entity.HasKey(e => e.Selectionaddresskey)
                    .HasName("selectionaddress_pkey");

                entity.ToTable("selectionaddress");

                entity.Property(e => e.Selectionaddresskey)
                    .HasColumnName("selectionaddresskey")
                    .HasDefaultValueSql("nextval('selectionaddress_key_seq'::regclass)");

                entity.Property(e => e.Citykey).HasColumnName("citykey");

                entity.HasOne(d => d.CitykeyNavigation)
                    .WithMany(p => p.Selectionaddresses)
                    .HasForeignKey(d => d.Citykey)
                    .HasConstraintName("selectionaddress_city_fkey");
            });

            modelBuilder.Entity<Specialpurposechapter>(entity =>
            {
                entity.HasKey(e => e.Chapterkey)
                    .HasName("specialpurposechapter_pkey");

                entity.ToTable("specialpurposechapter");

                entity.HasIndex(e => e.Chapter, "specialpurposechapter_chapter_key")
                    .IsUnique();

                entity.Property(e => e.Chapterkey)
                    .ValueGeneratedNever()
                    .HasColumnName("chapterkey");

                entity.Property(e => e.Chapter)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("chapter");

                entity.Property(e => e.Sectionkey)
                    .HasMaxLength(1)
                    .HasColumnName("sectionkey");

                entity.HasOne(d => d.SectionkeyNavigation)
                    .WithMany(p => p.Specialpurposechapters)
                    .HasForeignKey(d => d.Sectionkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("specialpurposechapter_sectionkey_fkey");
            });

            modelBuilder.Entity<Specialpurposeland>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("specialpurposeland_pkey");

                entity.ToTable("specialpurposeland");

                entity.HasIndex(e => new { e.Specialpurposechapter, e.Specialpurposesubgroup, e.Landplotkey, e.Specialpurposelanddate }, "specialpurposeland_specialpurposechapter_specialpurposesubg_key")
                    .IsUnique();

                entity.Property(e => e.Key).HasColumnName("key");

                entity.Property(e => e.Landplotkey).HasColumnName("landplotkey");

                entity.Property(e => e.Specialpurposechapter).HasColumnName("specialpurposechapter");

                entity.Property(e => e.Specialpurposelanddate)
                    .HasColumnType("date")
                    .HasColumnName("specialpurposelanddate")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Specialpurposesubgroup).HasColumnName("specialpurposesubgroup");

                entity.HasOne(d => d.LandplotkeyNavigation)
                    .WithMany(p => p.Specialpurposelands)
                    .HasForeignKey(d => d.Landplotkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("specialpurposeland_landplotkey_fkey");
            });

            modelBuilder.Entity<Specialpurposesection>(entity =>
            {
                entity.HasKey(e => e.Sectionkey)
                    .HasName("specialpurposesection_pkey");

                entity.ToTable("specialpurposesection");

                entity.HasIndex(e => e.Section, "specialpurposesection_section_key")
                    .IsUnique();

                entity.Property(e => e.Sectionkey)
                    .HasMaxLength(1)
                    .ValueGeneratedNever()
                    .HasColumnName("sectionkey");

                entity.Property(e => e.Section)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("section");
            });

            modelBuilder.Entity<Specialpurposesubgroup>(entity =>
            {
                entity.HasKey(e => new { e.Groupkey, e.Chapterkey })
                    .HasName("specialpurposesubgroup_pkey");

                entity.ToTable("specialpurposesubgroup");

                entity.HasIndex(e => e.Subgrouptext, "specialpurposesubgroup_subgrouptext_key")
                    .IsUnique();

                entity.Property(e => e.Groupkey).HasColumnName("groupkey");

                entity.Property(e => e.Chapterkey).HasColumnName("chapterkey");

                entity.Property(e => e.Bidinside)
                    .HasColumnName("bidinside")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Bidoutside)
                    .HasColumnName("bidoutside")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Subgrouptext)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("subgrouptext");

                entity.HasOne(d => d.ChapterkeyNavigation)
                    .WithMany(p => p.Specialpurposesubgroups)
                    .HasForeignKey(d => d.Chapterkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("specialpurposesubgroup_chapterkey_fkey");
            });

            modelBuilder.Entity<Squarelandplot>(entity =>
            {
                entity.HasKey(e => e.Squarelandplotkey)
                    .HasName("squarelandplot_pkey");

                entity.ToTable("squarelandplot");

                entity.HasIndex(e => new { e.Landplotkey, e.Square, e.Squarelandplotdate }, "squarelandplot_landplotkey_square_date_key")
                    .IsUnique();

                entity.Property(e => e.Squarelandplotkey)
                    .HasColumnName("squarelandplotkey")
                    .HasDefaultValueSql("nextval('squarelandplot_key_seq'::regclass)");

                entity.Property(e => e.Landplotkey).HasColumnName("landplotkey");

                entity.Property(e => e.Square).HasColumnName("square");

                entity.Property(e => e.Squarelandplotdate)
                    .HasColumnType("date")
                    .HasColumnName("squarelandplotdate")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.LandplotkeyNavigation)
                    .WithMany(p => p.Squarelandplots)
                    .HasForeignKey(d => d.Landplotkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("squarelandplot_landplotkey_fkey");
            });

            modelBuilder.Entity<Standartvaluation>(entity =>
            {
                entity.HasKey(e => e.Standartvaluationkey)
                    .HasName("standartvaluation_pkey");

                entity.ToTable("standartvaluation");

                entity.HasIndex(e => new { e.Landplotkey, e.Standartvaluation1, e.Standartvaluationdate }, "standartvaluation_landplotkey_standartvaluation_date_key")
                    .IsUnique();

                entity.Property(e => e.Standartvaluationkey)
                    .HasColumnName("standartvaluationkey")
                    .HasDefaultValueSql("nextval('standartvaluation_key_seq'::regclass)");

                entity.Property(e => e.Landplotkey).HasColumnName("landplotkey");

                entity.Property(e => e.Standartvaluation1)
                    .HasColumnType("money")
                    .HasColumnName("standartvaluation");

                entity.Property(e => e.Standartvaluationdate)
                    .HasColumnType("date")
                    .HasColumnName("standartvaluationdate")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.LandplotkeyNavigation)
                    .WithMany(p => p.Standartvaluations)
                    .HasForeignKey(d => d.Landplotkey)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("standartvaluation_landplotkey_fkey");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.HasKey(e => e.Streetkey)
                    .HasName("street_pkey");

                entity.ToTable("street");

                entity.HasIndex(e => e.Street1, "street_street_key")
                    .IsUnique();

                entity.Property(e => e.Streetkey)
                    .HasColumnName("streetkey")
                    .HasDefaultValueSql("nextval('street_key_seq'::regclass)");

                entity.Property(e => e.Street1)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("street");
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tax");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Taxday).HasColumnName("taxday");

                entity.Property(e => e.Totaltax).HasColumnName("totaltax");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users");

                entity.Property(e => e.Passwd).HasColumnName("passwd");

                entity.Property(e => e.Rolname)
                    .HasColumnName("rolname")
                    .UseCollation("C");

                entity.Property(e => e.Usename)
                    .HasColumnName("usename")
                    .UseCollation("C");

                entity.Property(e => e.Usesysid)
                    .HasColumnType("oid")
                    .HasColumnName("usesysid");
            });


            modelBuilder.HasDbFunction(typeof(TSNAPContext)
                .GetMethod(nameof(get_group_name),
                 new[] { typeof(string) }))
                .HasName("get_group_name");


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
